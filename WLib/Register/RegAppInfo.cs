/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace WLib.Register
{
    /// <summary>
    /// 软件注册授权信息
    /// </summary>
    public class RegAppInfo : AppInfo
    {
        /// <summary>
        /// 机器码
        /// </summary>
        [Description("机器码")]
        public string MachineCode { get; set; }
        /// <summary>
        /// 注册码
        /// </summary>
        [Description("注册码")]
        public string RegCode { get; set; }
        /// <summary>
        /// 注册开始时间
        /// </summary>
        [Description("注册开始时间")]
        public string RegStartTime { get; set; }
        /// <summary>
        /// 注册截止时间
        /// </summary>
        [Description("注册截止时间")]
        public string RegEndTime { get; set; }


        /// <summary>
        /// 软件注册授权信息
        /// </summary>
        public RegAppInfo() { }
        /// <summary>
        /// 软件注册授权信息
        /// </summary>
        /// <param name="name">授权的软件名</param>
        /// <param name="id">授权的软件ID</param>
        /// <param name="version">授权的软件版本</param>
        /// <param name="machineCode">机器码</param>
        /// <param name="regCode">注册码</param>
        /// <param name="regStartTime">注册开始时间</param>
        /// <param name="regEndTime">注册截止时间</param>
        /// <param name="comment">备注信息</param>
        public RegAppInfo(string name, string id, string version, string company, string machineCode, string regCode, string regStartTime, string regEndTime, string comment)
        {
            this.Key = this.Name = name;
            this.Id = id;
            this.Company = company;
            this.Version = version;
            this.MachineCode = machineCode;
            this.RegCode = regCode;
            this.RegStartTime = regStartTime;
            this.RegEndTime = regEndTime;
            this.Comment = comment;
        }
        /// <summary>
        /// 软件注册授权信息
        /// </summary>
        /// <param name="name">授权的软件名</param>
        /// <param name="id">授权的软件ID</param>
        /// <param name="version">授权的软件版本</param>
        /// <param name="machineCode">机器码</param>
        /// <param name="regCode">注册码</param>
        /// <param name="regStartTime">注册开始时间</param>
        /// <param name="regEndTime">注册截止时间</param>
        /// <param name="comment">备注信息</param>
        public RegAppInfo(string name, string id, string version, string company, string machineCode, string regCode, DateTime regStartTime, DateTime regEndTime, string comment)
        {
            this.Key = this.Name = name;
            this.Id = id;
            this.Version = version;
            this.Company = company;
            this.MachineCode = machineCode;
            this.RegCode = regCode;
            this.RegStartTime = regStartTime.ToString();
            this.RegEndTime = regEndTime.ToString();
            this.Comment = comment;
        }


        /// <summary>
        /// 软件注册授权信息写入bat文件
        /// </summary>
        /// <param name="filePath">bat文件路径</param>
        public void WriteToFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            var binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Seek(0, SeekOrigin.End);

            binaryWriter.Write(Name);
            binaryWriter.Write(Id);
            binaryWriter.Write(Key);
            binaryWriter.Write(Version);
            binaryWriter.Write(Company);
            binaryWriter.Write(MachineCode);
            binaryWriter.Write(RegCode);
            binaryWriter.Write(RegStartTime);
            binaryWriter.Write(RegEndTime);
            binaryWriter.Write(Comment);

            binaryWriter.Flush();
            binaryWriter.Close();
            fileStream.Close();
        }
        /// <summary>
        /// 从bat文件读取软件注册授权信息
        /// </summary>
        /// <param name="filePath">bat文件路径</param>
        /// <returns></returns>
        public static RegAppInfo[] ReadFromFile(string filePath)
        {
            var regInfos = new List<RegAppInfo>();
            var fs = new FileStream(filePath, FileMode.Open);
            var br = new BinaryReader(fs);
            try
            {
                var length = br.BaseStream.Length;
                while (br.BaseStream.Position < length)
                {
                    regInfos.Add(new RegAppInfo
                    {
                        Name = br.ReadString(),
                        Id = br.ReadString(),
                        Key = br.ReadString(),
                        Version = br.ReadString(),
                        Company = br.ReadString(),
                        MachineCode = br.ReadString(),
                        RegCode = br.ReadString(),
                        RegStartTime = br.ReadString(),
                        RegEndTime = br.ReadString(),
                        Comment = br.ReadString()
                    });
                }
            }
            catch (EndOfStreamException ex) { throw ex; }
            finally
            {
                br.Close();
                fs.Close();
            }
            return regInfos.ToArray();
        }
    }
}
