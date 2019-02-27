using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JohnSun.PinYinWuBi
{
    /// <summary>
    /// 获取拼音五笔工具类
    /// </summary>
    public class PinYinWuBiHelper
    {
        private static Dictionary<int, string[]> _data { get; } = ReadData();

        private static Dictionary<int, string[]> ReadData()
        {
            Dictionary<int, string[]> dict = new Dictionary<int, string[]>();
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("JohnSun.PinYinWuBi.DATA"))
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                string[] lines = Encoding.UTF8.GetString(bytes).Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split('|');
                    dict[int.Parse(temp[0])] = temp.ToList().GetRange(1, temp.Length - 1).ToArray();
                }
            }
            return dict;
        }

        /// <summary>
        /// 获取指定字符的信息，返回一个数组：
        /// 索引0：五笔
        /// 索引1：仓颉
        /// 索引2：郑码
        /// 索引3：笔顺
        /// 索引4+：拼音
        /// </summary>
        /// <param name="input">包含字符或代理项对的字符串。</param>
        /// <param name="index">字符或代理项对在 s 中的索引位置。</param>
        /// <returns>指定字符的信息</returns>
        public static string[] GetCharInfo(string input, int index)
        {
            int key = char.ConvertToUtf32(input, index);
            if (_data.ContainsKey(key))
                return _data[key];
            return null;
        }

        /// <summary>
        /// 获取字符串拼音首码
        /// </summary>
        /// <param name="input">输入内容</param>
        /// <returns>所有可能的拼音首码</returns>
        public static string[] GetAllPinYinFirstCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new string[] { "" };
            StringBuilder builder = new StringBuilder();

            List<StringBuilder> builders = new List<StringBuilder>() { new StringBuilder() };

            for (int i = 0; i < input.Length; i++)
            {
                int key = char.ConvertToUtf32(input, i);
                if (key >= 48 && key <= 57 || key >= 65 && key <= 90)
                {
                    foreach (StringBuilder item in builders)
                    {
                        item.Append(input.Substring(i, 1));
                    }
                }
                else if (key >= 97 && key <= 122)
                {
                    foreach (StringBuilder item in builders)
                    {
                        item.Append(input.Substring(i, 1).ToUpper());
                    }
                }
                else
                {
                    string[] charInfo = GetCharInfo(input, i);
                    if (charInfo != null)
                    {
                        List<string> temp = new List<string>();
                        for (int j = 4; j < charInfo.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(charInfo[j]) && !temp.Contains(charInfo[j].Substring(0, 1).ToUpper()))
                            {
                                temp.Add(charInfo[j].Substring(0, 1).ToUpper());
                            }
                        }
                        if (temp.Count > 0)
                        {
                            List<StringBuilder> tempBuilders = new List<StringBuilder>();
                            for (int j = 0; j < builders.Count; j++)
                            {
                                for (int k = 0; k < temp.Count; k++)
                                {
                                    tempBuilders.Add(new StringBuilder(builders[j].ToString()).Append(temp[k]));
                                }
                            }
                            builders = tempBuilders;
                        }
                    }
                }
            }

            List<string> pinyins = new List<string>();
            builders.ForEach(b => pinyins.Add(b.ToString()));
            return pinyins.Distinct().ToArray();
        }

        /// <summary>
        /// 获取字符串拼音首码
        /// </summary>
        /// <param name="input">输入内容</param>
        /// <returns>获取一个拼音首码</returns>
        public static string GetPinYinFirstCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                int key = char.ConvertToUtf32(input, i);
                if (key >= 48 && key <= 57 || key >= 65 && key <= 90)
                {
                    builder.Append(input.Substring(i, 1));
                }
                else if (key >= 97 && key <= 122)
                {
                    builder.Append(input.Substring(i, 1).ToUpper());
                }
                else
                {
                    string[] charInfo = GetCharInfo(input, i);
                    if (charInfo != null && !string.IsNullOrEmpty(charInfo[4]))
                    {
                        builder.Append(charInfo[4].Substring(0, 1).ToUpper());
                    }
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 获取字符串的五笔首码
        /// </summary>
        /// <param name="input">输入内容</param>
        /// <returns>该内容的五笔首码</returns>
        public static string GetWuBiFirstCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                int key = char.ConvertToUtf32(input, i);
                if (key >= 48 && key <= 57 || key >= 65 && key <= 90)
                {
                    builder.Append(input.Substring(i, 1));
                }
                else if (key >= 97 && key <= 122)
                {
                    builder.Append(input.Substring(i, 1).ToUpper());
                }
                else
                {
                    string[] charInfo = GetCharInfo(input, i);
                    if (charInfo != null && !string.IsNullOrEmpty(charInfo[0]))
                    {
                        builder.Append(charInfo[0].Substring(0, 1).ToUpper());
                    }
                }
            }

            return builder.ToString();
        }
    }
}