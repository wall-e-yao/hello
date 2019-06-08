<%@ page language="java" import="java.util.*,DAO.*,valueobject.*" pageEncoding="utf-8"%>
<%@page import="javax.servlet.jsp.tagext.TryCatchFinally"%>
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
                <li class="nav-item active">
                  <a class="nav-link" href="index.jsp">首页 <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" href="about.jsp">关于我们</a>
                </li>
                <li class="nav-item ">
                  <a class="nav-link " href="line.jsp" >旅游路线</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link " href="book.jsp">在线预约</a>
                </li>
              </ul>
              <form class="form-inline my-2 my-lg-0">
                <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
              </form>
              <div class="nav-item   active">
                    <a class="nav-link" href="single.jsp?uid=<%=uid%> ">
                        <img src="images/index/me.svg" alt="me">
                    </a>    
              </div>
            </div>
          </nav>
    </div>
    <br/>
    <div class="path">
        <ul>
            <li>
                <div class="china">
                    <div class="container">
                        <div class="row">
                            <div class="col-5">
                            <div class='card' style="width: 25rem; height : 38rem">
                             <img src="images/china1.jpg" alt="">
                             <div class="card-body">
                             	<h5 class="card-title">国内路线</h5>
                                <p class="card-text">相约好友，一起出行 </p>
                                <a href="line.jsp?f=1" class="btn btn-primary">查看更多</a>
                             </div>
                            </div>
                            
                            </div>
                            <% 
                            	 DaoInterface travelDao = DB.ObjectFactory.getTravelDao();
         						 List<Object> list = travelDao.GetBy("keyword like '%国内%' ");
         						 int num = list.size(),i = 0;
         						 List<Travel> travels = new ArrayList<Travel>();
         						 try{
         						 	for(i = 0;i < 4;i++){
         						 		travels.add((Travel)list.get(i));
         						 	}
         						 }
         						 catch(Exception e){
         						 }
                            %>
                            <div class="col">
                            	<% for(i = 0;i<2;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                                
                             </div>	
                             <div class="col">
                                <% for(i = 2;i<4;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="foreign">
                    <div class="container">
                        <div class="row">
                            <div class="col-5">
                            <div class='card' style="width: 25rem; height : 38rem">
                             <img src="images/fore1.jpg" alt="">
                             <div class="card-body">
                             	<h5 class="card-title">国外路线</h5>
                                <p class="card-text">世界那么大，我想去看看 </p>
                                <a href="line.jsp?f=2" class="btn btn-primary">查看更多</a>
                             </div>
                            </div>
                           
                            </div>
                            
                           <% 
         						 List<Object> list1 = travelDao.GetBy("keyword like '%国外%'");
         						 List<Travel> travels1 = new ArrayList<Travel>();
         						 try{
         						 	for(i = 0;i < 4;i++){
         						 		travels1.add((Travel)list1.get(i));
         						 	}
         						 }
         						 catch(Exception e){
         						 }
                            %>
                            <div class="col">
                            	<% for(i = 0;i<2;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels1.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels1.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels1.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels1.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                                
                             </div>	
                             <div class="col">
                                <% for(i = 2;i<4;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels1.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels1.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels1.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels1.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                       
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="free">
                    <div class="container">
                        <div class="row">
                            <div class="col-5">
                             <div class='card' style="width: 25rem; height : 38rem">
                              <img src="images/free1.jpg" alt="">
                               <div class="card-body">
                                <h5 class="card-title">自由路线</h5>
                                <p class="card-text">自由！自由！自由！！！ </p>
                                <a href="line.jsp?f=3" class="btn btn-primary">查看更多</a>
                             </div>
                            </div>
                            </div>
                            <% 
         						 List<Object> list2 = travelDao.GetBy("keyword like '%自由%'");
         						 List<Travel> travels2 = new ArrayList<Travel>();
         						 try{
         						 	for(i = 0;i < 4;i++){
         						 		travels2.add((Travel)list2.get(i));
         						 	}
         						 }
         						 catch(Exception e){
         						 }
                            %>
                            <div class="col">
                            	<% for(i = 0;i<2;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels2.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels2.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels2.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels2.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                                
                             </div>	
                             <div class="col">
                                <% for(i = 2;i<4;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels2.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels2.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels2.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels2.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class="hongkong">
                    <div class="container">
                        <div class="row">
                            <div class="col-5">
                            <div class='card' style="width: 25rem; height : 38rem">
                               <img src="images/hangkong1.jpg" alt="">
                               <div class="card-body">
                                <h5 class="card-title">港澳台路线</h5>
                                <p class="card-text">和爱人一起玩转港澳台 </p>
                                <a href="line.jsp?f=4" class="btn btn-primary">查看更多</a>
                             </div>
                            </div>
                            
                            </div>
                           
                           <% 
         						 List<Object> list3 = travelDao.GetBy("keyword like '%港澳台%'");
         						 List<Travel> travels3 = new ArrayList<Travel>();
         						 try{
         						 	for(i = 0;i < 4;i++){
         						 		travels3.add((Travel)list3.get(i));
         						 	}
         						 }
         						 catch(Exception e){
         						 }
                            %>
                            <div class="col">
                            	<% for(i = 0;i<2;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels3.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels3.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels3.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels3.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                                
                             </div>	
                             <div class="col">
                                <% for(i = 2;i<4;i++){ %>
                                <div class="card" style="width: 18rem;">
                                    <img class="card-img-top" src=<%=travels3.get(i).getImageAdd()%> alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%=travels3.get(i).getName()%> </h5>
                                        <p class="card-text">￥<%=travels3.get(i).getFee() %> </p>
                                        <a href="detail.jsp?tid=<%=travels3.get(i).getTravelid() %>" class="btn btn-primary">Go</a>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
    <script>
        
    </script>
</body>
</html>