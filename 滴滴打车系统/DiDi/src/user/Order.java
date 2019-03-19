/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package user;

import java.io.Serializable;

/**
 *
 * @author yaohao
 */
public class Order implements Serializable{
    private String name;
    private String telephone;
    private String start;
    private String destination;
    
    public Order()
    {
        this.name = "";
        this.telephone = "";
        this.start = "";
        this.destination = "";
    }
    public Order(String s1,String s2,String s3,String s4)
    {
        name  = s1;
        telephone  = s2;
        start = s3;
        destination = s4;
    }
    public void Show_Order()
    {
        System.out.println("姓名："+this.name);
        System.out.println("联系方式："+this.telephone);
        System.out.println("出发点："+this.start);
        System.out.println("目的地："+this.destination);
    }
    public void Set_start(String newstart)
    {
        this.start = newstart;
    }
    public void Set_destination(String newdes)
    {
        this.destination = newdes;
    }
    
}
