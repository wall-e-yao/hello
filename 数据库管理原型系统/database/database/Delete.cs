using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace database
{
    class Delete
    {
        string tabName="";
        public List<List<string>> myTable = new List<List<string>>();
        public void deleteMenu(string comm,ref User current)
        {
            comm = comm.Trim();
            comm = comm.Substring(0, comm.IndexOf(";"));
            List<string> SplitThree = Program.commandSplit(comm, "delete|from|where");
            if (SplitThree.Count <= 1 || SplitThree.Count > 3)
            {
                Console.WriteLine("delete指令错误");
                return;
            }
            tabName = SplitThree[1].Trim();

            List<string> a = new List<string>();
            a.Add(tabName);
            if (!new Enter().CheckUpdateLimit(current, a))
            {
                Console.WriteLine("您没有删除{0}表数据的权限！",a[0]);
                return ;
            }

            myTable = new Select().GetTableFromFile(tabName);

            if (SplitThree.Count == 2)
            {
                new Alter().AddTOTableFile(new List<List<string>>(),tabName);
            }
            else if (SplitThree.Count == 3)
            {
                for (int i = 0; i < myTable.Count; i++)
                {
                    if(JudgeAndCondition(myTable[i], SplitThree[2]))
                    {
                        myTable.RemoveAt(i);
                    }
                }
                new Alter().AddTOTableFile(myTable, tabName);

            }
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
            if (JudgeLineIndex.Count == 0 && JudgeParamsIndex.Count == 0)
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
                if (item.Contains("'") || double.TryParse(item, out double xx))
                    continue;
                int i = 0;
                for (i = 0; i < oneTable.lines.Count; i++)
                    if (item == oneTable.lines[i].Name)
                    {
                        result.Add(i);
                        break;
                    }
                if (i == oneTable.lines.Count)
                {
                    Console.WriteLine("没有找到{0}属性", item);
                    return new List<int>();
                }
            }
            return result;
        }

    }
}
