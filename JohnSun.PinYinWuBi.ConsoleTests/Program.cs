using System;
using System.Linq;

namespace JohnSun.PinYinWuBi.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "南京市长江大桥(Nanjing Yangtze River Bridge)";
            Test(text);
            Console.ReadKey();
        }

        public static void Test(string text)
        {
            Console.WriteLine($"测试输出：{text}");
            Console.WriteLine("==============测试获取汉字信息==============");
            {
                for (int i = 0; i < text.Length; i++)
                {
                    string[] temp = PinYinWuBiHelper.GetCharInfo(text, i);
                    if (temp == null)
                    {
                        Console.WriteLine($"【{text.Substring(i, 1)}】非汉字或非帮助类收录汉字。");
                    }
                    else
                    {
                        Console.WriteLine($"【{text.Substring(i, 1)}】********\r\n五笔:{temp[0]}\r\n仓颉:{temp[1]}\r\n郑码:{temp[2]}\r\n笔顺:{temp[3].Replace("1", "一").Replace("2", "丨").Replace("3", "ノ").Replace("4", "丶").Replace("5", "フ")}\r\n拼音:{string.Join(";", temp.ToList().GetRange(4, temp.Length - 4))}");
                    }
                }
            }
            Console.WriteLine("==============获取拼音首码信息==============");
            {
                Console.WriteLine(PinYinWuBiHelper.GetPinYinFirstCode(text));
            }
            Console.WriteLine("==============获取全部拼音首码==============");
            {
                string[] temp = PinYinWuBiHelper.GetAllPinYinFirstCode(text);
                for (int i = 0; i < temp.Length; i++)
                {
                    Console.WriteLine(temp[i]);
                }
            }
            Console.WriteLine("==============获取全部五笔首码==============");
            {
                Console.WriteLine(PinYinWuBiHelper.GetWuBiFirstCode(text));
            }
        }
    }
}
