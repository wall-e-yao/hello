<%@ page language="java" import="java.util.*,java.sql.*" pageEncoding="utf-8"%>
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
request.setCharacterEncoding("utf-8");
response.setCharacterEncoding("utf-8");

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
                  <a class="nav-link" href="#">后台管理系统 <span class="sr-only">(current)</span></a>
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
        <div class="col-2">
          <ul class="list-group">
            <li class="list-group-item list-group-item-action active" id="single">用户管理</li>
            <li class="list-group-item list-group-item-action" id="book">订单管理</li>
            <li class="list-group-item list-group-item-action" id="line">旅游方案</li>
            <li class="list-group-item list-group-item-action" id="form">审计报表</li>
          </ul>
        </div>
        <div class="col-10">
           <%--用户管理 --%>
           <%
           		String aca = request.getParameter("aca");
              	String namea = request.getParameter("namea");
              	if(aca==null)
              		aca="";
           		if(namea==null)
              		namea="";
            %>
          <div class="single-detial show" id="sd">
          <form action="admin.jsp?f=1" method="post">
            <table class="table">
              <thead class="thead-light">
                <tr>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	账号
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a1a' name = "aca" placeholder="Search" value="<%=aca %>" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b1a' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	姓名
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a2a' name = "namea" placeholder="Search" value="<%=namea %>" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b2a' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col">密码</th>
                  <th scope="col">性别</th>
                  <th scope="col">ID</th>
                  <th scope="col">生日</th>
                  <th scope="col">更新操作</th>
                  <th scope="col">删除操作</th>
                </tr>
              </thead>
              <tbody>
              	 <% 
              	 String where1 = " where ";
              	if(aca!=null && "".equals(aca)!=true){
              		where1 +="ac like '%"+aca+"%' and ";
              	}if(namea!=null && "".equals(namea)!=true){
              		where1 +="name like '%"+namea+"%' and ";
              	}
              	where1+=" true ";
               	Connection con = DB.ConnectPool.getInstance().getConnection();
				Statement st = con.createStatement();
				ResultSet rs = st.executeQuery(" select * from userman "+where1); 
				while(rs.next()){
					out.print("<tr>");
					out.print("<th scope='row'>"+rs.getInt("ac")+"</th>");
					out.print("<td >"+rs.getString("name")+"</td>");
					out.print("<td >"+rs.getString("pw")+"</td>");
					out.print("<td >"+((rs.getString("sex").equals("F")==true?"女":"男"))+"</td>");
					out.print("<td >"+rs.getString("id")+"</td>");
					out.print("<td >"+rs.getDate("date")+"</td>");
					out.print("<td><a href='user_update.jsp?uid="+rs.getInt("ac")+"'><button type='button' value="+rs.getInt("ac")+" class='btn btn-success'>更新</button></a></td>");
					out.print("<td><a href='DeleteUser?uid="+rs.getInt("ac")+"'><button type='button' value="+rs.getInt("ac")+" class='btn btn-danger'>删除</button></a></td>");
					out.print("</tr>");
				}
                %>
                           
              </tbody>
            </table>
            </form>
          </div>
          
          <%--订单管理 --%>
          <% 
          		String acc = request.getParameter("ac");
              	String namee = request.getParameter("namee");
              	String tnamee = request.getParameter("tname");
              	String keywordd = request.getParameter("keyword");
              	String gotimee = request.getParameter("gotime");
              	String ordertimee = request.getParameter("ordertime");
              	String peoplee = request.getParameter("people");
              	String telephonee = request.getParameter("telephone");
              	String moneyy = request.getParameter("money");
           		if(acc==null)
              		acc="";
           		if(namee==null)
              		namee="";
           		if(tnamee==null)
              		tnamee="";
           		if(keywordd==null)
              		keywordd="";
           		if(gotimee==null)
              		gotimee="";
           		if(ordertimee==null)
              		ordertimee="";
           		if(peoplee==null)
              		peoplee="";
           		if(telephonee==null)
              		telephonee="";
           		if(moneyy==null)
              		moneyy="";
          %>
          <div class="book-detial hidden" id="bd">
          <form action="admin.jsp?f=2" method="post">
            <table class="table">
              <thead class="thead-light">
                <tr>
                  <th scope="col"> 
                  <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	用户账号
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a1' name = "ac" placeholder="Search" value="<%=acc %>" aria-label="Search">
                               <button type="submit" id="s1" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b1' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"> <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	姓名
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a2' name = "namee" placeholder="Search" value="<%=namee%>"  aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b2' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"> <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	旅游名称
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a3' name = "tname" placeholder="Search" value="<%=tnamee %>" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b3' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	分类
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a4' name = "keyword" value="<%=keywordd %>"  placeholder="Search" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b4' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	出发时间
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a5' name = "gotime" placeholder="Search" value="<%=gotimee %>" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b5' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	预约时间
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a6' name = "ordertime" placeholder="Search" value="<%=ordertimee %>"   aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b6' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	人数
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a7' name = "people" placeholder="Search" value="<%=peoplee %>" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b7' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	联系方式
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a8' name = "telephone" placeholder="Search" value="<%=telephonee %>" aria-label="Search">
                               <button type="submit"  class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b8' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col"><div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            	金额
                          </button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               <input class="form-control mr-sm-1" type="search" id='a9' name = "money" placeholder="Search" value="<%=moneyy %>" aria-label="Search">
                               <button type="submit" class="btn btn-outline-success my-2 my-sm-0">搜索</button>
                               <button type="submit" id='b9' class="btn btn-outline-success my-2 my-sm-0">清空</button>
                          </div>
                        </div></th>
                  <th scope="col">更新操作</th>
                  <th scope="col">删除操作</th>
                </tr>
              </thead>
              <tbody>
              <% 
              	
              	String where = " where ";
              	if(acc!=null && "".equals(acc)!=true){
              		where +="ac like '%"+acc+"%' and ";
              	}if(namee!=null && "".equals(namee)!=true){
              		where +="name like '%"+namee+"%' and ";
              	}
              	if(tnamee!=null && "".equals(tnamee)!=true){
              		where +="tname like '%"+tnamee+"%' and ";
              	}
              	if(keywordd!=null && "".equals(keywordd)!=true){
              		where +="keyword like '%"+keywordd+"%' and ";
              	}
              	if(gotimee!=null && "".equals(gotimee)!=true){
              		where +="gotime like '%"+gotimee+"%' and ";
              	}
              	if(peoplee!=null && "".equals(peoplee)!=true){
              		where +="people like '%"+peoplee+"%' and ";
              	}
              	if(telephonee!=null && "".equals(telephonee)!=true){
              		where +="telephone like '%"+telephonee+"%' and ";
              	}if(moneyy!=null && "".equals(moneyy)!=true){
              		where +="money like '%"+moneyy+"%' and ";
              	}
              	where += " true ";
				rs = st.executeQuery(" select * from orderman "+where+" order by ordertime;"); 
				while(rs.next()){
					out.print("<tr>");
					out.print("<th scope='row'>"+rs.getInt("ac")+"</th>");
					out.print("<td >"+rs.getString("name")+"</td>");
					out.print("<td >"+rs.getString("tname")+"</td>");
					out.print("<td >"+rs.getString("keyword")+"</td>");
					out.print("<td >"+rs.getString("gotime")+"</td>");
					out.print("<td >"+rs.getString("ordertime")+"</td>");
					out.print("<td >"+rs.getInt("people")+"</td>");
					out.print("<td >"+rs.getString("telephone")+"</td>");
					out.print("<td >"+rs.getDouble("money")+"</td>");
					
					out.print("<td><a href='order_update.jsp?uid="+rs.getInt("ac")+"&tid="+rs.getInt("tid")+"&date="+rs.getString("gotime")+"'> <button type='button' value="+rs.getInt("ac")+" class='btn btn-success'>更新</button></a></td>");
					out.print("<td><a href='DeleteOrder?uid="+rs.getInt("ac")+"&tid="+rs.getInt("tid")+"&date="+rs.getString("gotime")+"'><button type='button' value="+rs.getInt("ac")+" class='btn btn-danger'>删除</button></a></td>");
					out.print("</tr>");
				}
                %>
                             
              </tbody>
            </table>
           </form>
          </div>
         
          <%--旅游方案管理 --%>
          <div class="line-detial hidden" id="ld">
              
              <table class="table">
                  <thead class="thead-light">
                    <tr>
                      <th scope="col">路线ID</th>
                      <th scope="col">旅游名称</th>
                      <th scope="col">关键字</th>
                      <th scope="col">出发时间 </th>
                      <th scope="col">总人数 </th>
                      <th scope="col">起始 </th>
                      <th scope="col">结束 </th>
                      <th scope="col">报名人数 </th>
                      <th scope="col">单价 </th>
                      <th scope="col">更新操作</th>
                      <th scope="col">删除操作</th>
                      <th><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter"> 新增</button></th>
                    </tr>
                    
                  </thead>
                  
                  <tbody>
                  
                    <% 
				rs = st.executeQuery("select t.travelid tid,t.name tname,t.keyword keyword ,t.time time,t.sum sum,t.start start,t.end end,t.people people,t.fee fee from traveltable t;"); 
				while(rs.next()){
					out.print("<tr>");
					out.print("<th scope='row'>"+rs.getInt("tid")+"</th>");
					out.print("<td >"+rs.getString("tname")+"</td>");
					out.print("<td >"+rs.getString("keyword")+"</td>");
					out.print("<td >"+rs.getString("time")+"</td>");
					out.print("<td >"+rs.getInt("sum")+"</td>");
					out.print("<td >"+rs.getString("start")+"</td>");
					out.print("<td >"+rs.getString("end")+"</td>");
					out.print("<td >"+rs.getInt("people")+"</td>");
					out.print("<td >"+rs.getDouble("fee")+"</td>");
					out.print("<td><a href='travel_update.jsp?tid="+rs.getInt("tid")+"'><button type='button' value="+rs.getInt("tid")+" class='btn btn-success'>更新</button></a></td>");
					out.print("<td><a href='DeleteTravel?tid="+rs.getInt("tid")+"'><button type='button' value="+rs.getInt("tid")+" class='btn btn-danger'>删除</button></a></td>");
					out.print("</tr>");
				}
                %>

                  </tbody>
                </table>
          </div>    
          
          <%--报表管理 --%>
          <div class="form-detial hidden" id="fd">
			待完善
          </div>
        </div>
      </div>
    </div>
    
  <!-- Modal -->
  <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLongTitle">新增旅游方案</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
	<form action="UploadImage"  method="post" ENCTYPE="multipart/form-data" >
        <div class="modal-body">
            <div class="input-group mb-3">
               <div class="custom-file">
                 <input type="file" class="custom-file-input" id="inputGroupFile02" name = "myimage">
                 <label class="custom-file-label" for="inputGroupFile02">点此上传图片</label>
               </div>
              </div>
                  <div class="form-row">
                    <div class="form-group col-md-6">
                      <label for="inputEmail4">旅游名称</label>
                      <input type="text" name="tname" class="form-control" id="inputid0" placeholder="该旅行的名字">
                    </div>
                    <div class="form-group col-md-6">
                      <label for="inputPassword4">关键字</label>
                      <div class="input-group mb-3">
						  <select class="custom-select" name='keyword' id="inputGroupSelect02">
						    <option selected>类型</option>
						    <option value="国内">国内</option>
						    <option value="国外">国外</option>
						    <option value="自由">自由</option>
						    <option value="港澳台">港澳台</option>
						  </select>
						</div>
                    </div>
                  </div>
                  <div class="form-row">
                      <div  class="form-group col-md-6">
                          <label for="inputphone">起始点</label>
                          <input type="text" name="start" class="form-control" id="inputname" placeholder="出发地点">
                      </div> 
                      <div  class="form-group col-md-6">
                          <label for="inputphone">目的地</label>
                          <input type="text" name="end" class="form-control" id="inputphone" placeholder="目的地点">
                      </div> 
                  </div>
                  <div class="form-row">
                    <div  class="form-group col-md-4">
                        <label for="inputbir">开始日期</label>
                        <input type="date" name="date" class="form-control" id="inputbir" placeholder="">
                    </div>
                    <div  class="form-group col-md-4">
                        <label for="inputid">单价</label>
                        <input type="text" name="price" class="form-control" id="inputid" placeholder="每个成人票价">
                    </div>
                    <div  class="form-group col-md-4">
                        <label for="inputid">人数</label>
                        <input type="text" name="sum" class="form-control" id="inputid" placeholder="最大容纳人数">
                    </div>
                  </div>
                  
				  <div class="form-row">
                    <div  class="form-group col-md-12">
                        <label for="inputbir">简介</label>
                        <input type="text" name="remark" class="form-control" id="inputbir" placeholder="简单介绍几句，">
                    </div>
                  </div>

        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">取消</button>
          <button type="submit" class="btn btn-primary">上传</button>
        </div>
     </form>  
        
      </div>
    </div>
  </div>

<script type="text/javascript">
	  	
	  	var b1a = document.getElementById('b1a');
	  	var b2a = document.getElementById('b2a');
	  	b1a.onclick = function(){
	  	    a1a.value="";
         }
         b2a.onclick = function(){
	  	    a2a.value="";
         }
	  	var b1 = document.getElementById('b1');
	  	var b2 = document.getElementById('b2');
	  	var b3 = document.getElementById('b3');
	  	var b4 = document.getElementById('b4');
	  	var b5 = document.getElementById('b5');
	  	var b6 = document.getElementById('b6');
	  	var b7 = document.getElementById('b7');
	  	var b8 = document.getElementById('b8');
	  	var b9 = document.getElementById('b9');
	  	b1.onclick = function(){
	  	    a1.value="";
         }
         b2.onclick = function(){
	  	    a2.value="";
         }
         b3.onclick = function(){
	  	    a3.value="";
         }
         b4.onclick = function(){
	  	    a4.value="";
         }
         b5.onclick = function(){
	  	    a5.value="";
         }
         b6.onclick = function(){
	  	    a6.value="";
         }
         b7.onclick = function(){
	  	    a7.value="";
         }
         b8.onclick = function(){
	  	    a8.value="";
         }
	  	 b9.onclick = function(){
	  	    a9.value="";
         }
</script>
      
<script>
         var bookDetial = document.getElementById('book');
         var singleDetial = document.getElementById('single');
         var lineDetial = document.getElementById('line');
         var formDetial = document.getElementById('form');
         var line = document.getElementById('ld');
         var form = document.getElementById('fd');
         var book = document.getElementById('bd');
         var single = document.getElementById('sd');
         
         function getQueryString(name){
          var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
          var r = window.location.search.substr(1).match(reg);
          if(r!=null)return  unescape(r[2]); return null;
      	  }
      	 var flag = getQueryString("f");
      	 
         switch(flag){
         	case "1": {
	     		singleDetial.className = 'list-group-item list-group-item-action active';
	            bookDetial.className = 'list-group-item list-group-item-action';
	            formDetial.className = 'list-group-item list-group-item-action';
	            lineDetial.className = 'list-group-item list-group-item-action ';
	            book.className = 'book-detail hidden';
	            single.className = 'single-detial show';
	            form.className = 'form-detial hidden';
	            line.className = 'line-detial hidden';
         	}break;
         	case "2": {
	     		singleDetial.className = 'list-group-item list-group-item-action';
	            bookDetial.className = 'list-group-item list-group-item-action active';
	            formDetial.className = 'list-group-item list-group-item-action';
	            lineDetial.className = 'list-group-item list-group-item-action ';
	            book.className = 'book-detail show';
	            single.className = 'single-detial hidden';
	            form.className = 'form-detial hidden';
	            line.className = 'line-detial hidden';
         	}break;
         	case "3": {
	     		singleDetial.className = 'list-group-item list-group-item-action';
	            bookDetial.className = 'list-group-item list-group-item-action';
	            formDetial.className = 'list-group-item list-group-item-action';
	            lineDetial.className = 'list-group-item list-group-item-action active';
	            book.className = 'book-detail hidden';
	            single.className = 'single-detial hidden';
	            form.className = 'form-detial hidden';
	            line.className = 'line-detial show';
         	}break;
         	case "4": {
	     		singleDetial.className = 'list-group-item list-group-item-action';
	            bookDetial.className = 'list-group-item list-group-item-action';
	            formDetial.className = 'list-group-item list-group-item-action active';
	            lineDetial.className = 'list-group-item list-group-item-action ';
	            book.className = 'book-detail hidden';
	            single.className = 'single-detial hidden';
	            form.className = 'form-detial show';
	            line.className = 'line-detial hidden';
         	}break;
         	default:
         	window.location.href = "admin.jsp?f=1";
         	break;
         	
         }
         singleDetial.onclick = function(){
           window.location.href = "admin.jsp?f=1";
         }
         bookDetial.onclick = function(){
             window.location.href = "admin.jsp?f=2";
         }
         lineDetial.onclick = function(){
            window.location.href = "admin.jsp?f=3";
         }
         formDetial.onclick = function(){
            window.location.href = "admin.jsp?f=4";
        }
         
</script>



</body>
</html>