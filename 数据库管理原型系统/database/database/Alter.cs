using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.IO;

namespace database
{
    class Alter
    {
        public bool TableAdd(string TableName,string newLine)
        {
            List<OneTable> oneTables = ReadOneTablesFromDD();
            int index = getTableIndex(TableName, oneTables);
            if (index == -1)
            {
                Console.WriteLine("没有找到该表{0}！", TableName);
                return false;
            }
            List<string> PList = Program.commandSplit(newLine, ",");
            
            //属性重复
            if(!new Create().CheckLineOnly(PList))
            {
                return false;
            }
            for (int i = 0; i < PList.Count; i++)
            {
                PList[i] = PList[i].Trim();
                string a = PList[i].Substring(0, PList[i].IndexOf(' '));
                if (oneTables[index].lines.Exists(x => x.name == a == true))
                {
                    Console.WriteLine("您输入的属性值已经存在于数据库！");
                    return false;
                }
            }

            //逐步将属性存入oneTable表对象中
            Create tmp = new Create();
            List<Type> types = new List<Type>();
            for (int i = 0; i < PList.Count; i++)
            {
                OneLine oneLine = new OneLine();
                if (tmp.dealOneLine(oneLine, PList[i]))
                {
                    types.Add(oneLine.type);
                    oneTables[index].lines.Add(oneLine);
                }
                else
                {
                    Console.WriteLine("属性值语句错误！");
                    return false;
                }
            }
            WriteOneTablesToDD(oneTables);
            
            //表文件扩容 写到表文件
            AddTOTableFile(Expend(TableName, types), TableName);

            return true;
        }

        //表文件扩容 add lines 增添属性
        public List<List<string>> Expend(string TableName,List<Type> types)
        {
            List<List<string>> tableFile = new Select().GetTableFromFile(TableName);

            for (int i = 0; i < tableFile.Count; i++)
            {
                foreach (Type item in types)
                {
                    switch (item)
                    {
                        case Type.Bool:
                            tableFile[i].Add("None");
                            break;
                        case Type.Int:
                            tableFile[i].Add(0 + "");
                            break;
                        case Type.Float:
                            tableFile[i].Add(0 + "");
                            break;
                        case Type.Double:
                            tableFile[i].Add(0 + "");
                            break;
                        case Type.Char:
                            tableFile[i].Add(null);
                            break;
                        default:
                            {
                                Console.WriteLine("类型无法判断！");
                                return new List<List<string>>();
                            }
                            break;
                    }
                }
            }

            return tableFile;

        }
        
        //将已经反序列化的表 写回表文件
        public void AddTOTableFile(List<List<string>> tableFile,string tabName)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Program.nowDirtionary + @"\" + tabName + ".txt", FileMode.Truncate, FileAccess.Write, FileShare.Write);
            foreach (List<string> item in tableFile)
            {
                formatter.Serialize(fileStream, item);
            }
            fileStream.Close();
        }

        public int getTableIndex(string TableName, List<OneTable> oneTables)
        {
            TableName = TableName.Trim();
            for(int i = 0;i<oneTables.Count;i++)
            {
                if (TableName == oneTables[i].name)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<OneTable> ReadOneTablesFromDD()
        {
            List<OneTable> result = new List<OneTable>();
            IFormatter formatter = new BinaryFormatter();
            Stream stream1 = new FileStream(Program.nowDirtionary + @"\DataDirtionary.txt", FileMode.Open,
            FileAccess.Read, FileShare.Read);
            try
            {
                OneTable obj = (OneTable)formatter.Deserialize(stream1);
                while (obj != null)
                {
                    result.Add(obj);
                    obj = (OneTable)formatter.Deserialize(stream1);
                }
            }
            catch{
                stream1.Close();
                return result;
            }
            finally
            {
                stream1.Close();
            }
            return result;
        }

        public void WriteOneTablesToDD(List<OneTable> oneTables)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream1 = new FileStream(Program.nowDirtionary + @"\DataDirtionary.txt", FileMode.Truncate,
            FileAccess.Write, FileShare.Read);
            foreach (OneTable item in oneTables)
            {
                formatter.Serialize(stream1, item);
            }
            stream1.Close();
        }

        public bool TableDrop(string TableName, string newLine)
        {
            List<OneTable> oneTables = ReadOneTablesFromDD();
            int index = getTableIndex(TableName, oneTables);
            if (index == -1)
            {
                Console.WriteLine("没有找到该表{0}！", TableName);
                return false;
            }

            List<string> PList = Program.commandSplit(newLine, @"\s+|,");
            //是否存在
            foreach (string item in PList)
            {
                if (!oneTables[index].lines.Exists(x => x.name == item == true))
                {
                    Console.WriteLine("{0}属性在表‘{1}’中不存在",item, TableName);
                    return false;
                }
            }
            List<int> removeListIndex = getIndexFromOneTable(PList, oneTables[index]);

            foreach (int item in removeListIndex)
            {
                try
                {
                    oneTables[index].lines.RemoveAt(item);
                }
                catch (Exception)
                {
                    Console.WriteLine("drop指令异常");
                }
            }
            //写回DD
            WriteOneTablesToDD(oneTables);

            AddTOTableFile(Decrease(TableName, removeListIndex), TableName);

            return true;
        }

        public List<int> getIndexFromOneTable(List<string> PList,OneTable oneTable)
        {
            List<int> IList = new List<int>();
            foreach (string item in PList)
            {
                for (int i = 0; i < oneTable.lines.Count; i++)
                {
                    if (item == oneTable.lines[i].Name)
                    {
                        IList.Add(i);
                        break;
                    }
                }
            }
            return IList;
        }

        public List<List<string>> Decrease(string TableName, List<int> removeList)
        {
            List<List<string>> tableFile = new Select().GetTableFromFile(TableName);

            for (int i = 0; i < tableFile.Count; i++)
            {
                foreach (int item in removeList)
                {
                    tableFile[i].RemoveAt(item);
                }
            }

            return tableFile;

        }
    }
}
