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
  	valueobject.Friend friend = (valueobject.Friend)request.getAttribute("updatefriend");
  	
   %>
	<center>
	    <form action="UpdateFriend"  method="post">
	      <table border = "1" >
	      	<tr>
	          <td align="right">好友ID：</td>
	          <td><input name = "id"  style="width:220;height:25;"  value=<%=friend.getId()%> readonly="readonly"></td>
	        </tr>
	        <tr>
	          <td align="right">我的ID：</td>
	          <td><input name = "userid" style="width:220;height:25;" value=<%=friend.getUserid()%> readonly="readonly" ></td>
	        </tr>
	        <tr>
	          <td align="right">姓名：</td>
	          <td><input name = "name" style="width:220;height:25;" value=<%=friend.getName()%>  ></td>
	        </tr>
	        <tr>
	          <td align="right">性别：</td>
	          <td>
	          <input type=radio name = "sex"  value="M" <%="M".equals(friend.getSex())?"checked":""%>>男　
	          <input type=radio name = "sex"  value="F" <%="F".equals(friend.getSex())?"checked":""%>>女
	          </td>
	        </tr>
	        <tr>
	          <td align="right">年龄：</td>
	          <td><input name = "age" style="width:220;height:25;" value=<%=friend.getAge()%> ></td>
	        </tr>
	        <tr>
	          <td align="right">QQ号：</td>
	          <td><input name = "qq" style="width:220;height:25;" value=<%=friend.getQq() %> ></td>
	        </tr>
	        <tr>
	          <td align="right">电话：</td>
	          <td><input name = "telephone" style="width:220;height:25;" value=<%=friend.getTelephone()%> ></td>
	        </tr>
	        <tr>
	          <td align="right">Email：</td>
	          <td><input name = "email" style="width:220;height:25;" value=<%=friend.getEmail() %> ></td>
	        </tr>
	        <tr>
	          <td align="right">地址：</td>
	          <td><input name = "address" style="width:220;height:25;" value=<%=friend.getAddress()%> ></td>
	        </tr>
	        <tr>
	           <td colspan = "2" ><center><input type=submit value="修改" >　<input type=reset value="重置" >　<input type=button value="返回" onclick=window.location.href="QueryAllFriend"; ></center></td>
	        </tr>
	      </table>
	    </form>
  	</center>
  	  <%@include  file="footer.jsp"%>
  </body>
  
</html>
