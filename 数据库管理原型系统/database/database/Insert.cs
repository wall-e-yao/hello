using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Dynamic;

namespace database
{
    class Insert
    {
        string tabName = null;
        public void insertInto(List<string> Clist)
        {
            try
            {
                if (Clist[1] == "into")
                {
                    tabName = Clist[2];

                    if (File.Exists(Program.nowDirtionary+@"\"+tabName+".txt"))
                    {
                        // 直接values 
                        if (Clist[3].IndexOf("(") == -1 && Clist[3] == "values")
                        {
                            onlyValuesInsert(Clist);
                            new Create().createIndex("ssn_index","employee", "ssn");
                        }
                        else if (Clist[3].IndexOf("(") != -1 && Clist[3].Substring(0, Clist[3].IndexOf("(")) == "values")
                        {
                            onlyValuesInsert(Clist);
                            new Create().createIndex("ssn_index", "employee", "ssn");
                        }
                        else
                            throw new Exception();
                        // 直接values 
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("insert 语句错误！");
            } 


        }
        //仅仅只含有values   insert into employee values('张三','230101198009081234','1980-09-08', '哈尔滨道里区十二道街', '男', 3125, '23010119751201312X', 'd1')
        public void onlyValuesInsert(List<string> Clist)
        {

            OneTable table = MyDeserialize(tabName);
            //根据数据字典确定主键的索引值 -- 查重
            List<int> keyIndex = new List<int>();
            for(int i = 0;i<table.lines.Count;i++)
            {
                if (table.lines[i].isKey == true)
                {
                    keyIndex.Add(i);
                }
            }

            int num = table.lines.Count;
            string str = null;
            for (int i = 0; i < Clist.Count; i++)
            {
                str += Clist[i] + " ";
            }

            string insertStr = str.Substring(str.IndexOf("(") + 1, str.IndexOf(")") - str.IndexOf("(") - 1);
            //'张三','230101198009081234','1980-09-08',+'哈尔滨道里区十二道街',+'男',+3125,+'23010119751201312x',+'d1'
            
            //根据 , 进行串分割
            List<string> InsertList = Program.commandSplit(insertStr, ",");
            //存储主键
            List<string> KeyList = new List<string>();

            //存储需要写入文件的信息
            List<string> SaveList = new List<string>();
            //判断传入参数是否与数据字典中的参数个数相同
            if(num == InsertList.Count)
            {
                //逐个保存属性 至 SaveList列表中
                for (int i = 0; i < num; i++)
                {
                    switch (table.lines[i].type)
                    {
                        case Type.Bool:
                            {
                                if(InsertList[i]=="true"|| InsertList[i] == "false")
                                {

                                    SaveList.Add(InsertList[i]);
                                    if (keyIndex.Contains(i))
                                    {
                                        KeyList.Add(InsertList[i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("第{0}个属性值 bool 类型 输入参数错误！",i+1);
                                    return;
                                }
                            }
                            break;
                        case Type.Int:
                            {
                                try
                                {
                                    int a = Convert.ToInt32(InsertList[i]);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("第{0}个属性值 int 类型 输入参数错误！", i + 1);
                                    return;
                                }
                                SaveList.Add(InsertList[i]);
                                if (keyIndex.Contains(i))
                                {
                                    KeyList.Add(InsertList[i]);
                                }
                            }
                            break;
                        case Type.Float:
                            {
                                try
                                {
                                    double a = Convert.ToDouble(InsertList[i]);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("第{0}个属性值 float 类型 输入参数错误！", i + 1);
                                    return;
                                }
                                SaveList.Add(InsertList[i]);
                                if (keyIndex.Contains(i))
                                {
                                    KeyList.Add(InsertList[i]);
                                }
                            }
                            break;
                        case Type.Double:
                            {
                                try
                                {
                                    double a = Convert.ToDouble(InsertList[i]);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("第{0}个属性值 double 类型 输入参数错误！", i + 1);
                                    return;
                                }
                                SaveList.Add(InsertList[i]);
                                if (keyIndex.Contains(i))
                                {
                                    KeyList.Add(InsertList[i]);
                                }
                            }
                            break;
                        case Type.Char:
                            {
                                try
                                {
                                    InsertList[i] = InsertList[i].Trim();
                                    string mystr = InsertList[i].Substring(InsertList[i].IndexOf("'")+1, InsertList[i].LastIndexOf("'")- InsertList[i].IndexOf("'")-1);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("第{0}个属性值 char 类型 输入参数错误！", i + 1);
                                    return;
                                }
                                SaveList.Add(InsertList[i]);
                                if (keyIndex.Contains(i))
                                {
                                    KeyList.Add(InsertList[i]);
                                }
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("类型错误！");
                                return;
                            }
                            break;
                    }
                }

                //判断 是否存在该记录
                if (TableExist(keyIndex, KeyList, tabName))
                    return;

                //序列化 存入表文件中
                IFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(Program.nowDirtionary+@"\"+tabName+".txt",FileMode.Append,FileAccess.Write,FileShare.Write);
                formatter.Serialize(fileStream, SaveList);
                fileStream.Close();

                Console.WriteLine("insert成功！");
                return;
            }

            else
            {
                Console.WriteLine("参数个数错误！");
                return;
            }  
            

        }

        //判断表名为tabName表中 是否已经存在该记录的主键信息
        public bool TableExist(List<int> keyIndex, List<string> KeyList,string tabName)
        {
            if (tabName == null)
            {
                Console.WriteLine("没有找到该表啊！");
                return false;
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream1 = new FileStream(Program.nowDirtionary + @"\"+tabName+".txt", FileMode.Open,
            FileAccess.Read, FileShare.Read);
            try
            {
                List<string> obj = (List<string>)formatter.Deserialize(stream1);
                while (obj != null)
                {
                    int i = 0;
                    for ( i = 0; i < keyIndex.Count; i++)
                    {
                        if (obj[keyIndex[i]].Trim() != KeyList[i].Trim())
                            break;
                    }
                    if (i == keyIndex.Count)
                    {
                        Console.WriteLine("主键约束错误，无法插入！");
                        return true;
                    }
                    obj = (List<string>)formatter.Deserialize(stream1);
                }
            }
            catch
            { }
            finally
            {
                stream1.Close();
            }
            return false;
        }

        //从数据字典中获取表名tabName的表信息
        public OneTable MyDeserialize(string tabName)
        {
            if (tabName == null)
            {
                Console.WriteLine("表名为空啊！");
                return null;
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream1 = new FileStream(Program.nowDirtionary + @"\DataDirtionary.txt", FileMode.Open,
            FileAccess.Read, FileShare.Read);
            List<OneTable> list = new List<OneTable>();
            try
            {
                OneTable obj = (OneTable)formatter.Deserialize(stream1);
                while (obj != null)
                {
                    if (obj.name == tabName)
                    {
                        stream1.Close();
                        return obj;
                    }
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
            return null;
        }

    }
}
