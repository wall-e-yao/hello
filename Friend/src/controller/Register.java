package controller;

import java.io.IOException;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class Register extends HttpServlet {

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
	}

	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		request.setCharacterEncoding("utf-8");
		response.setCharacterEncoding("utf-8");
		String username = request.getParameter("username");
		String password = request.getParameter("password1");
		model.Functions model = DB.ObjectFactory.getModelFunction();
		int flag = model.Add_User(username, password);
		if(flag==model.OPERATE_OK){
			response.sendRedirect("LoginForm.jsp");
			return ;
		}else if(flag==model.REGISTE_FAILED){
			request.setAttribute("errormsg", "该用户名已经被占用！");
			request.getRequestDispatcher("registerForm.jsp").forward(request, response);
			return ;
		}else if(flag == model.OPERATE_ERROR){
			request.setAttribute("errormsg", "注册操作错误！");
			request.getRequestDispatcher("registerForm.jsp").forward(request, response);
			return ;
		}else{
			request.setAttribute("errormsg", "注册过程中发生内部错误！请联系管理人员！");
			request.getRequestDispatcher("registerForm.jsp").forward(request, response);
			return ;
		}
	}

}
