/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package user;
/**
 *
 * @author yaohao
 */
import java.io.*;
//import java.util.logging.Level;
//import java.util.logging.Logger;
public class User_Information implements Serializable{
    private String name = null;
    private String password = null;
    private String telephone = null;
    private String id = null;
    public User_Information()
    {
        name = "";
        password = "";
        telephone = "";
        id = "";
    }
    
    public User_Information(String name,String password,String telephone,String id)
    {
        this.name = name;
        this.password = password;
        this.telephone = telephone;
        this.id = id;
    }
    public String Get_name()
    {
        return this.name;
    }
    public String Get_password()
    {
        return this.password;
    }
    public String Get_telephone()
    {
        return this.telephone;
    }
    public String Get_id()
    {
        return this.id;
    }
    public boolean Check(String name,String password)
    {
        return (this.name.equals(name)&&this.password.equals(password));
    }
   
    public void Show_User_Information()
    {
        System.out.println("姓名："+this.name);
        System.out.println("密码："+this.password);
        System.out.println("手机："+this.telephone);
        System.out.println("身份证："+this.id);
    }
    public void Set_Information(String name,String password,String telephone,String id)
    {
        this.name = name;
        this.password = password;
        this.telephone = telephone;
        this.id = id;
    }
}
