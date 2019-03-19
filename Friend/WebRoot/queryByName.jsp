<%@ page contentType="text/html" language="java" import="java.util.*" pageEncoding="utf-8"%>

<html>
  <head>
    <title>Query By Name</title>
  </head>
  
  <body>
	<%@include file="header.jsp" %>
    <form action="QueryByName" method="post">
    <center>
    <input name = friendname value = "王海味" style="width:120;height:25;">　<input type = submit value = "查询" style="height:25;"> 
    <a href = "index.jsp">返回主页</a> 
    <a href = "LoginForm.jsp">注销</a>
    </center>
    </form>
    <%@include file="footer.jsp" %>
  </body>
</html>
