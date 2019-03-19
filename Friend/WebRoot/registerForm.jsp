<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>

<html>
  <head>
    <title>Register</title>
  </head>
  <body>
    <%@include  file="header.jsp"%>
  <%
  	String msg = (String)request.getAttribute("errormsg");
  	if(msg!=null){
  		out.print("<center><font color=red >"+msg+"</font></center>");
  	}
   %>
	<center>
	    <form action="Register"  method="post" onsubmit="return checkAll();">
	      <table border = "1" >
	        <tr>
	          <td align="right">用户 名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br></td>
	          <td><input type="text" name = "username" value="abc"></td>
	        </tr>
	        <tr>
	          <td align="right">密   码：</td>
	          <td><input type="password" name = "password1" value="123" ></td>
	        </tr>
	        <tr>
	          <td align="right">确认密码：</td>
	          <td><input type="password" name = "password2" value="123" ></td>
	        </tr>
	        <tr>
	           <td colspan = "2" ><center><input type=submit value="注册" >　<input type=reset value="重置" >　<input type=button value="返回" onclick=window.location.href="LoginForm.jsp"; ></center></td>
	        </tr>
	      </table>
	    </form>
  	</center>
  	  <%@include  file="footer.jsp"%>
  </body>
	<script type="text/javascript">
	    function checkAll() {
	    	 var name=document.getElementById("username");
	    	if(name.value ==""){
	    		alert("用户名未填写！");
	    		return false;
	    	}
	    	else if(name.value.length<6){
	    		alert("用户名不完整");
	    		return false;
	    	}
	    	var p1 = document.getElementById("password1");
	    	var p2 = document.getElementById("password2");
	    	if (p1.value==""||p2.value=="") 
			{
				alert("密码为空！");
				return false;
			}
	    	if (p1.value!=p2.value) 
	    	{
	    		alert("两次密码不一致！");
	    		return false;
	    	}
	    	return true;
	    }
	</script>
</html>
