<%@ page language="java" import="java.util.*,DB.*,java.sql.*" pageEncoding="utf-8"%>
<%@page import="valueobject.User"%>
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
            <a class="navbar-brand" >xx旅行社</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
            </button>
          
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
              <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                  <a class="nav-link" href="admin.jsp">后台管理系统 <span class="sr-only">(current)</span></a>
                </li>
                
              </ul>
              <form class="form-inline my-2 my-lg-0">
                <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
              </form>
              <div class="nav-item ">
                <a class="nav-link" href="admin_single.jsp">
                    <img src="images/index/me.svg" alt="">
                </a>    
              </div>
            </div>
          </nav>
    </div>
    <div class="message">
      <div class="row">
        <div class="col-3">
          <ul class="list-group">
            <li class="list-group-item list-group-item-action active" id="single">个人信息</li>
            
            <a href='Logout' ><li class="list-group-item list-group-item-action" id="out">退出登录</li><a/>
          </ul>
        </div>
        <div class="col-9">
          <div class="single-detial show" id="sd">
            <div class="card">
              <div class="card-header">
                个人信息
              </div>
              <%
              	 DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
         		 List<Object> list = userDao.GetBy("account = '"+uid+"'");
         		 User user = ObjectFactory.getUser();
         		 user = (User)list.get(0);
              %>
              <div class="card-body">
                  <form action="Update" method="post"> 
                      <div class="form-row">
                        <div class="form-group col-md-6">
                          <label for="inputEmail4">账号</label>
                          <input type="text" class="form-control" name = "account" id="inputid0" value="<%=user.getAccount() %>" disabled>
                        </div>
                        <div class="form-group col-md-6">
                          <label for="inputPassword4">密码</label>
                          <input type="password" class="form-control" name = "password" id="inputPassword4" value = "<%=user.getPassword() %>">
                          <small id="passwordHelpInline" class="text-muted">
                            Should be 8-20 characters long.
                          </small>
                        </div>
                      </div>
                      <div class="form-row">
                          <div  class="form-group col-md-6">
                              <label for="inputphone">昵称</label>
                              <input type="text" class="form-control" name = "name" id="inputname" value="<%=user.getName() %>">
                          </div> 
                          <div  class="form-group col-md-6">
                              <label for="inputphone">手机号码</label>
                              <input type="text" class="form-control" name = "telephone" id="inputphone" value="<%=user.getTelephone() %>">
                          </div> 
                      </div>
                      <div class="form-row">
                        <div  class="form-group col-md-6">
                            <label for="inputbir">生日</label>
                            <input type="date" class="form-control" name = "birthday" id="inputbir" value="<%=user.getBirthday() %>">
                        </div>
                        <div  class="form-group col-md-6">
                            <label for="inputid">身份证号</label>
                            <input type="text" class="form-control" name = "id" id="inputid" value="<%=user.getId() %>">
                        </div>
                      </div>
                      
                      <div  class="form-group col-md-6">
                          <label  for="inputsex">性别</label>
                          <div class="form-check form-check-inline">
                              <input class="form-check-input" type="radio" name="sex" id="sexboy" value="M" checked = 'false' >
                              <label class="form-check-label" for="inlineboy">男</label>
                            </div>
                            <div class="form-check form-check-inline">
                              <input class="form-check-input" type="radio" name="sex" id="sexgirl" value="F" checked = 'false' >
                              <label class="form-check-label" for="inlinegirl">女</label>
                            </div>
                      </div>
                      <input class = "btn btn-primary" type = "submit" value="修改" />
                    </form>
                <a></a>
              </div>
            </div>
          </div>
        
      </div>
    </div>
       
       <script>
         var bookDetial = document.getElementById('book');
         var singleDetial = document.getElementById('single');
         var book = document.getElementById('bd');
         var single = document.getElementById('sd');

         bookDetial.onclick = function(){
            singleDetial.className = 'list-group-item list-group-item-action';
            bookDetial.className = 'list-group-item list-group-item-action active';
            book.className = 'book-detail show';
            single.className = 'single-detial hidden';
         }
         singleDetial.onclick = function(){
            singleDetial.className = 'list-group-item list-group-item-action  active';
            bookDetial.className = 'list-group-item list-group-item-action';
            book.className = 'book-detail hidden';
            single.className = 'single-detial show';
         }
       </script>
</body>
</html>