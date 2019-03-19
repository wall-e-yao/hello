using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace database
{
    [Serializable]
    class User
    {
        public string uername;
        public string password;
        public List<string> select = new List<string>();
        public List<string> insert = new List<string>();
        public List<string> update = new List<string>();
        public List<string> delete = new List<string>();

        public User()
        {
            uername = "root";
            password = "123456";
        }

        public User(string user, string pwd)
        {
            uername = user;
            password = pwd;
        }
        public void selectAdd(string name)
        {
            select.Add(name);
        }
        public void insertAdd(string name)
        {
            insert.Add(name);
        }
        public void updateAdd(string name)
        {
            update.Add(name);
        }
        public void deleteAdd(string name)
        {
            delete.Add(name);
        }
    }

    class Enter
    {
        public User CheckPassword(string name,string pwd)
        {
            List<User> users = ReadFromFile();
            User one = new User();
            foreach (User item in users)
            {
                if (name == item.uername && pwd == item.password)
                {
                    one = item;
                    return one;
                }
            }
            return null;
        }
        public void WriteToFile(User user)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("userinformation.txt", FileMode.Append, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, user);
            stream.Close();
        }
        public List<User> ReadFromFile()
        {
            List<User> users = new List<User>();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("userinformation.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                User a = (User)formatter.Deserialize(stream);
                while (a != null)
                {
                    users.Add(a);
                    a = (User)formatter.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                stream.Close();
                return users;
            }
            stream.Close();
            return users;
        }

        public bool CheckSelectLimit(User user,List<string> tables)
        {
            foreach (var item in tables)
            {
                if(!user.select.Contains(item.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckInsertLimit(User user,List<string> tables)
        {
            foreach (var item in tables)
            {
                if(!user.insert.Contains(item.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckDeleteLimit(User user,List<string> tables)
        {
            foreach (var item in tables)
            {
                if(!user.delete.Contains(item.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckUpdateLimit(User user,List<string> tables)
        {
            foreach (var item in tables)
            {
                if(!user.update.Contains(item.Trim()))
                {
                    return false;
                }
            }
            return true;
        }


    }

}
