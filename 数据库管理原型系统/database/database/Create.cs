using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace database
{
    [Serializable]
    public class OneTable
    {
        public string name = null;

        public List<OneLine> lines = new List<OneLine>();
        public void Show()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].Show();
            }
        }

    }
    public enum Type
    {
        Bool,
        Int,
        Float,
        Double,
        Char
    }

    [Serializable]
    public class OneLine : IEquatable<OneLine>
    {
        public string name = null;
        public Type type; // 0-bool-2,  1-int-4,  2-float-4,  3-double-8,   4-char-n
        public int size;
        public bool canNull;
        public bool isKey;

        public string Name { get => name; set => name = value; }
        public bool CanNull { get => canNull; set => canNull = value; }
        public bool IsKey { get => isKey; set => isKey = value; }

        public bool Equals(OneLine other)
        {
            return this.Name == other.Name;
        }
        public void Show()
        {
            Console.WriteLine("属性名：" + name);
            Console.WriteLine("属性类型：" + type);
            Console.WriteLine("属性大小：" + size);
            Console.WriteLine("属性是否能为空：" + canNull);
            Console.WriteLine("属性是否为主键：" + isKey);
        }
    }

    class OnlyName : IEquatable<OnlyName>
    {
        public string name;
        public bool Equals(OnlyName other)
        {
            return this.name == other.name;
        }
    }

    class Create
    {
        String path = @"C:\Users\yaohao\Desktop\大学--\sql\数据库实现\data\";
        String dataName = null;
        String tableName = null;
        string primaryKey = null;
        public string Path { get => path; }

        public string DataName { get => dataName; set => dataName = value; }

        public string TableName { get => tableName; set => tableName = value; }

        public void createIndex(string indexName,string myTableName,string line)
        {

            StreamWriter str = new StreamWriter(new FileStream(Program.nowDirtionary + @"\" + indexName + ".txt", FileMode.Create, FileAccess.Write));
            str.Close();
            OneTable table = new Insert().MyDeserialize(myTableName);
            int index = 0,i = 0;
            for ( i = 0; i < table.lines.Count; i++)
            {
                if (table.lines[i].name == line)
                {
                    index = i;
                    break;
                }
            }

            if(i == table.lines.Count)
            {
                Console.WriteLine("没有找到{0}属性",line);
                return;
            }

            Dictionary<string, long> keys = new Dictionary<string, long>();

            IFormatter formatter = new BinaryFormatter();
            Stream stream1 = new FileStream(Program.nowDirtionary + @"\" + myTableName + ".txt", FileMode.Open,
            FileAccess.Read, FileShare.Read);
            try
            {
                long fore = stream1.Position;
                List<string> obj = (List<string>)formatter.Deserialize(stream1);
                while (obj != null)
                {
                    keys.Add(obj[index], fore); 
                    fore = stream1.Position;
                    obj = (List<string>)formatter.Deserialize(stream1);
                }
            }
            catch
            { }
            finally
            {
                stream1.Close();
            }

            Stream stream = new FileStream(Program.nowDirtionary + @"\" + indexName + ".txt", FileMode.Truncate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, keys);


            stream.Close();
        }

        public void createTable(string tabName, string param, string nowDirtionary)
        {
            OneTable oneTable = new OneTable();
            List<string> keyName = new List<string>();
            oneTable.name = tabName;

            if (Program.isMatch(param) == false)
            {
                Console.WriteLine("括号不匹配啊！");
                return;
            }

            if (File.Exists(nowDirtionary + @"\"+tabName + ".txt")==true)
            {
                Console.WriteLine("该表已经建立！");
                return;
            }

            //提前保存 primary key （  ， ， ）指令
            int p = param.IndexOf("primary key");
            if (p != -1)
            {
                primaryKey = param.Substring(p);
                int i = primaryKey.IndexOf('(');
                int j = primaryKey.IndexOf(')');
                string mystr = primaryKey.Substring(i + 1, j - i - 1);
                keyName = Program.commandSplit(mystr, @"\s+|,");
            }
            else
            {
                primaryKey = null;
                Console.WriteLine("缺少主键！");
                return;
            }
            string shuXing = param.Substring(0, param.IndexOf("primary key"));
            shuXing = shuXing.Trim();
            List<string> PList = Program.commandSplit(shuXing, @",");

            //判断属性值是否重复
            if (!CheckLineOnly(PList))
            {
                return;
            }

            //逐步将属性存入oneTable表对象中
            for (int i = 0; i < PList.Count; i++)
            {
                OneLine oneLine = new OneLine();
                if (dealOneLine(oneLine, PList[i]))
                {
                    oneTable.lines.Add(oneLine);
                }
                else
                {
                    Console.WriteLine("属性值语句错误！");
                    return;
                }
            }

            //处理主键问题 --（可能是多个主键）
            for (int j = 0; j < keyName.Count; j++)
            {
                for (int k = 0; k < oneTable.lines.Count; k++)
                {
                    if (keyName[j] == oneTable.lines[k].Name)
                    {
                        oneTable.lines[k].isKey = true;
                    }
                }
            }

            // 将oneTable 序列化 到DD.txt   并生成表文件
            oneTable.Show();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(nowDirtionary + @"\DataDirtionary.txt", FileMode.Append,FileAccess.Write, FileShare.None);
            StreamWriter str = new StreamWriter(new FileStream(nowDirtionary + @"\"+tabName+".txt", FileMode.Create,FileAccess.Write));
            str.Close();
            formatter.Serialize(stream, oneTable);
            stream.Close();
            
        }

        public bool CheckLineOnly(List<string> PList)
        {
            List<OnlyName> onlyName = new List<OnlyName>();
            for (int i = 0; i < PList.Count; i++)
            {
                OnlyName a = new OnlyName();
                try
                {
                    PList[i] = PList[i].Trim();
                    a.name = PList[i].Substring(0, PList[i].IndexOf(' '));
                }
                catch (Exception e)
                {
                    Console.WriteLine("属性值语法有错误！ in 判断属性值是否重复");
                    return false;
                }
                if (a.name != "primary" && onlyName.Exists(x => x.name == a.name) == true)
                {
                    Console.WriteLine("您输入的指令属性重复！");
                    return false;
                }
                else
                    onlyName.Add(a);
            }
            return true;
        }

        public bool dealOneLine(OneLine oneLine, string str)// 0 错误 
        {
            List<string> line = Program.commandSplit(str, "\\s+");

            if (str.Contains(" int ")||str.Contains(" int"))
            {
                if (str.Contains(" not ") && str.Contains(" null"))//非空
                {
                    if (line.Count == 4)
                    {
                        oneLine.Name = line[0];
                        oneLine.canNull = false;
                        oneLine.isKey = false;
                        oneLine.type = Type.Int;
                        oneLine.size = sizeof(Int32);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性有错误!", line[0]);
                        return false;
                    }
                }
                else if (str.Contains(" not ") && !str.Contains(" null") || !str.Contains(" not ") && str.Contains(" null"))
                {
                    Console.WriteLine("无法确定是否为空!");
                    return false;
                }
                if (line.Count == 2)
                {
                    oneLine.Name = line[0];
                    oneLine.canNull = true;
                    oneLine.isKey = false;
                    oneLine.type = Type.Int;
                    oneLine.size = sizeof(int);
                    return true;
                }
                else
                {
                    Console.WriteLine("{0}属性有错误!", line[0]);
                    return false;
                }
            }

            else if (str.Contains(" float ") || str.Contains(" float"))
            {
                if (str.Contains(" not ") && str.Contains(" null"))//非空
                {
                    if (line.Count == 4)
                    {
                        oneLine.Name = line[0];
                        oneLine.canNull = false;
                        oneLine.isKey = false;
                        oneLine.type = Type.Float;
                        oneLine.size = sizeof(float);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性有错误!", line[0]);
                        return false;
                    }
                }
                else if (str.Contains(" not ") && !str.Contains(" null") || !str.Contains(" not ") && str.Contains(" null"))
                {
                    Console.WriteLine("无法确定是否为空!");
                    return false;
                }
                if (line.Count == 2)
                {
                    oneLine.Name = line[0];
                    oneLine.canNull = true;
                    oneLine.isKey = false;
                    oneLine.type = Type.Float
;                   oneLine.size = sizeof(float);
                    return true;
                }
                else
                {
                    Console.WriteLine("{0}属性有错误!", line[0]);
                    return false;
                }
            }

            else if (str.Contains(" double ") || str.Contains(" double"))
            {
                if (str.Contains(" not ") && str.Contains(" null"))//非空
                {
                    if (line.Count == 4)
                    {
                        oneLine.Name = line[0];
                        oneLine.canNull = false;
                        oneLine.isKey = false;
                        oneLine.type = Type.Double;
                        oneLine.size = sizeof(double);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性有错误!", line[0]);
                        return false;
                    }
                }
                else if (str.Contains(" not ") && !str.Contains(" null") || !str.Contains(" not ") && str.Contains(" null"))
                {
                    Console.WriteLine("无法确定是否为空!");
                    return false;
                }
                if (line.Count == 2)
                {
                    oneLine.Name = line[0];
                    oneLine.canNull = true;
                    oneLine.isKey = false;
                    oneLine.type = Type.Double;
                    oneLine.size = sizeof(double);
                    return true;
                }
                else
                {
                    Console.WriteLine("{0}属性有错误!", line[0]);
                    return false;
                }
            }

            else if (str.Contains(" bool ") || str.Contains(" bool"))
            {
                if (str.Contains(" not ") && str.Contains(" null"))//非空
                {
                    if (line.Count == 4)
                    {
                        oneLine.Name = line[0];
                        oneLine.canNull = false;
                        oneLine.isKey = false;
                        oneLine.type = Type.Bool;
                        oneLine.size = sizeof(bool);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性有错误!", line[0]);
                        return false;
                    }
                }
                else if (str.Contains(" not ") && !str.Contains(" null") || !str.Contains(" not ") && str.Contains(" null"))
                {
                    Console.WriteLine("无法确定是否为空!");
                    return false;
                }
                if (line.Count == 2)
                {
                    oneLine.Name = line[0];
                    oneLine.canNull = true;
                    oneLine.isKey = false;
                    oneLine.type = Type.Bool;
                    oneLine.size = sizeof(bool);
                    return true;
                }
                else
                {
                    Console.WriteLine("{0}属性有错误!", line[0]);
                    return false;
                }
            }

            else if (str.Contains(" char(") || str.Contains(" char "))
            {
                int number = 0;
                try
                {
                    List<string> num = Program.commandSplit(str, "\\D+");
                    number = Convert.ToInt32(num[0]);
                }
                catch (Exception)
                {
                    Console.WriteLine("char的长度不符合要求");
                    return false;
                }
                string otherStr = str.Remove(str.IndexOf("("), str.IndexOf(")") - str.IndexOf("(") + 1);

                List<string> other = Program.commandSplit(str, "\\s+");

                if (otherStr.Contains(" not ") && otherStr.Contains(" null"))//非空
                {
                    if (other.Count == 4)
                    {
                        oneLine.Name = other[0];
                        oneLine.canNull = false;
                        oneLine.isKey = false;
                        oneLine.type = Type.Char;
                        oneLine.size = sizeof(char) * number;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性有错误!", other[0]);
                        return false;
                    }
                }
                else if (otherStr.Contains(" not ") && !otherStr.Contains(" null") || !otherStr.Contains(" not ") && otherStr.Contains(" null"))
                {
                    Console.WriteLine("无法确定是否为空!");
                    return false;
                }
                if (other.Count == 2)
                {
                    oneLine.Name = other[0];
                    oneLine.canNull = true;
                    oneLine.isKey = false;
                    oneLine.type = Type.Bool;
                    oneLine.size = sizeof(char) * number;
                    return true;
                }
                else
                {
                    Console.WriteLine("{0}属性有错误!", other[0]);
                    return false;
                }
            }

            return false;
        }

        public void createDatabase(List<string> Clist)
        {
            //Create database zhang;
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
                //创建一个数据库文件夹
                if (Directory.Exists(Path + @"\" + DataName) == false)
                {
                    DirectoryInfo dir = Directory.CreateDirectory(Path + @"\" + DataName);
                    Stream fileStream = new FileStream(Path + @"\" + DataName + @"\DataDirtionary.txt", FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Console.WriteLine("已经创建 {0} 数据库", DataName);
                }
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("该数据库已经存在！");
            }
        }

    }

}
