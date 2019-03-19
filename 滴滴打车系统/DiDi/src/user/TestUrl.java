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
import java.awt.Desktop;  
import java.io.IOException;  
import java.net.URI;  
import java.net.URISyntaxException;  
     
/**  
 * @author liuyazhuang  
 * @time 2015-10-22  
 *  
 */  
public class TestUrl {  
      
    /**  
     * 打开IE浏览器访问页面  
     */  
    public static void openIEBrowser(){  
         //启用cmd运行IE的方式来打开网址。  
        String str = "cmd /c start iexplore file:///C:/Users/yaohao/Desktop/%E5%A4%A7%E5%AD%A6++/%E6%BB%B4%E6%BB%B4%E6%89%93%E8%BD%A6%E7%B3%BB%E7%BB%9F/DiDi/src/user/html_test.html";  
        try {  
            Runtime.getRuntime().exec(str);  
        } catch (IOException e) {  
            e.printStackTrace();  
        }  
    }  
      
    /**  
     * 打开默认浏览器访问页面  
     */  
    public static void openDefaultBrowser(){  
        //启用系统默认浏览器来打开网址。  
        try {  
            URI uri = new URI("file:///D:/test/hello.html");  
            Desktop.getDesktop().browse(uri);  
        } catch (URISyntaxException e) {  
            e.printStackTrace();  
        } catch (IOException e) {  
            e.printStackTrace();  
        }  
    }  
   
    public static void main(String[] args) {  
        //openIEBrowser();  
        openDefaultBrowser();  
    }  
}  