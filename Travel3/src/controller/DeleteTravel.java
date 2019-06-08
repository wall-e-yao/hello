package controller;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;


@SuppressWarnings("serial")
public class DeleteTravel extends HttpServlet {

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
	}
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		response.setCharacterEncoding("utf-8");
		request.setCharacterEncoding("utf-8");
		
		String tid = request.getParameter("tid");
		DAO.DaoInterface travelDao = DB.ObjectFactory.getTravelDao();
		int flag = travelDao.Delete((Object)Integer.parseInt(tid));
		System.out.println(flag);
		if(flag==travelDao.DB_CONNECTION_ERROR){
			request.setAttribute("errormsg", "连接数据库错误！");
			request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
			return ;
		}else if(flag==travelDao.OPERATE_ERROR){
			request.setAttribute("errormsg", "delete failed");
			request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
			return ;
		}else if(flag==travelDao.OPERATE_SUCCESS)
		{
			request.setAttribute("errormsg", "delete success！");
			request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
			return ;
		}
		
	}

	public void init() throws ServletException {
		// Put your code here
	}
}




