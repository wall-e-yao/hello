package controller;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

public class Login extends HttpServlet {

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
	}
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		response.setCharacterEncoding("utf-8");
		request.setCharacterEncoding("utf-8");
		String username = request.getParameter("username");
		String password = request.getParameter("password");
		if(username==null||username.length()<=0){
			request.setAttribute("errormsg", "用户名为空！");
			request.getRequestDispatcher("LoginForm.jsp").forward(request, response);
			return ;
		}
		if(password==null||password.length()<=0){
			request.setAttribute("errormsg", "密码为空！");
			request.getRequestDispatcher("LoginForm.jsp").forward(request, response);
			return ;
		}
		model.Functions model = DB.ObjectFactory.getModelFunction();
		int flag = model.Login(username, password);
		if(flag==model.LOGIN_OK){
			HttpSession session = request.getSession();
			session.setAttribute("username", username);
			response.sendRedirect("index.jsp");
			return ;
		}else if(flag==model.USERNAME_ERROR){
			request.setAttribute("errormsg", "用户名错误！");
			request.getRequestDispatcher("LoginForm.jsp").forward(request, response);
			return ;
		}else if(flag==model.PASSWORD_ERROR){
			request.setAttribute("errormsg", "密码错误！");
			request.getRequestDispatcher("LoginForm.jsp").forward(request, response);
			return ;
		}else if(flag==model.OTHER_ERROR){
			request.setAttribute("errormsg", "内部错误！，请联系管理员！");
			request.getRequestDispatcher("LoginForm.jsp").forward(request, response);
			return ;
		}
	}

	public void init() throws ServletException {
		// Put your code here
	}
}
