package controller;

import java.io.IOException;


import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import otherclass.TimeConvert;

import DAO.DaoInterface;

import valueobject.User;

@SuppressWarnings("serial")
public class Register extends HttpServlet {

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
	}

	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		
		
		response.setContentType("text/html");
		request.setCharacterEncoding("utf-8");
		response.setCharacterEncoding("utf-8");
		

		int account;
		try {
			account =  Integer.parseInt(request.getParameter("username"));
			System.out.println(account);
		} catch (Exception e) {
			// TODO: handle exception
			request.setAttribute("errormsg", "用户名只能是数字！（0-8）位");
			request.getRequestDispatcher("register.jsp").forward(request, response);
			return ;
		}
		
		User user = DB.ObjectFactory.getUser();
		user.setAccount(account);
		user.setAddress(request.getParameter("address"));
		String bir = request.getParameter("birthday");
		
		System.out.println(bir);
		
		user.setBirthday(TimeConvert.strToDate(bir));
		user.setFlag(0);
		user.setId(request.getParameter("id"));
		user.setName(request.getParameter("name"));
		user.setPassword(request.getParameter("password1"));
		user.setProfession(request.getParameter("profession"));
		user.setSex(request.getParameter("sex"));
		user.setTelephone(request.getParameter("telephone"));
		
		DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
		
		int flag = userDao.Add(user);
		
		if(flag==DaoInterface.OPERATE_SUCCESS){
			response.sendRedirect("login.jsp");
			return ;
		}else if(flag==DaoInterface.OPERATE_ERROR){
			request.setAttribute("errormsg", "该用户名已经被占用！");
			request.getRequestDispatcher("register.jsp").forward(request, response);
			return ;
		}else if(flag == DaoInterface.DB_CONNECTION_ERROR){
			request.setAttribute("errormsg", "其他数据错误！");
			request.getRequestDispatcher("register.jsp").forward(request, response);
			return ;
		}else{
			request.setAttribute("errormsg", "注册过程中发生内部错误！请联系管理人员！");
			request.getRequestDispatcher("register.jsp").forward(request, response);
			return ;
		}
	}

}
