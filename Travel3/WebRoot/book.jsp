<%@ page language="java" import="java.util.*" pageEncoding="utf-8"%>
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
	
	
	DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
	List<Object> list1 = userDao.GetBy("account = '"+uid+"'");
	valueobject.User user = (valueobject.User)list1.get(0);
	
	
   	String tid = request.getParameter("tid");
   	if(tid==null){
   		request.setAttribute("errormsg","您还没看详情哦");
   		request.getRequestDispatcher("index.jsp").forward(request,response);
   		return ;
   	}
   	DAO.DaoInterface travelDao = DB.ObjectFactory.getTravelDao();
  	List<Object> list = travelDao.GetBy("travelid = '"+tid+"'");
  	valueobject.Travel travel = (valueobject.Travel)list.get(0);
  	
  	
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

    <div class="book-detail">
        <div class="card text-center cards">
            <div class="card-header">
                <%=travel.getName() %>
            </div>
            <div class="card-body">
            
                <form action="Pay?tid=<%=tid%>" method="post" onsubmit = "return check();">
                    <div class="form-row">
                      <div class="form-group col-md-6">
                        <label for="inputEmail4">账号</label>
                        <input type="text" class="form-control" name="account" id="inputid0" value="<%=uid%>" readonly>
                      </div>
                      <div class="form-group col-md-6">
                        <label for="inputname">姓名</label>
                        <input type="text" class="form-control" id="inputname" value="<%=user.getName() %>" readonly>
                      </div>
                    </div>
                    <div class="form-row">
                        <div  class="form-group col-md-4">
                            <label for="inputadult">成人</label>
                            <input type="text" class="form-control " name = "adult" id="inputadult" placeholder="至少1人" >
                        </div> 
                        <div  class="form-group col-md-4">
                            <label for="inputkid">儿童</label>
                            <input type="text" class="form-control " name = "kid" id="inputkid" placeholder = "可以0人" >
                        </div> 
                        <div  class="form-group col-md-4">
                            <label for="inputmoney">单价</label>
                            <input type="text" class="form-control " id="inputmoney" value="<%=travel.getFee() %>" readonly>
                        </div> 
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="inputbir">日期</label>
                            <input type="date" class="form-control" name = "time" id="inputtime"  min=<%=travel.getTime() %> >
                        </div>
                        <div class="form-group col-md-6">
                            <label for="inputmoney">总价</label>
                            <input type="text" class="form-control " id="inputmoneys" name = "money"  readonly>
                        </div>  
                         <div class="form-group col-md-10">
                           <input type="submit" class="btn btn-primary" value = "付款" >
                           </div>
                           <div class="form-group col-md-20">
                           <a href="javascript:history.back(-1)">
                                <button type="button" class="btn btn-danger">取消</button>
                            </a>
                           </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-10">
                        </div>
                        <div class="form-group col-md-2">
                            <a href="#">
                               
                            </a>
                        </div>     
                    </div>
                  </form>
            </div>
        </div>
    </div>
      <script>
        var price = document.getElementById('inputmoney').value;//单价
        var adultPeople = document.getElementById('inputadult');
        var kidPeople = document.getElementById('inputkid');
        var money = document.getElementById('inputmoneys');
        var moneys = 10;
        var money1 = 0;//成人总价
        var money2 = 0;//儿童总价
        money.value = price;
        kidPeople.onchange = function(){
          var people = 0;
          var kid = (this.value)/2;
          people = kid + people;
          money1 = people*price;
          moneys = money1 + money2;
          moneys = moneys.toFixed(2);
          money.value = moneys/1;
        }
        adultPeople.onchange = function(){
          var people = 0;
          var adult = (this.value)/1;
          if(adult<1){
            alert('至少一个成人');
          }
          people = adult + people  ;
          money2 = people*price;
          moneys = money1 + money2;
          moneys = moneys.toFixed(2); 
          money.value = moneys/1;
           
        }
        function check(){
        	 
	    	if(adultPeople.value ==""){
	    		alert("成人未填写！");
	    		return false;
	    	}
	    	if(kidPeople.value ==""){
	    		alert("儿童未填写！");
	    		return false;
	    	}
	    	 var time = document.getElementById('inputtime');
	    	if(time.value ==""){
	    		alert("日期未填写！");
	    		return false;
	    	}
        }
        
      </script>
       
</body>
</html>