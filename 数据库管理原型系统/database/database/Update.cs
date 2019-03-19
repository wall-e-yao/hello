using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
    class Update
    {
        string tabName= "";

        public List<List<string>> myTable = new List<List<string>>();
        public void upDateTable(string comm)
        {
            comm = comm.Trim();
            comm = comm.Substring(0, comm.IndexOf(";"));
            List<string> SplitThree = Program.commandSplit(comm, "update|set|where");
            if (SplitThree.Count <= 1|| SplitThree.Count > 3)
            {
                Console.WriteLine("update指令错误");
                return;
            }
            tabName = SplitThree[0].Trim();
            myTable= new Select().GetTableFromFile(tabName);
            //无条件
            if(SplitThree.Count == 2)
            {
                if (!AllUpdate(SplitThree[1].Trim()))
                {
                    return;
                }
                
            }
            //有条件
            else
            {
                if (!ConditionUpdate(SplitThree[1].Trim(),SplitThree[2].Trim()))
                {
                    return;
                }
            }
        }
        public bool AllUpdate(string command)
        {
            List<string> PList = Program.commandSplit(command, ",");
            List<string> LeftList = new List<string>();
            List<string> RightList = new List<string>();
            try
            {
                foreach (var item in PList)
                {
                    List<string> two = Program.commandSplit(item, "=");
                    LeftList.Add(two[0].Trim());
                    RightList.Add(two[1].Trim());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("set 错误");
                return false;
            }
            List<int> index = new List<int>();
            OneTable oneTable = new Insert().MyDeserialize(tabName);
            
            //获取更新的下标
            foreach (var item in LeftList)
            {
                for (int i = 0; i < oneTable.lines.Count; i++)
                    if (item == oneTable.lines[i].Name)
                        index.Add(i);
            }
            //修改
            try
            {
                for (int i = 0; i < myTable.Count; i++)
                {
                    for (int j = 0; j < index.Count; j++)
                    {
                        myTable[i][index[j]] = RightList[j];
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("指令错误，update失败");
                return false;
            }
            
            //写入表文件 （覆盖）
            new Alter().AddTOTableFile(myTable, tabName);

            return true;
        }
        //带条件
        public bool ConditionUpdate(string command,string conditions)
        {
            List<string> PList = Program.commandSplit(command, ",");
            List<string> LeftList = new List<string>();
            List<string> RightList = new List<string>();
            try
            {
                foreach (var item in PList)
                {
                    List<string> two = Program.commandSplit(item, "=");
                    LeftList.Add(two[0].Trim());
                    RightList.Add(two[1].Trim());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("set 错误");
                return false;
            }
            List<int> index = new List<int>();
            OneTable oneTable = new Insert().MyDeserialize(tabName);

            //获取更新的下标
            foreach (var item in LeftList)
            {
                for (int i = 0; i < oneTable.lines.Count; i++)
                    if (item == oneTable.lines[i].Name)
                        index.Add(i);
            }

            int flag = 0;
            List<string> ConditionList = new List<string>();
            if (conditions.Contains(" or "))
            {
                ConditionList = Program.commandSplit(conditions, "or");
                flag = 1;
            }
            //修改
            try
            {
                if (flag == 1) { 
                    for (int i = 0; i < myTable.Count; i++)
                    {
                        if(new Select().JudgeOrCondition(myTable[i],ConditionList))
                        for (int j = 0; j < index.Count; j++)
                        {
                            myTable[i][index[j]] = RightList[j];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < myTable.Count; i++)
                    {
                        if (JudgeAndCondition(myTable[i], conditions))
                            for (int j = 0; j < index.Count; j++)
                            {
                                myTable[i][index[j]] = RightList[j];
                            }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("指令错误，update失败");
                return false;
            }

            //写入表文件 （覆盖）
            new Alter().AddTOTableFile(myTable, tabName);

            return true;
        }
        public bool JudgeAndCondition(List<string> oneJiLu, string Condition)
        // employee.salary > 3000 and employee.bdate > '1982-01-01' and employee.sex = '男'
        {
            Condition = Condition.Trim();
            List<string> oneCondition = Program.commandSplit(Condition, " and ");
            List<string> onlyJudgeLine = new List<string>();//保存属性
            List<string> onlyJudgeSymbol = new List<string>();//保存符号 >= ,<= ,!= ,> ,< ,=,
            List<string> onlyJudgeParams = new List<string>();//保存参数
            OneTable myTable = new Insert().MyDeserialize(tabName);
            foreach (string item in oneCondition)
            {
                List<string> temp = new Select().getSubStr(item);
                temp[0] = temp[0].Trim();
                onlyJudgeLine.Add(temp[0]);
                temp[1] = temp[1].Trim();
                onlyJudgeSymbol.Add(temp[1]);
                temp[2] = temp[2].Trim();
                onlyJudgeParams.Add(temp[2]);
            }

            //获取所有条件的下标
            List<int> JudgeLineIndex = getIndex(onlyJudgeLine);
            List<int> JudgeParamsIndex = getIndex(onlyJudgeParams);
            if(JudgeLineIndex.Count==0&&JudgeParamsIndex.Count == 0)
            {
                return true;
            }
            for (int i = 0, k = 0, j = 0; k < oneCondition.Count; k++)
            {
                //九种情况
                //left 数字
                if (double.TryParse(onlyJudgeLine[k], out double xx))
                {
                    if (double.TryParse(onlyJudgeParams[k], out double yy))
                    {
                        if (!new Select().JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], onlyJudgeParams[k], Type.Double))
                            return false;
                    }
                    else if (onlyJudgeParams[k].Contains("'"))
                    {
                        Console.WriteLine("数值与字符串无法比较！");
                        return false;
                    }
                    else
                    {
                        if (!new Select().JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], oneJiLu[JudgeParamsIndex[j]], myTable.lines[JudgeParamsIndex[j++]].type))
                            return false;
                    }
                }
                //left 字符串
                else if (onlyJudgeLine[k].Contains("'"))
                {
                    if (double.TryParse(onlyJudgeParams[k], out double yy))
                    {
                        Console.WriteLine("数值与字符串无法比较！");
                        return false;
                    }
                    else if (onlyJudgeParams[k].Contains("'"))
                    {
                        if (!new Select().JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], onlyJudgeParams[k], Type.Char))
                            return false;
                    }
                    else
                    {
                        if (!new Select().JudgeOneCondition(onlyJudgeLine[k], onlyJudgeSymbol[k], oneJiLu[JudgeParamsIndex[j]], myTable.lines[JudgeParamsIndex[j++]].type))
                            return false;
                    }
                }
                // left 属性
                else
                {
                    if (double.TryParse(onlyJudgeParams[k], out double yy))
                    {
                        if (!new Select().JudgeOneCondition(oneJiLu[JudgeLineIndex[i]], onlyJudgeSymbol[k], onlyJudgeParams[k], myTable.lines[JudgeLineIndex[i++]].type))
                            return false;
                    }
                    else if (onlyJudgeParams[k].Contains("'"))
                    {
                        if (!new Select().JudgeOneCondition(oneJiLu[JudgeLineIndex[i]], onlyJudgeSymbol[k], onlyJudgeParams[k], myTable.lines[JudgeLineIndex[i++]].type))
                            return false;
                    }
                    else
                    {
                        if (!new Select().JudgeOneCondition(oneJiLu[JudgeLineIndex[i++]], onlyJudgeSymbol[k], oneJiLu[JudgeParamsIndex[j]], myTable.lines[JudgeParamsIndex[j++]].type))
                            return false;
                    }
                }
            }

            return true;
        }
        public List<int> getIndex(List<string> linesName)   //属性名List
        {
            OneTable oneTable = new Insert().MyDeserialize(tabName);
            List<int> result = new List<int>();
            //获取更新的下标
            foreach (var item in linesName)
            {
                if (item.Contains("'")|| double.TryParse(item, out double xx))
                    continue;
                int i = 0;
                for (i = 0; i < oneTable.lines.Count; i++)
                    if (item == oneTable.lines[i].Name)
                    {
                        result.Add(i);
                        break;
                    }
                if(i== oneTable.lines.Count)
                {
                    Console.WriteLine("没有找到{0}属性",item);
                    return new List<int>();
                }
            }
            return result;
        }
    }
}
