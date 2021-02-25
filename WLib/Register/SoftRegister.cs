/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using Microsoft.Win32;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WLib.Computer;

namespace WLib.Register
{
    /* 为了代码安全和防破解需要，请对该类所在库的代码进行混淆、加壳  */
    /// <summary>
    /// 软件注册器
    /// <para>提供机器码、注册码的生成、验证、注册表写入等功能</para>
    /// </summary>
    public sealed class SoftRegister
    {
        /// <summary>
        /// 添加到生成机器码的字符串的后缀
        /// </summary>
        private string _hardwareCodeSuffix;
        /// <summary>
        /// 注册表注册项：公司名称项
        /// </summary>
        private string _companyKey;
        /// <summary>
        /// 注册表注册项：软件名称项
        /// </summary>
        private string _appKey;
        /// <summary>
        /// 注册表注册项：许可授权项
        /// </summary>
        private const string LIC_KEY = "reg";
        /// <summary>
        /// 注册表值名称：机器码
        /// </summary>
        private const string MAC_KEY = "macCode";
        /// <summary>
        /// 注册表值名称：注册码
        /// </summary>
        private const string REG_KEY = "regCode";
        /// <summary>
        /// 注册表值名称：上次登录时间
        /// </summary>
        private const string LAST_TIME_KEY = "lastTime";
        /// <summary>
        /// 对授权开始时间加密时使用的常量
        /// </summary>
        private const long START_TIME_DIVISOR = 3L;
        /// <summary>
        /// 对授权结束时间加密时使用的常量
        /// </summary>
        private const long END_TIME_DIVISOR = 7L;
        /// <summary>
        /// 对记录上一次打开软件的系统时间加密时使用的常量
        /// </summary>
        private const long LAST_TIME_DIVISOR = 5L;


        /// <summary>
        /// 软件注册器
        /// <para>提供机器码、注册码的生成、验证、注册表写入等功能</para>
        /// <para>注册表位置：HKEY_CURRENT_USER\SOFTWARE\<paramref name="companyKey"/>\<paramref name="appKey"/>\reg</para>
        /// </summary>
        /// <param name="companyKey">注册表注册项：软件供应商名称项</param>
        /// <param name="appKey">注册表注册项：软件名称项</param>
        /// <param name="hardwareCodeSuffix">添加到生成机器码的字符串的后缀，若为null则使用<paramref name="companyKey"/>加上<paramref name="appKey"/>的值作为后缀</param>
        public SoftRegister(string companyKey, string appKey, string hardwareCodeSuffix = null)
        {
            if (string.IsNullOrEmpty(companyKey)) throw new ArgumentException($"参数{nameof(companyKey)}（软件供应商名称项）不能为null或Empty");
            if (string.IsNullOrEmpty(appKey)) throw new ArgumentException($"参数{nameof(appKey)}（软件名称项）不能为null或Empty");
            this._companyKey = companyKey;
            this._appKey = appKey;
            this._hardwareCodeSuffix = string.IsNullOrWhiteSpace(hardwareCodeSuffix) ? companyKey + appKey : hardwareCodeSuffix;
        }
        /// <summary>
        /// 软件注册器
        /// <para>提供机器码、注册码的生成、验证、注册表写入等功能</para>
        /// </summary>
        /// <param name="appInfo">需要注册授权的应用程序</param>
        /// <param name="hardwareCodeSuffix">添加到生成机器码的字符串的后缀，若为null则使用<paramref name="appInfo"/>.Company加上<paramref name="appInfo"/>.Key的值作为后缀</param>
        public SoftRegister(AppInfo appInfo, string hardwareCodeSuffix = null)
        {
            if (string.IsNullOrEmpty(appInfo.Company) || string.IsNullOrEmpty(appInfo.Key))
                throw new ArgumentException($"参数{nameof(appInfo)}中，{nameof(appInfo.Company)}、{nameof(appInfo.Key)}不能为null或Empty");
            this._companyKey = appInfo.Company;
            this._appKey = appInfo.Key;
            this._hardwareCodeSuffix = string.IsNullOrWhiteSpace(hardwareCodeSuffix) ? appInfo.Company + appInfo.Key : hardwareCodeSuffix;
        }


        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="successAction">验证授权通过的操作，例如打开主窗体、进入某项功能</param>
        /// <param name="failAction">验证授权失败的操作，例如退出程序, string参数表示授权失败的信息</param>
        public void CheckRegister(Action successAction, Action<string> failAction) => InnerCheckRegister(successAction, failAction);
        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <returns></returns>
        public string CreateHardwareCode() => InnerCreateHardwareCode();
        /// <summary>
        ///  在注册表中创建项，写入机器码、注册码
        /// </summary>
        /// <param name="machineCode">机器码</param>
        /// <param name="inputRegCode">注册码</param>
        public void WriteToRegistry(string machineCode, string inputRegCode) => InnerWriteToRegistry(machineCode, inputRegCode);
        /// <summary>
        /// 验证注册码，判断软件是否注册且在授权时段内
        /// (返回值：0-符合授权，1-授权验证码错误， 2-超出授权时段)
        /// </summary>
        /// <param name="hardwareCode">机器码</param>
        /// <param name="inputRegCode">注册码</param>
        /// <returns>返回值：0-符合授权，1-授权验证码错误， 2-超出授权时段</returns>
        public int CheckRegistered(string hardwareCode, string inputRegCode) => InnerCheckRegistered(hardwareCode, inputRegCode);
        /// <summary>
        /// 生成包括授权时段的注册码
        /// </summary>
        /// <param name="hardwareCode">机器码，根据此机器码生成注册码</param>
        /// <param name="startTime">授权开始时间</param>
        /// <param name="endTime">授权结束时间</param>
        /// <returns></returns>
        public string CreateRegCode(string hardwareCode, DateTime startTime, DateTime endTime) => InnerCreateRegCode(hardwareCode, startTime, endTime);


        #region 内部方法

        #region 生成机器码
        /// <summary>
        /// 生成完整的动态的机器码
        /// </summary>
        /// <returns></returns>
        private string InnerCreateHardwareCode()
        {
            CreateCoreAndBaseCode(out var coreCode, out var baseCode);

            string timeCode = DateTime.Now.Ticks.ToString();
            timeCode += timeCode;
            string tBaseCode = string.Empty;
            for (int i = 0; i < baseCode.Length; i++)
                tBaseCode += i % 2 == 0 ? timeCode[i] : baseCode[i];

            return coreCode + tBaseCode;
        }
        /// <summary>
        /// 创建核心（CPU+硬盘）机器码和主板机器码
        /// </summary>
        /// <param name="coreCode">核心（CPU+硬盘）机器码</param>
        /// <param name="baseCode">主板机器码</param>
        private void CreateCoreAndBaseCode(out string coreCode, out string baseCode)
        {
            string cpuId = Hardware.CpuId;
            string diskSNumber = Hardware.GetDiskVolumeSerialNumber();//硬盘卷标号
            string baseSNumber = Hardware.BaseBoardSerialNumber;//主板序列号
            if (string.IsNullOrWhiteSpace(baseSNumber))
                baseSNumber = diskSNumber + cpuId;
            baseSNumber = baseSNumber.PadLeft(16, 'C').PadRight(24, 'F');

            coreCode = CreatePartHardwareCode(cpuId + diskSNumber);
            baseCode = CreatePartHardwareCode(baseSNumber);
        }
        /// <summary>
        /// 根据指定的设备序列号(或ID)生成部分机器码
        /// </summary>
        /// <param name="serialNumber">设备序列号</param>
        /// <returns></returns>
        private string CreatePartHardwareCode(string serialNumber)
        {
            string[] strid = new string[24];
            for (int i = 0; i < 24; i++)//把字符赋给数组
                strid[i] = serialNumber.Substring(i, 1);

            string temp = string.Empty;
            for (int i = 0; i < 24; i++)//从数组抽取24个字符组成新的字符生成机器
                temp += strid[i + 3 >= 24 ? 0 : i + 3];
            temp += _hardwareCodeSuffix;
            return GetMd5(temp);
        }
        /// <summary>
        /// 获取Md5加密串
        /// </summary>
        /// <param name="text">要加密的文本</param>
        /// <returns></returns>
        private string GetMd5(string text)
        {
            byte[] byteResult = null;
            try
            {
                MD5CryptoServiceProvider MD5Pro = new MD5CryptoServiceProvider();
                byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(text);
                byteResult = MD5Pro.ComputeHash(buffer);
            }
            catch
            {
                return string.Empty;
            }
            return BitConverter.ToString(byteResult).Replace("-", "");
        }
        /// <summary>
        /// 获取软件在注册表中的注册信息项
        /// <para>HKEY_CURRENT_USER\SOFTWARE\_companyKey\_appKey\reg</para>
        /// </summary>
        /// <returns></returns>
        private RegistryKey GetRegisteKey()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).
               CreateSubKey(_companyKey).CreateSubKey(_appKey).CreateSubKey(LIC_KEY);
            return regkey;
        }
        #endregion


        #region 注册(授权)
        /// <summary>
        /// 创建密钥
        /// </summary>
        private int[] SetKeys()
        {
            int[] keys = new int[127];   //存储密钥
            for (int i = 1; i < keys.Length; i++)
                keys[i] = (i + i % 2) % 9;
            return keys;
        }
        /// <summary>
        /// 生成核心部分的注册码
        /// </summary>
        /// <param name="hardwareCode"></param>
        /// <returns></returns>
        private string CreatePartRegCode(string hardwareCode)
        {
            int[] keys = SetKeys();          //创建密钥
            char[] charCode = new char[48];  //存储ASCII码
            int[] intNumber = new int[48];   //存储ASCII码值

            for (int i = 1; i < charCode.Length; i++)   //存储机器码
                charCode[i] = Convert.ToChar(hardwareCode.Substring(i - 1, 1));

            for (int j = 1; j < intNumber.Length; j++)  //改变ASCII码值
                intNumber[j] = Convert.ToInt32(charCode[j]) + keys[Convert.ToInt32(charCode[j])];

            string strAsciiName = "";   //注册码
            for (int k = 1; k < intNumber.Length; k++)  //生成注册码
            {

                if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k]
                    <= 90) || (intNumber[k] >= 97 && intNumber[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[k]).ToString();
                }
                else if (intNumber[k] > 122)  //判断如果大于z
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 10).ToString();
                }
                else
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 9).ToString();
                }
            }
            return strAsciiName;
        }
        /// <summary>
        /// 生成注册(授权)开始/结束时间编码串
        /// </summary>
        /// <returns></returns>
        private string CreateRegTime(DateTime dt, long addTicks)
        {
            long tmpTicks = dt.Ticks - 360126000000001761L;
            var strTicksArray = (tmpTicks * addTicks).ToString().ToCharArray();
            string[] upArray = strTicksArray.Select(v => Convert.ToInt32(v)).Select(v => Convert.ToChar((v + 21)).ToString()).ToArray();
            Random r = new Random();
            string result = upArray.Aggregate((a, b) => a + r.Next(9).ToString() + b);
            return result;
        }
        /// <summary>
        ///  在注册表中创建项，写入机器码、注册码
        /// </summary>
        /// <param name="machineCode">机器码</param>
        /// <param name="inputRegCode">注册码</param>
        private void InnerWriteToRegistry(string machineCode, string inputRegCode)
        {
            try
            {
                RegistryKey regkey = GetRegisteKey();
                regkey.SetValue(MAC_KEY, machineCode);//写入机器码
                regkey.SetValue(REG_KEY, inputRegCode);//写入注册码

                var curTimeString = CreateRegTime(DateTime.Now, LAST_TIME_DIVISOR);
                regkey.SetValue(LAST_TIME_KEY, curTimeString);//写入注册时间段
                regkey.Close();
            }
            catch (Exception ex) { throw new Exception(string.Format("读注册表错误{0}" + ex.Message)); }
        }
        /// <summary>
        /// 生成包括授权时段的注册码
        /// </summary>
        /// <param name="hardwareCode">机器码，根据此机器码生成注册码</param>
        /// <param name="startTime">授权开始时间</param>
        /// <param name="endTime">授权结束时间</param>
        /// <returns></returns>
        private string InnerCreateRegCode(string hardwareCode, DateTime startTime, DateTime endTime)
        {
            var regCode = CreatePartRegCode(hardwareCode);
            var regStartTime = CreateRegTime(startTime, START_TIME_DIVISOR);
            var regEndTime = CreateRegTime(endTime, END_TIME_DIVISOR);
            return string.Format("{0}-{1}-{2}", regCode, regStartTime, regEndTime);
        }
        #endregion


        #region 检验注册(授权)
        /// <summary>
        /// 获取注册(授权)开始/截止时间
        /// </summary>
        /// <param name="regTimeCode">注册开始/截止时间编码串</param>
        /// <returns></returns>
        private DateTime GetRegTime(string regTimeCode, long addTicks)
        {
            try
            {
                if (string.IsNullOrEmpty(regTimeCode))
                    return new DateTime(0);

                var regChars = regTimeCode.Where(v => (int)v > 64 && (int)v < 91).ToArray();
                string strTime = regChars.Select(v => Convert.ToInt32(v)).
                    Select(v => Convert.ToChar((v - 21)).ToString()).Aggregate((a, b) => a + b);
                long tmpTicks = Convert.ToInt64(strTime) / addTicks;
                long ticks = tmpTicks + 360126000000001761L;
                return new DateTime(ticks);
            }
            catch
            {
                return new DateTime(0);
            }
        }
        /// <summary>
        /// 读注册表，判断软件是否注册(授权)
        /// （返回值：0-符合授权，1-授权验证码错误， 2-超出授权时段，3-非法更改系统时间导致不符合授权）
        /// </summary>
        /// <param name="appKey">程序注册表项</param>
        /// <param name="licKey">程序设置授权信息的注册表项</param>
        /// <returns></returns>
        private int CheckRegisteredByRegistry()
        {
            int result = 1;
            RegistryKey regkey = null;
            try
            {
                regkey = GetRegisteKey();

                string regCode = regkey.GetValue(REG_KEY, "").ToString();
                string lastTimeCode = regkey.GetValue(LAST_TIME_KEY, "").ToString();
                if (string.IsNullOrEmpty(lastTimeCode))
                    throw new Exception("请重新授权！");

                string hardwareCode = GetHardwareCodeFromRegisteKey();
                if (string.IsNullOrEmpty(hardwareCode))
                    result = 1;
                else
                    result = InnerCheckRegistered(hardwareCode, regCode);

                if (result == 0)//符合授权时，还需再判断系统时间有无被修改
                {
                    if (!CompareLastTime(lastTimeCode))
                    {
                        result = 3;//0-符合授权，1-授权验证码错误， 2-超出授权时段，3-非法更改系统时间导致不符合授权
                    }
                    else
                    {
                        lastTimeCode = CreateRegTime(DateTime.Now, LAST_TIME_DIVISOR);//将当期时间保存到注册表
                        regkey.SetValue(LAST_TIME_KEY, lastTimeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读注册表失败：" + ex.Message);
            }
            finally
            {
                if (regkey != null)
                    regkey.Close();
            }
            return result;
        }
        /// <summary>
        /// 读注册表获取机器码，若找不到则返回null
        /// </summary>
        /// <returns></returns>
        private string GetHardwareCodeFromRegisteKey()
        {
            string hardwareCode = null;
            try
            {
                RegistryKey regkey = GetRegisteKey();
                hardwareCode = regkey.GetValue(MAC_KEY, "").ToString();
            }
            catch { }
            return hardwareCode;
        }
        /// <summary>
        /// 对比软件上次登录时间与当前时间，当前时间比上次登录时间早，表示时间正常True，否则时间异常False
        /// </summary>
        /// <returns></returns>
        private bool CompareLastTime(string lastTimeCode)
        {
            var regTime = GetRegTime(lastTimeCode, LAST_TIME_DIVISOR);
            var now = DateTime.Now;
            return DateTime.Compare(regTime, now) < 0;
        }
        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="successAction">验证授权通过的操作，例如打开主窗体、进入某项功能</param>
        /// <param name="failAction">验证授权失败的操作，例如退出程序, string参数表示授权失败的信息</param>
        private void InnerCheckRegister(Action successAction, Action<string> failAction)
        {
            var msg = string.Empty;
            int registeState = 100;
            try
            {
                registeState = CheckRegisteredByRegistry();
            }
            catch (Exception ex) { msg = ex.Message; }
            if (registeState == 0)
                successAction?.Invoke();
            else
            {
                switch (registeState)
                {
                    case 1: msg = "软件未授权"; break;
                    case 2: msg = "授权过期"; break;
                    case 3: msg = "授权过期！系统时间被更改！"; break;
                }
                failAction?.Invoke(msg);
            }
        }
        /// <summary>
        /// 验证注册码，判断软件是否注册且在授权时段内
        /// (返回值：0-符合授权，1-授权验证码错误， 2-超出授权时段)
        /// </summary>
        /// <param name="hardwareCode">机器码</param>
        /// <param name="inputRegCode">注册码</param>
        /// <returns>返回值：0-符合授权，1-授权验证码错误， 2-超出授权时段</returns>
        private int InnerCheckRegistered(string hardwareCode, string inputRegCode)
        {
            if (!CheckHardwareCode(hardwareCode))//验证指定的机器码是否为本机的机器码
                return 1;

            var regArray = inputRegCode.Split('-');
            var regCodePart = regArray[0];
            var regStartTime = regArray[1];
            var regEndTime = regArray[2];

            var realRegCode = CreatePartRegCode(hardwareCode);//实际注册码
            var startTime = GetRegTime(regStartTime, START_TIME_DIVISOR);   //授权开始时间
            var endTime = GetRegTime(regEndTime, END_TIME_DIVISOR); //授权截止时间

            var now = DateTime.Now;
            int m = DateTime.Compare(endTime, now);
            int m2 = DateTime.Compare(startTime, now);

            //0-符合授权，1-授权验证码错误， 2-超出授权时段
            if (m > 0 && m2 < 0)
                return regCodePart == realRegCode ? 0 : 1;
            else
                return 2;
        }
        /// <summary>
        /// 验证指定的机器码是否为本机的机器码
        /// </summary>
        /// <param name="hardwareCode"></param>
        /// <returns></returns>
        private bool CheckHardwareCode(string hardwareCode)
        {
            CreateCoreAndBaseCode(out var coreCode, out var baseCode);
            hardwareCode = hardwareCode.Replace(coreCode, "");

            for (int i = 1; i < hardwareCode.Length; i = i + 2)
            {
                if (hardwareCode[i] != baseCode[i])
                    return false;
            }
            return true;
        }
        #endregion

        #endregion
    }
}
