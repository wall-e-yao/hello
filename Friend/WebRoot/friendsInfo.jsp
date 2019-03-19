<%@ page language="java" import="java.util.*,valueobject.Friend" pageEncoding="utf-8"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <title>My Friends</title>
  </head>
  <body>
  <%@ include file="header.jsp" %>
<%
	String name = (String)session.getAttribute("username");
    if(name==null){
    	request.setAttribute("errormsg","您当前还没有登录，请先登录！");
		request.getRequestDispatcher("LoginForm.jsp").forward(request,response);
		return;
    }
    String msg = (String)request.getAttribute("errormsg");
	if(msg!=null){
	out.print("<center><font color=red >"+msg+"</font></center>");
	}
    %>
    <a href="addForm.jsp">添加新好友</a>
    <a href="#" onclick="myupdate()">修改选中好友</a>
    <!-- <a href="DeleteFriends" >删除选中好友</a> -->
    <a href="#" onclick="mydelete()">删除选中好友</a>
    <a href="index.jsp">返回主页</a>
    <a href="LoginForm.jsp">注销</a>
    <form action="#" name = myform method="post">
    <table border=1>
    <tr>
    <th>修改</th>
    <th>删除</th>
    <th>编号</th>
    <th>姓名</th>
    <th>年龄</th>
    <th>性别</th>
    <th>电话</th>
    <th>EMail</th>
    <th>QQ</th>
    <th>地址</th>
    </tr>
    <%
    
    List<Friend> friends = (List<Friend>)request.getAttribute("friends");
    //int recordPerPage = 10;
    //int pageNo = Integer.parseInt(request.getParameter("pageNo"));//0-x
    //int maxpage = (int)Math.ceil((double)friends.size()/recordPerPage); 
	for (int i = 0; i < friends.size(); i++) {
  		out.println("<tr>");
  		out.println("<td><input type = radio name = update value = "+friends.get(i).getId()+" ></td>");
  		out.println("<td><input type = checkbox name = delete value = "+friends.get(i).getId()+" ></td>");
  		out.println("<td>"+(i+1)+"</td>");
  		out.println("<td>"+friends.get(i).getName()+"</td>");
  		out.println("<td>"+friends.get(i).getAge()+"</td>");
  		out.println("<td>"+("M".equals(friends.get(i).getSex())?"男":"女")+"</td>");
  		out.println("<td>"+(friends.get(i).getTelephone().equals("")? "&nbsp;" :friends.get(i).getTelephone())+"</td>");
  		out.println("<td>"+friends.get(i).getEmail()+"</td>");
  		out.println("<td>"+friends.get(i).getQq()+"</td>");
  		out.println("<td>"+friends.get(i).getAddress()+"</td>");
  		out.println("</tr>");
	} 	
%>
</table> 
</form>  
  </body>
</html>

<script type="text/javascript">
function mydelete(){
   document.myform.action="DeleteFriends";
   document.myform.submit();
}
function myupdate(){
   document.myform.action="getOneInfo";
   document.myform.submit();
}
</script>
