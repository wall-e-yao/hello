using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace database
{
    class Grant
    {
        public void Empower(string comm,ref User current)//授权
        {
            comm = comm.Trim();
            comm = comm.Substring(0, comm.IndexOf(";"));
            List<string> SplitThree = Program.commandSplit(comm, "grant|on|to");
            if (SplitThree.Count <= 1 || SplitThree.Count > 3)
            {
                Console.WriteLine("grant指令错误");
                return;
            }
            List<string> power = Program.commandSplit(SplitThree[0],",");
            for (int j = 0; j < power.Count; j++)
            {
                power[j] = power[j].Trim();
            }
            List<string> tables = Program.commandSplit(SplitThree[1],",");
            for (int j = 0; j < power.Count; j++)
            {
                tables[j] = tables[j].Trim();
            }
            string name = SplitThree[2].Trim();
            List<User> users = new Enter().ReadFromFile();
            int i = 0;
            for ( i = 0; i < users.Count; i++)
            {
                if (users[i].uername == name)
                {
                    if (power.Contains("select"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].select.Add(item);
                        }
                    }
                    if (power.Contains("update"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].update.Add(item);
                        }
                    }
                    if (power.Contains("insert"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].insert.Add(item);
                        }
                    }
                    if (power.Contains("delete"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].delete.Add(item);
                        }
                    }
                    break;
                }
            }
            if(i == users.Count)
            {
                Console.WriteLine("没有找到{0}用户",name);
                return;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("userinformation.txt", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            foreach (User item in users)
            {
                formatter.Serialize(stream, item);
            }
            stream.Close();
        }

        public void Unpower(string comm, ref User current)//撤回
        {
            comm = comm.Trim();
            comm = comm.Substring(0, comm.IndexOf(";"));
            List<string> SplitThree = Program.commandSplit(comm, "revoke|on|from");
            if (SplitThree.Count <= 1 || SplitThree.Count > 3)
            {
                Console.WriteLine("revoke指令错误");
                return;
            }
            List<string> power = Program.commandSplit(SplitThree[0], ",");
            for (int j = 0; j < power.Count; j++)
            {
                power[j] = power[j].Trim();
            }
            List<string> tables = Program.commandSplit(SplitThree[1], ",");
            for (int j = 0; j < power.Count; j++)
            {
                tables[j] = tables[j].Trim();
            }
            string name = SplitThree[2].Trim();
            List<User> users = new Enter().ReadFromFile();

            int i = 0;
            for (i = 0; i < users.Count; i++)
            {
                if (users[i].uername == name)
                {
                    if (power.Contains("select"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].select.Remove(item);
                        }
                    }
                    if (power.Contains("update"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].update.Remove(item);
                        }
                    }
                    if (power.Contains("insert"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].insert.Remove(item);
                        }
                    }
                    if (power.Contains("delete"))
                    {
                        foreach (string item in tables)
                        {
                            users[i].delete.Remove(item);
                        }
                    }
                    break;
                }
            }
            if (i == users.Count)
            {
                Console.WriteLine("没有找到{0}用户", name);
                return;
            }


            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("userinformation.txt", FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            foreach (User item in users)
            {
                formatter.Serialize(stream, item);
            }
            stream.Close();

        }

    }
}
