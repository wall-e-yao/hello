<%@ page language="java" import="java.util.*,DAO.*,valueobject.*" pageEncoding="utf-8"%>
<%@page import="DB.ObjectFactory"%>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="data/style.css">
    <link rel="stylesheet" href="https://cdn.bootcss.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://cdn.bootcss.com/jquery/3.2.1/jquery.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.bootcss.com/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.bootcss.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Document</title>
</head>
<%
	Integer uid = (Integer)session.getAttribute("account");
	String msg = (String)request.getAttribute("errormsg");
	if(msg!=null){
		out.print("<script> alert('"+msg+"'); </script>");
		request.setAttribute("errormsg",null);
	}
%>
<body>
    <div class="head">
        <nav class="navbar navbar-expand-lg navbar-light bg-light " style="background-color: hsl(205, 78%, 79%);">
            <img src="images/index/u182.png" width="64px" height="64px" alt="">
            <a class="navbar-brand" href="about.jsp">xx旅行社</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
            </button>
          
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
              <ul class="navbar-nav mr-auto">
                <li class="nav-item">
                  <a class="nav-link" href="index.jsp">首页 <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" href="about.jsp">关于我们</a>
                </li>
                <li class="nav-item ">
                  <a class="nav-link " href="line.jsp" >旅游路线</a>
                </li>
                <li class="nav-item active">
                  <a class="nav-link " href="book.jsp">在线预约</a>
                </li>
              </ul>
              <form class="form-inline my-2 my-lg-0">
                <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
              </form>
              <div class="nav-item dropdown">
                    <a class="nav-link " href="single.jsp?uid=<%=uid%>">
                        <img src="images/index/me.svg" alt="">
                    </a>    
              </div>
            </div>
          </nav>
    </div>
	        <%
	          	String tid = request.getParameter("tid");
	          	DaoInterface travelDao = DB.ObjectFactory.getTravelDao();
	         	List<Object> list = travelDao.GetBy("travelid = '"+tid+"'");
	         	Travel travel = (Travel)list.get(0);
         	%>
    <div class="card text-center cards">

        <div class="card-header">
         <%=travel.getName() %>
        </div>
        
        <div class="card-body">
          <img class="card-img-top" src="<%=travel.getImageAdd() %>" alt="Card image cap">
          <h5 class="card-title">我们一起去旅行！---<%=travel.getKeyword() %></h5>
          <p class="card-text"><%=travel.getRemake() %></p>
          <iframe id='mymap' scrolling="no" frameborder="0" webkitallowfullscreen="" mozallowfullscreen="" allowfullscreen="" src="mymap.jsp?tid=<%=tid%> " style="width:600px;height:600px"></iframe>
          <a href="book.jsp?tid=<%=tid %>" class="btn btn-primary">马上出发</a>
        </div>

      </div>
      <script>
      </script>
       
</body>
</html>