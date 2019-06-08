package controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.*;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import DAO.OrderDao;

import otherclass.TimeConvert;

public class Pay extends HttpServlet {

	/**
	 * Constructor of the object.
	 */
	public Pay() {
		super();
	}

	/**
	 * Destruction of the servlet. <br>
	 */
	public void destroy() {
		super.destroy(); // Just puts "destroy" string in log
		// Put your code here
	}

	public void doDelete(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {

		// Put your code here
	}


	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		doPost(request,response);
	}

	/**
	 * The doPost method of the servlet. <br>
	 *
	 * This method is called when a form has its tag value method equals to post.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		response.setCharacterEncoding("utf-8");
		request.setCharacterEncoding("utf-8");
		
		PrintWriter out = response.getWriter();
		
		String account = request.getParameter("account");
		String adult = request.getParameter("adult");
		String kid = request.getParameter("kid");
		String money = request.getParameter("money");
		String time = request.getParameter("time");
		String tid = request.getParameter("tid");
		System.out.println(account);		
		System.out.println(adult);		
		System.out.println(kid);		
		System.out.println(money);		
		int people = Integer.parseInt(adult)+Integer.parseInt(kid);
		
		DAO.DaoInterface travelDao = DB.ObjectFactory.getTravelDao();
     	List<Object> list = travelDao.GetBy("travelid="+tid);
     	valueobject.Travel travel = (valueobject.Travel)list.get(0); 
     	
     	if((travel.getPeople()+people) > travel.getSum()){
     		request.setAttribute("errormsg", "不好意思，预约失败 ，人员已满！");
     		request.getRequestDispatcher("book.jsp?tid="+tid).forward(request, response);
     		return ;
     	}else{//成功预约
     		
     		DAO.DaoInterface orderDao = DB.ObjectFactory.getOrderDao();
     		valueobject.Order order = DB.ObjectFactory.getOrder();

     		List<Object> orderList = orderDao.GetBy("account = '"+account+"' and travelid = '"+tid+"' and date='"+time+"'");
     		if(orderList.size()==0){
     			travel.setPeople(travel.getPeople()+people);
         		travelDao.Update((Object)travel);
         		
         		order.setAccount(Integer.parseInt(account));
         		order.setState(0);
         		order.setTime(TimeConvert.dateToStr(TimeConvert.getSystemTime()));
         		order.setTravelid(Integer.parseInt(tid));
         		order.setKid(Integer.parseInt(kid));
         		order.setAdult(Integer.parseInt(adult));
         		order.setMoney(Double.parseDouble(money));
         		order.setDate(time);
         		
         		out.print(orderDao.Add((Object)order));
         		request.setAttribute("errormsg", "恭喜你，预约成功！");
         		request.getRequestDispatcher("book.jsp?tid="+tid).forward(request, response);
         		return ;
     		}
     		else{
     			request.setAttribute("errormsg", "您已经预约过相同时间的该订单");
         		request.getRequestDispatcher("book.jsp?tid="+tid).forward(request, response);
         		return;
     		}
     		
     	}
     	
//		out.println(name);
//		out.println(account);
//		out.println(money);
//		out.println(time);
//		out.println(people);
	}

	/**
	 * The doPut method of the servlet. <br>
	 *
	 * This method is called when a HTTP put request is received.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doPut(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		// Put your code here
	}

	/**
	 * Returns information about the servlet, such as 
	 * author, version, and copyright. 
	 *
	 * @return String information about this servlet
	 */
	public String getServletInfo() {
		return "将支付记录写入数据库！";
	}

	/**
	 * Initialization of the servlet. <br>
	 *
	 * @throws ServletException if an error occurs
	 */
	public void init() throws ServletException {
		// Put your code here
	}

}
