using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace database
{
    class Drop
    {
        String DataName = null;
        string tabName = null;
        public void delete(List<string> Clist)
        {
            switch (Clist[1])
            {
                case "table":
                    {

                        if (Clist[2].IndexOf(";") != -1)
                        {
                            tabName = Clist[2].Remove(Clist[2].IndexOf(";"));
                        }
                        else if (Clist[1].IndexOf(";") == -1 && Clist[3].Trim() == ";")
                        {
                            tabName = Clist[2];
                        }
                        else
                        {
                            Console.WriteLine("语法错误！");
                            return;
                        }

                        //   删除表文件

                        if (File.Exists(Program.nowDirtionary + @"\" + tabName + ".txt"))
                        {
                            File.Delete(Program.nowDirtionary + @"\" + tabName + ".txt");
                        }
                        else
                        {
                            Console.WriteLine("没有找到该表，确定一下表名或数据库名称！");
                            return;
                        }
                        new Create().createIndex("ssn_index", "employee", "ssn");

                        // 删除DD中的该表信息
                        // 1 将其他信息放入list中
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream1 = new FileStream(Program.nowDirtionary + @"\DataDirtionary.txt", FileMode.Open,
                        FileAccess.Read, FileShare.Read);
                        List<OneTable> list = new List<OneTable>();
                        try
                        {
                            OneTable obj = (OneTable)formatter.Deserialize(stream1);
                            while (obj != null)
                            {
                                if(obj.name!=tabName)
                                list.Add(obj);
                                obj = (OneTable)formatter.Deserialize(stream1);
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            stream1.Close();
                        }
                        //2 list中信息进行序列化入表文件

                        Stream stream = new FileStream(Program.nowDirtionary + @"\DataDirtionary.txt", FileMode.Truncate,
                        FileAccess.Write);
                        try
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                formatter.Serialize(stream, list[i]);
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            stream.Close();
                        }
                        Console.WriteLine("表文件: {0} drop 成功！",tabName);
                    }
                    break;
                //删除数据库本身 drop database zhang
                case "database":
                    {
                        if (Clist[2].IndexOf(";") != -1)
                        {
                            DataName = Clist[2].Remove(Clist[2].IndexOf(";"));
                        }
                        else if (Clist[1].IndexOf(";") == -1 && Clist[3].Trim() == ";")
                        {
                            DataName = Clist[2];
                        }
                        else
                        {
                            Console.WriteLine("语法错误！");
                            return;
                        }
                        try
                        {
                            Directory.SetCurrentDirectory(new Create().Path);
                            if (Directory.Exists(DataName))
                            {
                                Directory.Delete(new Create().Path + @"\" + DataName, true);
                                Console.WriteLine("已经删除{0}database", DataName);
                                return;
                            }
                            throw new Exception();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("没有找到 {0} database!", DataName);
                        }
                    }
                    break;
                case "index":
                    {
                        string indexName = "";
                        if (Clist[2].IndexOf(";") != -1)
                        {
                            indexName = Clist[2].Remove(Clist[2].IndexOf(";"));
                        }
                        else if (Clist[1].IndexOf(";") == -1 && Clist[3].Trim() == ";")
                        {
                            indexName = Clist[2];
                        }
                        else
                        {
                            Console.WriteLine("语法错误！");
                            return;
                        }
                        if (File.Exists(Program.nowDirtionary + @"\" + indexName + ".txt"))
                        {
                            File.Delete(Program.nowDirtionary + @"\" + indexName + ".txt");
                        }
                        else
                        {
                            Console.WriteLine("没有建立该索引或者已经删除！");
                            return;
                        }
                    }
                   
                    break;
                default:
                    Console.WriteLine("{0}语法错误！",Clist[1]);
                    return;
            }

        }
    }
}
