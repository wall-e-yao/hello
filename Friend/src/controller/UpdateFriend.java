package controller;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import valueobject.Friend;

public class UpdateFriend extends HttpServlet {

	/**
	 * The doGet method of the servlet. <br>
	 *
	 * This method is called when a form has its tag value method equals to get.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		this.doPost(request, response);
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
		
		String id = request.getParameter("id");
		String userid = request.getParameter("userid");
		String name = request.getParameter("name");
		String sex = request.getParameter("sex");
		String age = request.getParameter("age");
		String qq = request.getParameter("qq");
		String tele = request.getParameter("telephone");
		String email = request.getParameter("email");
		String address = request.getParameter("address");
		Friend f = DB.ObjectFactory.getFriendObject();
		f.setUserid(Integer.parseInt(userid));
		f.setAddress(address);
		f.setAge(Integer.parseInt(age));
		f.setEmail(email);
		f.setId(Integer.parseInt(id));
		f.setName(name);
		f.setQq(qq);
		f.setSex(sex);
		f.setTelephone(tele);
		
		model.Functions model = DB.ObjectFactory.getModelFunction();
		int i = model.Update_Friend(f);
		if(i==model.OPERATE_OK){
			request.setAttribute("errormsg", "成功更新好友！");
			request.getRequestDispatcher("QueryAllFriend").forward(request, response);
			return ;
		}else if(i==model.OPERATE_ERROR){
			request.setAttribute("errormsg", "更新好友信息失败！你等会再来试试。");
			request.getRequestDispatcher("QueryAllFriend").forward(request, response);
			return ;
		}else{
			request.setAttribute("errormsg", "出现内部错误，请联系管理员！");
			request.getRequestDispatcher("QueryAllFriend").forward(request, response);
			return ;
		}
		
		
	}

}
