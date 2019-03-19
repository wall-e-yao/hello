<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>

<html>
  <head>
    <title>Register</title>
  </head>
  <body>&nbsp; 
  <%@include  file="header.jsp"%>
  <%
  	String msg = (String)request.getAttribute("errormsg");
  	if(msg!=null){
  		out.print("<center><font color=red >"+msg+"</font></center>");
  	}	
   %>
	<center>
	    <form action="Addfriend"  method="post">
	      <table border = "1" >
	        <tr>
	          <td align="right">姓名：</td>
	          <td><input name = "name" value="张三" style="width:220;height:25;"   ></td>
	        </tr>
	        <tr>
	          <td align="right">性别：</td>
	          <td><input name = "sex" value="张三" style="width:220;height:25;"  ></td>
	        </tr>
	        <tr>
	          <td align="right">年龄：</td>
	          <td><input name = "age" value="18" style="width:220;height:25;" ></td>
	        </tr>
	        <tr>
	          <td align="right">QQ号：</td>
	          <td><input name = "qq" value="张三" style="width:220;height:25;" ></td>
	        </tr>
	        <tr>
	          <td align="right">电话：</td>
	          <td><input name = "telephone" value="张三" style="width:220;height:25;"  ></td>
	        </tr>
	        <tr>
	          <td align="right">Email：</td>
	          <td><input name = "email" value="张三" style="width:220;height:25;" ></td>
	        </tr>
	        <tr>
	          <td align="right">地址：</td>
	          <td><input name = "address" value="张三" style="width:220;height:25;" ></td>
	        </tr>
	        <tr>
	           <td colspan = "2" ><center><input type=submit value="添加" >　<input type=reset value="重置" >　<input type=button value="返回" onclick=window.location.href="QueryAllFriend"; ></center></td>
	        </tr>
	      </table>
	    </form>
  	</center>
  	  <%@include  file="footer.jsp"%>
  </body>
  
</html>
