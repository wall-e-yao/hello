package controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;


import model.UserFunction;


@SuppressWarnings("serial")
public class UpdateOrder extends HttpServlet {

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
	}
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		response.setCharacterEncoding("utf-8");
		request.setCharacterEncoding("utf-8");
		HttpSession session = request.getSession();
		
		Integer account = (Integer)session.getAttribute("account");
		String password = request.getParameter("password");
		String name = request.getParameter("name");
		String id = request.getParameter("id");
		String tele = request.getParameter("telephone");
		String sex = request.getParameter("sex");
		String birthday = request.getParameter("birthday");
		PrintWriter out = response.getWriter();
		
		DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
		List<Object> list = userDao.GetBy(" account = '"+account+"'");
		out.println(list.size());
		valueobject.User user = DB.ObjectFactory.getUser();
		user = (valueobject.User)list.get(0);
		try {
			user.setAccount(account);
			user.setBirthday(otherclass.TimeConvert.strToDate(birthday));
			user.setId(id);
			user.setName(name);
			user.setPassword(password);
			user.setSex(sex);
			user.setTelephone(tele);
		} catch (Exception e) {
			// TODO: handle exception
			request.setAttribute("errormsg", "填写信息错误！");
			request.getRequestDispatcher("single.jsp?uid="+account).forward(request, response);
			return ;
		}
		
		int flag = userDao.Update(user);

		if(flag==userDao.OPERATE_SUCCESS){
			request.setAttribute("errormsg", "修改信息成功！");
			if(user.getFlag()==0)
				request.getRequestDispatcher("single.jsp").forward(request, response);
			else if(user.getFlag()==1){
				request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			}
			return ;
		}else if(flag==userDao.OPERATE_ERROR){
			request.setAttribute("errormsg", "修改失败！");
			if(user.getFlag()==0)
				request.getRequestDispatcher("single.jsp").forward(request, response);
			else if(user.getFlag()==1){
				request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			}
			return ;
		}else{
			request.setAttribute("errormsg", "内部其他错误，请联系管理员！");
			if(user.getFlag()==0)
				request.getRequestDispatcher("single.jsp").forward(request, response);
			else if(user.getFlag()==1){
				request.getRequestDispatcher("admin.jsp?f=1").forward(request, response);
			}
			return ;
		}
	}

	public void init() throws ServletException {
		// Put your code here
	}
}
