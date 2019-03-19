using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace database
{
    class Use
    {
        public string useDataBase(List<string> Clist)
        {
            string dataName;

            string nowDirtionary;
            if (Clist[1].IndexOf(";") != -1)
            {
                dataName = Clist[1].Remove((Clist[1].IndexOf(";")));
                nowDirtionary = new Create().Path + @"\" + dataName;
            }
            else if (Clist[1].IndexOf(";") == -1 && Clist[2].Trim() == ";")
            {
                dataName = Clist[1];
                nowDirtionary = new Create().Path + @"\" + dataName;
            }
            else
            {
                Console.WriteLine("语法错误！");
                return null;
            }
            try
            {
                if (Directory.Exists(nowDirtionary))
                {
                    Console.WriteLine("{0} database will be used", dataName);
                    return nowDirtionary;
                }
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                nowDirtionary = null;
                Console.WriteLine("没有找到数据库{0}", dataName);
                return nowDirtionary;
            }
        }
    }
}
