package controller;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;


@SuppressWarnings("serial")
public class DeleteUser extends HttpServlet {

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
		DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
		int flag = userDao.Delete((Object)Integer.parseInt(uid));
		System.out.println(flag);
		if(flag==userDao.DB_CONNECTION_ERROR){
			request.setAttribute("errormsg", "连接数据库错误！");
			request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			return ;
		}else if(flag==userDao.OPERATE_ERROR){
			request.setAttribute("errormsg", "无法删除！");
			request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			return ;
		}else if(flag==userDao.OPERATE_SUCCESS)
		{
			request.setAttribute("errormsg", "delete success！");
			request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			return ;
		}
		
	}

	public void init() throws ServletException {
		// Put your code here
	}
}




