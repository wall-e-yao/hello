<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>

 <body>
<%@ include file="header.jsp"%>
<%
	String name = (String)session.getAttribute("username");
    if(name==null){
    	request.setAttribute("errormsg","您当前还没有登录，请先登录！");
		request.getRequestDispatcher("LoginForm.jsp").forward(request,response);
		return;
    }
    session.setAttribute("friendname",null);
 %>
 
 <a href="QueryAllFriend">查询我的所有好友信息！</a> <br/><br><br><br><br>
 
 <a href="queryByName.jsp">按照姓名查询我的好友信息！</a><br/>
<%@ include file="footer.jsp" %>
 </body>