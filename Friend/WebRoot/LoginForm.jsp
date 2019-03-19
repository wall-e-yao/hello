<%@page pageEncoding="utf-8" %>
<html>
<head>
	<title>Login</title>
</head>
<body>
<%@include file="header.jsp" %>

<%
	String msg = (String)request.getAttribute("errormsg");
	if(msg!=null){
	out.print("<center><font color=red >"+msg+"</font></center>");
	}
%>
<center>
    <form action="Login"  method="post">
      <table border = "1" >
        <tr>
          <td align="right">用户名：</td>
          <td><input type="text" name = "username" value="abc" style="height:25;width:120;"></td>
        </tr>
        <tr>
          <td align="right">密　码：</td>
          <td><input type="password" name = "password" value="123" style="height:25;width:120;" ></td>
        </tr>
        <tr>
           <td colspan = "2" ><center><input type=submit value="登录" style="height:25;" > <input type=reset value="重置" style="height:25;"> <input type=button value="注册" onClick="window.open('registerForm.jsp');" style="height:25;"></center></td>
        </tr>
      </table>
    </form>
  </center>
  
<%@include file="footer.jsp" %>
</body>
</html>