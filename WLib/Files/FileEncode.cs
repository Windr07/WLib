/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2018
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;

namespace WLib.Files
{
    /// <summary>
    /// 判断文件的文本编码
    /// </summary>
    public class FileEncode
    {
        /// <summary>   
        /// 取得一个文本文件的编码方式
        /// </summary>   
        /// <param name="filePath">文件名</param>   
        /// <returns></returns>   
        public static Encoding GetEncoding(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Encoding targetEncoding = GetEncoding(fileStream);
            fileStream.Close();
            return targetEncoding;
        }
        /// <summary>   
        /// 通过给定的文件流，判断文件的编码类型   
        /// </summary>   
        /// <param name="stream">文件流</param>   
        /// <returns>文件的编码类型</returns>   
        public static Encoding GetEncoding(Stream stream)
        {
            byte[] unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] unicodeBig = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] utf8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM   
            Encoding encoding = Encoding.Default;

            BinaryReader reander = new BinaryReader(stream, System.Text.Encoding.Default);
            byte[] ss = reander.ReadBytes(4);
            if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                encoding = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                encoding = Encoding.Unicode;
            }
            else
            {
                if (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    int.TryParse(stream.Length.ToString(), out var i);
                    ss = reander.ReadBytes(i);

                    if (IsUtf8Bytes(ss))
                        encoding = Encoding.UTF8;
                }
            }
            reander.Close();
            return encoding;

        }
        /// <summary>   
        /// 判断是否是不带 BOM 的 UTF8 格式   
        /// </summary>   
        /// <param name="data"></param>   
        /// <returns></returns>   
        private static bool IsUtf8Bytes(byte[] data)
        {
            int charByteCounter = 1;　 //计算当前正分析的字符应还有的字节数   
            byte curByte; //当前分析的字节.   
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前   
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　   
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1   
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式，不能识别文本的字符串编码！");
            }
            return true;
        }
    }
}
