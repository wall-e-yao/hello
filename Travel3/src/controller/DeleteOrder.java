package controller;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import DAO.DaoInterface;


@SuppressWarnings("serial")
public class DeleteOrder extends HttpServlet {

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
	}
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		response.setCharacterEncoding("utf-8");
		request.setCharacterEncoding("utf-8");
		
		String uid = request.getParameter("uid");
		String tid = request.getParameter("tid");
		String date = request.getParameter("date");
		DAO.DaoInterface orderDao = DB.ObjectFactory.getOrderDao();
		valueobject.Order order = DB.ObjectFactory.getOrder();
		order.setAccount(Integer.parseInt(uid));
		order.setDate(date);
		order.setTravelid(Integer.parseInt(tid));
		int flag = orderDao.Delete((Object)order);
		System.out.println(flag);
		if(flag==orderDao.DB_CONNECTION_ERROR){
			request.setAttribute("errormsg", "连接数据库错误！");
			request.getRequestDispatcher("admin.jsp?f=2").forward(request, response);
			return ;
		}else if(flag==DaoInterface.OPERATE_ERROR){
			request.setAttribute("errormsg", "无法删除！");
			request.getRequestDispatcher("admin.jsp?f=2").forward(request, response);
			return ;
		}else if(flag==orderDao.OPERATE_SUCCESS)
		{
			request.setAttribute("errormsg", "delete success！");
			request.getRequestDispatcher("admin.jsp?f=2").forward(request, response);
			return ;
		}
	}

	public void init() throws ServletException {
		// Put your code here
	}
}




