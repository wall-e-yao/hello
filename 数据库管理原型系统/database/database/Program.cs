using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace database
{
    class Program
    {
        public static String nowDirtionary = null;

        public static void Main(string[] args)
        {
            //User root = new User();
            //User zft = new User("", "");
            //new Enter().WriteToFile(root);
            //new Enter().WriteToFile(zft);

            Console.Write("请输入用户名：");
            string name = Console.ReadLine();
            Console.Write("请输入密码：");
            string pwd = Console.ReadLine();
            User current = null;
            while ((current = new Enter().CheckPassword(name, pwd)) == null)
            {
                Console.WriteLine("密码错误！");
                Console.Write("请输入用户名：");
                name = Console.ReadLine();
                Console.Write("请输入密码：");
                pwd = Console.ReadLine();
            }

            while (true)
            {
                //接受命令
                string comm = getCommand();
                //判断括号是否匹配
                if (isMatch(comm) == false)
                {
                    Console.WriteLine("括号不匹配！");
                    return;
                }

                List<string> Clist = commandSplit(comm, @"\s+");  //根据 空格 分割 -> 取第一个

                switch (Clist[0])
                {
                    case "create":
                        if (current.uername != "root")
                        {
                            Console.WriteLine("只有管理员可以create！");
                            break;
                        }
                        switch (Clist[1])
                        {
                            case "table": //create table a(name int,name int);
                                if (nowDirtionary != null)
                                {
                                    //
                                    int i = comm.IndexOf('(');
                                    int j = comm.LastIndexOf(')');
                                    String param = comm.Substring(i + 1, j - i - 1);
                                    string tableName = null;
                                    if (Clist[2].IndexOf('(') == -1)
                                        tableName = Clist[2];
                                    else
                                        tableName = Clist[2].Substring(0, Clist[2].IndexOf('('));

                                    new Create().createTable(tableName, param, nowDirtionary);
                                }
                                else
                                    Console.WriteLine("没有打开数据库");
                                break;
                            case "database": //  Create database zhang ;
                                new Create().createDatabase(Clist);
                                break;
                            case "index":
                                {
                                    if (nowDirtionary == null)
                                    {
                                        Console.WriteLine("没有打开数据库！");
                                        break;
                                    }
                                    comm = comm.Trim();
                                    comm = comm.Substring(0, comm.IndexOf(";"));
                                    List<string> sp_by_on = Program.commandSplit(comm, " on ");
                                    sp_by_on[1] = sp_by_on[1].Trim();
                                    string myTableName = sp_by_on[1].Substring(0, sp_by_on[1].IndexOf("(")).Trim();
                                    string line = sp_by_on[1].Substring(sp_by_on[1].IndexOf("(") + 1, sp_by_on[1].IndexOf(")") - sp_by_on[1].IndexOf("(") - 1);
                                    new Create().createIndex(Clist[2], myTableName, line);
                                }

                                break;
                            default:
                                Console.WriteLine("没有创建其他！");
                                break;
                        }
                        break;
                    case "use": //use zhangsan;
                        {
                            nowDirtionary = new Use().useDataBase(Clist);

                            if (current.uername == "root" && nowDirtionary != null)
                            {
                                List<OneTable> oTables = new Alter().ReadOneTablesFromDD();
                                foreach (var item in oTables)
                                {
                                    current.select.Add(item.name);
                                    current.insert.Add(item.name);
                                    current.delete.Add(item.name);
                                    current.update.Add(item.name);
                                }
                            }
                        }
                        break;
                    case "drop":   // drop database zhang;   drop table employee ;
                        if (current.uername != "root")
                        {
                            Console.WriteLine("只有管理员可以drop！");
                            break;
                        }
                        new Drop().delete(Clist);
                        break;
                    case "alter":
                        {
                            if (nowDirtionary == null)
                            {
                                Console.WriteLine("没有打开数据库！");
                                break;
                            }
                            if (current.uername != "root")
                            {
                                Console.WriteLine("只有管理员可以alert！");
                                break;
                            }
                            if (Clist[1] == "table")
                            {
                                if (Clist[3] == "add" || Clist[3].Contains("add("))
                                {
                                    try
                                    {
                                        int i = comm.IndexOf("(");
                                        int j = comm.LastIndexOf(")");
                                        new Alter().TableAdd(Clist[2], comm.Substring(i + 1, j - i - 1));
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("指令错误");
                                        break;
                                    }

                                }
                                else if (Clist[3] == "drop" && (Clist[4].Contains("column(") || Clist[4] == "column"))
                                {
                                    try
                                    {
                                        int i = comm.IndexOf("(");
                                        int j = comm.LastIndexOf(")");
                                        new Alter().TableDrop(Clist[2], comm.Substring(i + 1, j - i - 1));
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("指令错误");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ALTER指令错误！");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("无法识别{0}", Clist[1]);
                                break;
                            }
                        }
                        break;
                    case "insert":
                        {
                            if (nowDirtionary == null)
                            {
                                Console.WriteLine("没有打开数据库！");
                                break;
                            }
                            List<string> a = new List<string>();
                            a.Add(Clist[2].Trim());
                            if (!new Enter().CheckInsertLimit(current, a))
                            {
                                Console.WriteLine("您没有插入{0}表的权限！", a[0]);
                                break;
                            }
                            new Insert().insertInto(Clist);
                        }

                        break;
                    case "update":
                        {
                            if (nowDirtionary == null)
                            {
                                Console.WriteLine("没有打开数据库！");
                                break;
                            }
                            List<string> a = new List<string>();
                            a.Add(Clist[1].Trim());
                            if (!new Enter().CheckUpdateLimit(current, a))
                            {
                                Console.WriteLine("您没有更新{0}表的权限！", a[0]);
                                break;
                            }
                            new Update().upDateTable(comm);
                        }
                        break;
                    case "delete":
                        if (nowDirtionary == null)
                        {
                            Console.WriteLine("没有打开数据库！");
                            break;
                        }
                        new Delete().deleteMenu(comm, ref current);
                        break;
                    case "select":
                        if (nowDirtionary == null)
                        {
                            Console.WriteLine("没有打开数据库！");
                            break;
                        }
                        new Select().SelectMenu(comm, ref current);
                        break;
                    case "exit":
                        System.Environment.Exit(0);
                        break;
                    case "grant":
                        {
                            if (current.uername != "root")
                            {
                                Console.WriteLine("只有管理员可以grant！");
                                break;
                            }
                            new Grant().Empower(comm, ref current);
                        }
                        break;
                    case "revoke":
                        {
                            if (current.uername != "root")
                            {
                                Console.WriteLine("只有管理员可以revoke！");
                                break;
                            }
                            new Grant().Unpower(comm, ref current);
                        }
                        break;
                    case "show":
                        switch (Clist[1])
                        {
                            case "index":
                                {
                                    Stream stream1 = null;
                                    try
                                    {
                                        IFormatter formatter = new BinaryFormatter();
                                        stream1 = new FileStream(Program.nowDirtionary + @"\" + Clist[2] + ".txt", FileMode.Open,
                                        FileAccess.Read, FileShare.Read);
                                        Dictionary<string, long> keys = (Dictionary<string, long>)formatter.Deserialize(stream1);
                                        foreach (var item in keys)
                                        {
                                            Console.Write(item.Key + "------");
                                            Console.WriteLine(item.Value);
                                        }
                                        stream1.Close();
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("没有创建该索引！");
                                    }
                                }
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("请您查看{0}命令功能，，，可能还没有完善到！", Clist[0]);
                        break;
                }
            }
        }

        public static List<string> commandSplit(string command, string rule)
        {
            command = command.ToLower();
            string[] words = Regex.Split(command, rule);
            List<string> Clist = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                    Clist.Add(words[i]);
            }
            //foreach (string s in Clist)
            //{
            //    Console.WriteLine(s + "-");
            //}
            return Clist;
        }

        static string getCommand()
        {
            Console.Write(">");
            string s = Convert.ToString(Console.ReadLine());
            string command = s + " ";
            while (s == "" || s[s.Length - 1] != ';')
            {
                Console.Write("…");
                s = Convert.ToString(Console.ReadLine());
                command += s + " ";
            }
            return command;
        }

        public static bool isMatch(string command)
        {
            Stack stack = new Stack();
            foreach (char ch in command)
            {
                if (ch == '(')
                {
                    stack.Push(ch);
                }
                else if (ch == ')')
                {
                    stack.Pop();
                }
            }
            if (stack.Count == 0)
                return true;
            else
                return false;
        }

    } 
}