## JohnSun.PinYinWuBi

一个获取汉字的拼音、五笔等信息的帮助类库，可以获取汉字的拼音/五笔/仓颉码/郑码/笔顺，同时可也可以用于输出汉字的拼音简码或五笔简码。

## 测试用例

```csharp
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
Console.WriteLine("==============获取五笔首码信息==============");
{
    Console.WriteLine(PinYinWuBiHelper.GetWuBiFirstCode(text));
}
```

## Licence
[MIT License (MIT)](./LICENSE)
