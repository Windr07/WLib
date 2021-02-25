using System.Security.Cryptography;
using System.Text;

namespace WLib.Data
{
    /// <summary>
    /// 加密解密（MD5加密等）
    /// </summary>
    public static class Encrypt
    {
        /// <summary>
        /// 获取MD5加密串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5Hash(this string str)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str)); //将输入字符串转换为字节数组并计算哈希。
                //X为     十六进制 X都是大写 x都为小写
                //2为 每次都是两位数
                //假设有两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，可以指定"X2"，这样显示出来就是：0x0A、0x1A。 
                //遍历哈希数据的每个字节
                //并将每个字符串格式化为十六进制字符串。
                int length = data.Length;
                for (int i = 0; i < length; i++)
                    sb.Append(data[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="str"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMD5Hash(this string str, string hash)
        {
            string hashOfInput = GetMD5Hash(str);
            return hashOfInput.CompareTo(hash) == 0;
        }
    }
}
