using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {

        //Test1();
        //Test2();
        //Test3();
        Test4();
    }

    static void Test1()
    {
        string path = "Test.txt";
        string content = "Hi!";

        File.WriteAllText(path, content);

        Console.WriteLine("文件已创建！");
        // 打印文件路径
        Console.WriteLine("文件位置：" + Path.GetFullPath(path));

        if (File.Exists(path))
        {
            string readContent = File.ReadAllText(path);
            Console.WriteLine("文件内容：" + readContent);
        }
        else
        {
            Console.WriteLine("文件未找到！");
        }
        //解决方案不显示创建的文件
        File.WriteAllText(@"E:\vs2022\repos\Test@\Test@\Tests.txt", string.Empty);



        //路径拼接   系统函数获取文件夹实际路径   枚举值，代表桌面文件夹
        string path1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test.txt");
    }

    static void Test2()
    {
        string path = "output.txt";

        // 第二个参数为 true 表示追加内容，false 表示覆盖
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine("第一行文字");
            writer.WriteLine("第二行文字");
            writer.WriteLine("写入时间：" + DateTime.Now);
        }

        Console.WriteLine("写入完成！");
        Console.WriteLine(File.ReadAllText(path));

        var lines = File.ReadAllLines(path).ToList();
        lines.RemoveAt(1); // 删除第二行并删除回车换行符
        File.WriteAllLines(path, lines);
    }
    static void Test3()
    {
        // 1. 确定文件路径（这里放在桌面）
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "DataDemo.txt"
        );

        // 2. 要写入的内容
        string[] lines = {
            "张三,18,男",
            "李四,20,女",
            "王五,19,男"
        };

        // 3. 写入文件（覆盖写入）
        File.WriteAllLines(path, lines);
        Console.WriteLine("数据已写入文件：" + path);

        // 4. 读取文件
        string[] readLines = File.ReadAllLines(path);

        // 5. 输出到控制台（识别并分列显示）
        Console.WriteLine("\n从文件读取的数据：");
        Console.WriteLine("姓名\t年龄\t性别");
        Console.WriteLine("-------------------");
        foreach (string line in readLines)
        {
            // 按逗号分割
            string[] parts = line.Split(',');
            if (parts.Length == readLines.Length)//3
            {
                Console.WriteLine($"{parts[0]}\t{parts[1]}\t{parts[2]}");
            }
        }
    }
    static void Test4()
    {
        string path = "People.txt";
        Console.WriteLine("欢迎使用登记查询修改系统！");
        Console.WriteLine("请选择操作：1.登记 2.查询 3.修改 4.查看名单 5.退出");

        if(int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    Test4_1(path);
                    break;
                case 2:
                    Test4_2(path);
                    break;
                case 3:
                    Test4_3(path);
                    break;
                case 4:
                    Test4_4(path);
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("无效的选择。");
                    Test4();
                    break;
            }
        }
        else
        {
            Console.WriteLine("请输入有效的数字选择。");
            Test4();
        }
    }
    static void Test4_1(string path)
    {
        Console.WriteLine("---登记---");
        Console.WriteLine("请输入姓名：");
        string name = Console.ReadLine();
        var lines = File.ReadAllLines(path).ToList() ;
        if(lines.Any(e => e.StartsWith(name + ",")))
        {
            Console.WriteLine("该姓名已存在，无法重复登记。");
            Test4();
            return;
        }

        Console.WriteLine("请输入年龄：");

        if(int.TryParse(Console.ReadLine(), out int age))
        {
            if (age < 0 || age > 1200)
            {
                Console.WriteLine("年龄输入无效，请输入0-1200之间的数字。");
                Test4();
                return;
            }
        }  
        else
        {
            Console.WriteLine("请输入有效的数字年龄。");
            Test4();
            return;
        }
        //string age = Console.ReadLine();
        Console.WriteLine("请输入地址：");
        string address = Console.ReadLine();

        Console.WriteLine("确认信息无误吗？1.确认 2.取消");
        if(int.TryParse(Console.ReadLine(), out int confirm))
        {
            if (confirm != 1)
            {
                Console.WriteLine("登记已取消。");
                Test4();
                return;
            }
        }
        else
        {
            Console.WriteLine("输入无效，登记已取消。");
            Test4();
            return;
        }
        string entry = $"{name},{age},{address}";
        File.AppendAllText(path, entry + Environment.NewLine);//Environment.NewLine不同系统使用正确的换行符
        Console.WriteLine("信息已保存到文件。");
        Test4();
    }

    static void Test4_2(string Path)
    {
        string path = Path;
        var lines = File.ReadAllLines(path).ToList();

        Console.WriteLine("方法查询: 1.ID 2.名称 3.返回");
        if(int.TryParse(Console.ReadLine() ,out int ee)){
            switch (ee)
            {
                case 1:
                    Console.WriteLine("输入查询人ID:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        if (id >= 1 && id < lines.Count+1)
                        {
                            Console.WriteLine("查有此人");
                            Console.WriteLine("Info: " + lines[id-1]);
                            Test4_2(path); return;
                        }
                        else
                        {
                            Console.WriteLine("无此人ID");
                            Test4_2(path);
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("请输入有效的数字ID");
                        Test4_2(path);

                    }
                    break;
                case 2:
                    Console.WriteLine("输入查询人名称:");
                    string FindName = Console.ReadLine();
                    if (lines.Any(e => e.StartsWith(FindName + ",")))
                    {
                        Console.WriteLine("查有此人");
                        //Console.WriteLine("Id: " + lines.FindIndex(e => e.StartsWith(FindName + ",")));
                        Console.WriteLine("Info: " + lines.Find(e => e.StartsWith(FindName + ",")));
                        Test4_2(path); return;
                    }
                    else
                    {
                        Console.WriteLine("无此人");
                        Console.WriteLine("是否去登陆？1.继续查询 2.去登陆");

                        if (int.TryParse(Console.ReadLine(), out int choice))
                        {
                            switch (choice)
                            {
                                case 1:
                                    Test4_2(path);


                                    break;
                                case 2:
                                    string name = FindName;
                                    Console.WriteLine("请输入年龄：");
                                    if (int.TryParse(Console.ReadLine(), out int age))
                                    {
                                        if (age < 0 || age > 1200)
                                        {
                                            Console.WriteLine("年龄输入无效，请输入0-1200之间的数字。");
                                            Test4();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("请输入有效的数字年龄。");
                                        Test4();
                                        return;
                                    }
                                    Console.WriteLine("请输入地址：");
                                    string address = Console.ReadLine();

                                    string entry = $"{name},{age},{address}";
                                    File.AppendAllText(path, entry + Environment.NewLine);
                                    Console.WriteLine("信息已保存到文件");
                                    Test4();
                                    break;
                                default:
                                    Console.WriteLine("无效的选择");
                                    Test4_2(path);

                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("请输入有效的数字选择");
                            Test4_2(path);

                        }
                    }
                    break;
                case 3:
                    Test4();
                    break;
                default:
                    Console.WriteLine("无效的选择");
                    Test4_2(path);

                    break;
            }
        }
        else
        {
            Console.WriteLine("请输入有效的数字选择");
            Test4_2(path);

        }
    }
    static void Test4_3(string Path)
    {
        string path = Path;
        var lines = File.ReadAllLines(path).ToList();

        Console.WriteLine("- - -修改- - -");
        Console.WriteLine("通过 - 1.ID 2.名称 3.返回 - 查询" );

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("输入修改人ID:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        if (id >= 1 && id < lines.Count+1)
                        {
                            Console.WriteLine("查有此人");
                            Console.WriteLine("当前Info: " + lines[id-1]);
                            Test4_3_1(id-1, path);
                            Test4();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("无此人ID");
                            Test4_3(path);
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("请输入有效的数字ID。");
                    }
                    break;
                case 2:
                    Console.WriteLine("输入修改人名称:");
                    string FindName = Console.ReadLine();
                    int index = lines.FindIndex(e => e.StartsWith(FindName + ","));
                    if (index == -1)
                    {
                        Console.WriteLine("无此人");
                        Test4_3(path);

                    }
                    else
                    {
                        Console.WriteLine("有此人");
                        Test4_3_1(index, path);
                        Test4();
                        return;
                    }
                    break;
                case 3:
                    Test4();
                    break;
                default:
                    Console.WriteLine("无效的选择。");
                    Test4_3(path);
                    return;
            }
        }
        else
        {
            Console.WriteLine("请输入有效的数字ID。");
        }

    }
    static void Test4_3_1(int index, string Path)
    {
        string path = Path;
        var lines = File.ReadAllLines(path).ToList();


        Console.WriteLine("请输入新的姓名：");
        string name = Console.ReadLine();
        if (lines.Any(e => e.StartsWith(name + ",")))
        {
            Console.WriteLine("该姓名已存在，无法修改为此姓名。");
            Test4_3(path);
            return;
        }
        Console.WriteLine("请输入新的年龄：");
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            if (age < 0 || age > 1200)
            {
                Console.WriteLine("年龄输入无效，请输入0-1200之间的数字。");
                Test4();
                return;
            }
        }
        else
        {
            Console.WriteLine("请输入有效的数字年龄。");
            Test4();
            return;
        }
        Console.WriteLine("请输入新的地址：");
        string address = Console.ReadLine();
        string entry = $"{name},{age},{address}";
        lines[index] = entry;
        File.WriteAllLines(path, lines);
        Console.WriteLine("信息已更新。");
    }

    static void Test4_4(string Path)
    {
        string path = Path;
        string[] lines = File.ReadAllLines(path);
        Console.WriteLine("人员列表：");
        Console.WriteLine("姓名\t年龄\t地址");
        Console.WriteLine("-----------------------");
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            Console.WriteLine($"{parts[0]}\t{parts[1]}\t{parts[2]}");
        }
        Test4();
    }
}