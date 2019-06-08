package controller;

import java.io.IOException;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import valueobject.User;

import model.UserFunction;


@SuppressWarnings("serial")
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
		
		String username = request.getParameter("account");
		String password = request.getParameter("password");
		if(username==null||username.length()<=0){
			request.setAttribute("errormsg", "用户名为空！");
			request.getRequestDispatcher("login.jsp").forward(request, response);
			return ;
		}
		if(password==null||password.length()<=0){
			request.setAttribute("errormsg", "密码为空！");
			request.getRequestDispatcher("login.jsp").forward(request, response);
			return ;
		}
		
		UserFunction model = DB.ObjectFactory.getUserFunction();
		int account = 0;
		try {
			account = Integer.parseInt(username);
		} catch (Exception e) {
			// TODO: handle exception
			request.setAttribute("errormsg", "用户名错误");
			request.getRequestDispatcher("login.jsp").forward(request, response);
			return ;
		}
		
		
		
		int flag = model.Login(account, password);
		
		
		if(flag==UserFunction.LOGIN_OK){
			HttpSession session = request.getSession();
			session.setAttribute("account", account);
			DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
			List<Object> list = userDao.GetBy("account = "+account);
			User user = (User)list.get(0);
			session.setAttribute("name", user.getName());
			response.sendRedirect("index.jsp");
			return ;
		}else if(flag==UserFunction.LOGIN_FAILED){
			request.setAttribute("errormsg", "用户名错误或密码错误！");
			request.getRequestDispatcher("login.jsp").forward(request, response);
			return ;
		}else if(flag==UserFunction.MANAGER_LOGIN_OK){
			HttpSession session = request.getSession();
			session.setAttribute("account", account);
			DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
			List<Object> list = userDao.GetBy("account = "+account);
			User user = (User)list.get(0);
			request.setAttribute("errormsg", "尊敬的管理员:"+user.getName()+"，欢迎您！");
			request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			return ;
		}else{
			request.setAttribute("errormsg", "内部其他错误，请联系管理员！");
			request.getRequestDispatcher("login.jsp").forward(request, response);
			return ;
		}
	}

	public void init() throws ServletException {
		// Put your code here
	}
}
