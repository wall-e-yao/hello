using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace database
{
    class Select
    {
        List<string> tabNameList = new List<string>();
        List<string> lineNameList = new List<string>();
        OneTable maxTable = new OneTable();
        public bool error = false;  //是否出错

        //选择菜单
        public void SelectMenu(string command,ref User current)
        {
            if (!(command.Contains("select") && command.Contains("from ")))
            {
                Console.WriteLine("select 语句必须包含 select 和 from 关键字");
                return;
            }
            command = command.Trim();
            command = command.Substring(0, command.IndexOf(";"));
            List<string> SSplit = Program.commandSplit(command, "select|from|where");
            if (SSplit.Count < 2)
            {
                Console.WriteLine("select语句错误！");
                return;
            }

            //确定用到的表名
            tabNameList = GetTableNameList(SSplit[1]);
            if (tabNameList.Count == 0)
            {
                Console.WriteLine("表名有错误！");
                return;
            }

            if (!new Enter().CheckSelectLimit(current, tabNameList))
            {
                Console.WriteLine("可能存在没有查询权限的表！");
                return;
            }
            //确定需要投影的属性名
            lineNameList = GetLineNameList(SSplit[0]);
            if (lineNameList.Count == 0)
            {
                return;
            }

            //先做 笛卡尔积
            List<List<string>> diKaEr = GetMaxTable();
            //获取投影下标
            List<int> indexList = NameListToIndexList();
            //
            List<List<string>> tmp = Shadow(diKaEr, indexList);

            //没有where条件的
            if (SSplit.Count == 2)
            {
                ShowTable(tmp);
            }
            //包含where条件的
            if (SSplit.Count == 3)
            {
                List<List<string>> temp = Choose(diKaEr, SSplit[2]);
                if (error == true)
                    return;
                ShowTable(Shadow(temp, indexList));
            }
        }

        //返回表的笛卡尔积计算  并且计算最大表的DD
        public List<List<string>> GetMaxTable()
        {
            if (tabNameList.Count >= 2)
            {
                List<List<string>> DiKaEr = RXS(GetTableFromFile(tabNameList[0]), GetTableFromFile(tabNameList[1]));
                int index = 0;
                maxTable = new Insert().MyDeserialize(tabNameList[0]);
                //加上表名前缀
                for (index = 0; index < maxTable.lines.Count; index++)
                {
                    maxTable.lines[index].Name = tabNameList[0] + "." + maxTable.lines[index].Name;
                }
                OneTable otherTable = new Insert().MyDeserialize(tabNameList[1]);
                foreach (OneLine item in otherTable.lines)
                {
                    maxTable.lines.Add(item);
                }
                //加上表名前缀
                for (; index < maxTable.lines.Count; index++)
                {
                    maxTable.lines[index].Name = tabNameList[1] + "." + maxTable.lines[index].Name;
                }

                for (int i = 2; i < tabNameList.Count; i++)
                {
                    DiKaEr = RXS(DiKaEr, GetTableFromFile(tabNameList[i]));
                    otherTable = new Insert().MyDeserialize(tabNameList[i]);
                    foreach (OneLine item in otherTable.lines)
                    {
                        maxTable.lines.Add(item);
                    }
                    //加上表名前缀
                    for (; index < maxTable.lines.Count; index++)
                    {
                        maxTable.lines[index].Name = tabNameList[i] + "." + maxTable.lines[index].Name;
                    }
                }
                return DiKaEr;
            }
            else if (tabNameList.Count == 1)
            {
                maxTable = new Insert().MyDeserialize(tabNameList[0]);
                //加上表名前缀
                for (int index = 0; index < maxTable.lines.Count; index++)
                {
                    maxTable.lines[index].Name = tabNameList[0] + "." + maxTable.lines[index].Name;
                }
                return GetTableFromFile(tabNameList[0]);
            }
            else
            {
                return new List<List<string>>();
            }
        }
        //获取投影的下标
        public List<int> NameListToIndexList()
        {
            List<int> indexList = new List<int>();
            if (lineNameList.Count <= 0)
            {
                Console.WriteLine("投影异常");
                return indexList;
            }
            if (lineNameList.Count == 1 && lineNameList[0] == "*")
            {
                for (int i = 0; i < maxTable.lines.Count; i++)
                {
                    indexList.Add(i);
                }
                return indexList;
            }

            foreach (string item in lineNameList)
            {
                for (int i = 0; i < maxTable.lines.Count; i++)
                {
                    if (item == maxTable.lines[i].Name)
                    {
                        indexList.Add(i);
                        break;
                    }
                }
            }
            return indexList;
        }

        //从一个DD -> list
        public List<string> OneTableToList(OneTable one)
        {
            List<string> newLineName = new List<string>();
            foreach (OneLine item in one.lines)
            {
                newLineName.Add(item.Name);
            }
            return newLineName;
        }

        //根据 form 后面的表 得到可能用到的表名    缺陷-> 没有考虑重命名运算 eg: from employee a,employee b
        public List<string> GetTableNameList(string str)
        {
            str = str.Trim();
            List<string> result = Program.commandSplit(str, @"\s+|,");
            foreach (string item in result)
            {
                if (!File.Exists(Program.nowDirtionary + @"\" + item + ".txt"))
                {
                    return new List<string>();
                }
            }
            return result;
        }

        //得到需要投影的属性的名字  以employee.name 的形式 ，如果有的属性不存在 将返回空串
        public List<string> GetLineNameList(string str)
        {
            str = str.Trim();
            List<string> result = Program.commandSplit(str, @"\s+|,");
            List<string> LineName = new List<string>();
            if (result.Count == 1 && result[0] == "*")
            {
                LineName.Add("*");
                return LineName;
            }
            foreach (string item in result)
            {
                if (item.Contains("."))
                {
                    List<string> lineTableLineName = Program.commandSplit(item, "\\.");//  employee.name
                    if (LineExist(lineTableLineName[0], lineTableLineName[1]))
                    {
                        if (!LineName.Contains(item))
                            LineName.Add(item);
                        else
                        {
                            Console.WriteLine("属性名重复啦！");
                            return new List<string>();
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0}属性在{1}中不存在！", lineTableLineName[1], lineTableLineName[0]);
                        return new List<string>();
                    }
                }
                else
                {
                    int f = 0;  // 标记
                    for (int i = 0; i < tabNameList.Count; i++)
                    {
                        if (LineExist(tabNameList[i], item))
                        {
                            if (!LineName.Contains(tabNameList[i] + "." + item))
                            {
                                LineName.Add(tabNameList[i] + "." + item);
                                f++;
                            } else
                            {
                                Console.WriteLine("属性名重复啦！");
                                return new List<string>();
                            }
                        }
                    }
                    if (f > 1)
                    {
                        Console.WriteLine("无法确定{0}属性所在的表", item);
                        return new List<string>();
                    }
                    else if (f == 0)
                    {
                        Console.Write("{0}属性在表", item);
                        foreach (string it in this.tabNameList)
                        {
                            Console.Write(it + ",");
                        }
                        Console.Write("\b中没有被发现");
                        Console.WriteLine();
                        return new List<string>();
                    }
                }
            }
            return LineName;
        }
        //查看某一个属性 LineName存不存在与表TableName
        public bool LineExist(string TableName, string LineName)
        {
            OneTable table = new Insert().MyDeserialize(TableName);
            for (int i = 0; i < table.lines.Count; i++)
            {
                if (LineName == table.lines[i].Name)
                {
                    return true;
                }
            }
            return false;
        }
        //RXS 笛卡尔积
        public List<List<string>> RXS(List<List<string>> R, List<List<string>> S)
        {
            if (S.Count == 0)
            {
                return R;
            }
            List<List<string>> result = new List<List<string>>();
            for (int j = 0; j < R.Count; j++)
            {
                for (int k = 0; k < S.Count; k++)
                {
                    List<string> tmp = new List<string>(R[j]);
                    foreach (string item in S[k])
                    {
                        tmp.Add(item);
                    }
                    result.Add(tmp);
                }
            }
            return result;
        }
        //并集
        public List<List<string>> UnionSet(List<List<string>> R, List<List<string>> S)
        {
            return R.Union(S).ToList();
        }
        //交集
        public List<List<string>> IntersectSet(List<List<string>> R, List<List<string>> S)

        {
            return R.Intersect(S).ToList();
        }
        //差集
        public List<List<string>> MinusSet(List<List<string>> R, List<List<string>> S)
        {
            return R.Except(S).ToList();
        }
        //投影
        public List<List<string>> Shadow(List<List<string>> R, List<int> LineIndex)
        {
            List<List<string>> result = new List<List<string>>();
            try
            {
                for (int i = 0; i < R.Count; i++)
                {
                    List<string> tmp = new List<string>(LineIndex.Count);
                    for (int j = 0; j < LineIndex.Count; j++)
                    {
                        tmp.Add(R[i][LineIndex[j]]);
                    }
                    result.Add(tmp);
                }
            }
            catch
            {
                Console.WriteLine("在投影过程中出现异常！");
                return new List<List<string>>();
            }
            return result;
        }

        public List<List<string>> Shadow(List<List<string>> R, string str)
        {
            str = str.Trim();
            if (str == "*")
            {
                return R;
            }
            else
            {
                Console.WriteLine("全投影异常！");
                return new List<List<string>>();
            }
        }
        //选择
        public List<List<string>> Choose(List<List<string>> R, string odds)  //条件
        {
            odds = odds.Trim();
            List<List<string>> result = new List<List<string>>();
            try
            {
                if (odds.Contains(" or "))
                {
                    List<string> ConditionList = Program.commandSplit(odds, "or");
                        foreach (List<string> item1 in R)
                        {
                            if (JudgeOrCondition(item1, ConditionList))
                            {
                                result.Add(item1);
                            }
                        }
                }
                else
                {
                    foreach (List<string> item in R)
                    {
                        if (error == true)
                            return new List<List<string>>();
                        if (JudgeAndCondition(item, odds))
                        {
                            result.Add(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("where条件错误"+e);
                error = true;
                return new List<List<string>>();
            }
            return result;
        }
        //为条件左边中的属性加上表名
        public void LineLeftAddTableName(ref List<string> onlyLine)
        {
            for (int i = 0; i < onlyLine.Count; i++)
            {
                if(onlyLine.Contains("'"))//字符串
                {
                    continue;
                }

                else if(double.TryParse(onlyLine[i], out double a))//数值
                {
                    continue;
                }

                if (onlyLine[i].Contains("."))
                {
                    List<string> lineTableLineName = Program.commandSplit(onlyLine[i], "\\.");//  employee.name
                    if (LineExist(lineTableLineName[0], lineTableLineName[1]) && tabNameList.Contains(lineTableLineName[0]))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性在{1}中不存在！或者{1}本来就不存在", lineTableLineName[1], lineTableLineName[0]);
                        error = true;//出错
                        return;
                    }
                }
                else 
                {
                    int f = 0;  // 标记
                    for (int j = 0; j < tabNameList.Count; j++)
                    {
                        if (LineExist(tabNameList[j], onlyLine[i]))
                        {
                            onlyLine[i] = tabNameList[j] + "."+onlyLine[i];
                            f++;
                        }
                    }

                    if (f > 1)
                    {
                        Console.WriteLine("无法确定{0}属性所在的表", onlyLine[i]);
                        error = true;
                        return;
                    }
                }
            }
        }

        public void LineRightAddTableName(ref List<string> onlyLine)
        {
            for (int i = 0; i < onlyLine.Count; i++)
            {
                if (onlyLine[i].Contains("'"))//字符串
                {
                    continue;
                }
                if(double.TryParse(onlyLine[i], out double a))//浮点数
                {
                    continue;
                }

                if (onlyLine[i].Contains("."))
                {
                    List<string> lineTableLineName = Program.commandSplit(onlyLine[i], "\\.");//  employee.name
                    if (LineExist(lineTableLineName[0], lineTableLineName[1]) && tabNameList.Contains(lineTableLineName[0]))
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("{0}属性在{1}中不存在！或者{1}本来就不存在", lineTableLineName[1], lineTableLineName[0]);
                        error = true;//出错
                        return;
                    }
                }
                else
                {
                    int f = 0;  // 标记
                    for (int j = 0; j < tabNameList.Count; j++)
                    {
                        if (LineExist(tabNameList[j], onlyLine[i]))
                        {
                            onlyLine[i] = tabNameList[j] + "." + onlyLine[i];
                            f++;
                        }
                    }

                    if (f > 1)
                    {
                        Console.WriteLine("无法确定{0}属性所在的表", onlyLine[i]);
                        error = true;
                        return;
                    }

                }
            }
        }

        public bool JudgeOrCondition(List<string>  item1,List<string> ConditionList)
        {
            foreach (string item in ConditionList)
            {

                if (JudgeAndCondition(item1, item))
                {
                    return true;
                }
                if (error == true)
                    return false;
            }
            return false;
        }

        public bool JudgeAndCondition(List<string> oneJiLu, string Condition)
        // employee.salary > 3000 and employee.bdate > '1982-01-01' and employee.sex = '男'
        {
            Condition = Condition.Trim();
            List<string> oneCondition = Program.commandSplit(Condition, " and ");
            List<string> onlyJudgeLine = new List<string>();//保存属性
            List<string> onlyJudgeSymbol = new List<string>();//保存符号 >= ,<= ,!= ,> ,< ,=,
            List<string> onlyJudgeParams = new List<string>();//保存参数
            foreach (string item in oneCondition)
            {
                List<string> temp = getSubStr(item);
                temp[0] = temp[0].Trim();
                onlyJudgeLine.Add(temp[0]);
                temp[1] = temp[1].Trim();
                onlyJudgeSymbol.Add(temp[1]);
                temp[2] = temp[2].Trim();
                onlyJudgeParams.Add(temp[2]);
            }

            LineLeftAddTableName(ref onlyJudgeLine);
            LineRightAddTableName(ref onlyJudgeParams);

            //获取所有条件的下标
            List<int> JudgeLineIndex = getIndex(onlyJudgeLine);
            List<int> JudgeParamsIndex = getIndex(onlyJudgeParams);

            for (int i = 0,k = 0, j = 0; k < oneCondition.Count;k++)
            {
                //九种情况
                //left 数字
                if (double.TryParse(onlyJudgeLine[k], out double xx))
                {
                    if (double.TryParse(onlyJudgeParams[k], out double yy))
                    {
                        if (!JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], onlyJudgeParams[k], Type.Double))
                            return false;
                    }

                    else if (onlyJudgeParams[k].Contains("'"))
                    {
                        error = true;
                        Console.WriteLine("数值与字符串无法比较！");
                        return false;
                    }
                    else
                    {
                        if (!JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], oneJiLu[JudgeParamsIndex[j]], maxTable.lines[JudgeParamsIndex[j++]].type))
                            return false;
                    }
                }
                //left 字符串
                else if (onlyJudgeLine[k].Contains("'"))
                {
                    if (double.TryParse(onlyJudgeParams[k], out double yy))
                    {
                        error = true;
                        Console.WriteLine("数值与字符串无法比较！");
                        return false;
                    }
                    else if (onlyJudgeParams[k].Contains("'"))
                    {
                        if (!JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], onlyJudgeParams[k], Type.Char))
                            return false;
                    }
                    else
                    {
                        if (!JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], oneJiLu[JudgeParamsIndex[j]], maxTable.lines[JudgeParamsIndex[j++]].type))
                            return false;
                    }
                }
                // left 属性
                else
                {
                    if (double.TryParse(onlyJudgeParams[k], out double yy))
                    {
                        if (!JudgeOneCondition(oneJiLu[JudgeLineIndex[i]], onlyJudgeSymbol[k], onlyJudgeParams[k], maxTable.lines[JudgeLineIndex[i++]].type))
                            return false;
                    }
                    else if (onlyJudgeParams[k].Contains("'"))
                    {
                        if (!JudgeOneCondition(oneJiLu[JudgeLineIndex[i]], onlyJudgeSymbol[k], onlyJudgeParams[k], maxTable.lines[JudgeLineIndex[i++]].type))
                            return false;
                    }
                    else
                    {
                        if (!JudgeOneCondition(oneJiLu[JudgeLineIndex[i++]], onlyJudgeSymbol[k], oneJiLu[JudgeParamsIndex[j]], maxTable.lines[JudgeParamsIndex[j++]].type))
                            return false;
                    }
                }
            }

            return true;
        }

        public List<string> getSubStr(string conditon)  //拆分成 a ;> ;b;三部分;
        {
            List<string> splitThreeParts = new List<string>();
            if (conditon.Contains(">="))
            {
                splitThreeParts = Program.commandSplit(conditon, ">=");
                splitThreeParts.Insert(1, ">=");
            }
            else if (conditon.Contains("<="))
            {
                splitThreeParts = Program.commandSplit(conditon, "<=");
                splitThreeParts.Insert(1, "<=");
            }
            else if (conditon.Contains("!="))
            {
                splitThreeParts = Program.commandSplit(conditon, "!=");
                splitThreeParts.Insert(1, "!=");
            }
            else if (conditon.Contains(">"))
            {
                splitThreeParts = Program.commandSplit(conditon, ">");
                splitThreeParts.Insert(1, ">");
            }
            else if (conditon.Contains("<"))
            {
                splitThreeParts = Program.commandSplit(conditon, "<");
                splitThreeParts.Insert(1, "<");
            }
            else if (conditon.Contains("="))
            {
                splitThreeParts = Program.commandSplit(conditon, "=");
                splitThreeParts.Insert(1, "=");
            }
            if (splitThreeParts.Count != 3)
            {
                error = true;
            }
            return splitThreeParts;
        }

        public List<int> getIndex(List<string> linesName)   //属性名List
        {
            List<int> result = new List<int>();
            foreach (string item in linesName)
            {
                for (int i = 0; i < maxTable.lines.Count; i++)
                {
                    if (item == maxTable.lines[i].Name)
                    {
                        result.Add(i);
                        break;

                    }
                }
            }
            return result;
        }

        //判断一个条件的
        public bool JudgeOneCondition(string line, string symbol, string param,Type type)
        {
            CompareStr a = new CompareStr(line);
            string b = param;
            switch (symbol)
            {
                case ">=":
                    if (a.Compare(b,type) == 0 || a.Compare(b, type) == 1)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                    break;
                case "<=":
                    if (a.Compare(b, type) == 0 || a.Compare(b, type) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "!=":
                    if (a.Compare(b, type) != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case ">":
                    if (a.Compare(b, type) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "<":
                    if (a.Compare(b, type) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "=":
                    if (a.Compare(b, type) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    error = true;
                    break;
            }
            return false;
        }
        //打印表
        public void ShowTable(List<List<string>> result)
        {
            if (result.Count == 0)
            {
                Console.WriteLine("该表为空！");
                return;
            }
            foreach (List<string> item in result)
            {
                foreach (string item1 in item)
                {
                    Console.Write(item1 + " ");
                }
                Console.WriteLine();
            }
        }

        //从文件中读取表文件 List<List<string>>
        public List<List<string>> GetTableFromFile(string tabName)
        {
            if (tabName == null)
            {
                Console.WriteLine("没有找到该表啊！");
                return new List<List<string>>();
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream1 = new FileStream(Program.nowDirtionary + @"\" + tabName + ".txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<List<string>> result = new List<List<string>>();
            try
            {
                List<string> obj = (List<string>)formatter.Deserialize(stream1);
                while (obj != null)
                {
                    result.Add(obj);
                    obj = (List<string>)formatter.Deserialize(stream1);
                }
            }
            catch
            { }
            finally
            {
                stream1.Close();
            }
            return result;
        }

    }

    class SoredList : IComparable<SoredList>, IEquatable<SoredList>
    {
        List<string> list;
        public SoredList()
        {
            List = new List<string>();
        }
        public SoredList(List<string> other)
        {
            List = new List<string>(other);
        }

        public List<string> List { get => list; set => list = value; }


        public int CompareTo(SoredList other)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].CompareTo(other.List[i]) != 0)
                {
                    return List[i].CompareTo(other.List[i]);
                }
            }
            return 0;
        }

        public bool Equals(SoredList other)
        {
            for (int i = 0; i < this.List.Count; i++)
            {
                if (this.List[i] != other.List[i])
                    return false;
            }
            return true;
        }

        public void Show()
        {
            foreach (string item in this.List)
            {
                Console.Write(item + " ");
            }
        }
    }

    class CompareStr
    {
        string str;
        public CompareStr()
        {
            str = "";
        }
        public CompareStr(string other)
        {
            str = new String(other);
        }
        public int Compare(string other, Type type)
        {
            if (type == Type.Int)
            {
                try
                {
                    int a = Convert.ToInt32(this.str);
                    int b = Convert.ToInt32(other);
                    if (a < b)
                        return -1;
                    else if (a == b)
                    { return 0; }
                    else
                        return 1;
                }
                catch (Exception)
                {
                    Console.WriteLine("int类型转换错误，无法比较！");
                    return -2; //错误
                }
            }
            else if (type == Type.Float)
            {
                try
                {
                    double a = Convert.ToDouble(this.str);
                    double b = Convert.ToDouble(other);
                    if (a < b)
                        return -1;
                    else if (a == b)
                    { return 0; }
                    else
                        return 1;
                }
                catch (Exception)
                {
                    Console.WriteLine("float类型转换错误，无法比较！");
                    return -2; //错误
                }
            }
            else if (type == Type.Double)
            {
                try
                {
                    double a = Convert.ToDouble(this.str);
                    double b = Convert.ToDouble(other);
                    if (a < b)
                        return -1;
                    else if (a == b)
                    { return 0; }
                    else
                        return 1;
                }
                catch (Exception)
                {
                    Console.WriteLine("double类型转换错误，无法比较！");
                    return -2; //错误
                }
            }
            else if (type == Type.Bool)
            {
                try
                {
                    bool a = Convert.ToBoolean(this.str);
                    bool b = Convert.ToBoolean(other);
                    if (a == b)
                    { return 0; }
                    else
                        return 1;
                }
                catch (Exception)
                {
                    Console.WriteLine("bool类型转换错误，无法比较！");
                    return -2; //错误
                }
            }
            else if (type == Type.Char)
            {
                try
                {
                    return other.CompareTo(str);
                }
                catch (Exception)
                {
                    Console.WriteLine("string类型转换错误，无法比较！");
                    return -2; //错误
                }
            }
            return -2;
        }
    }

}
