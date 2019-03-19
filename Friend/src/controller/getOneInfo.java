package controller;

import java.io.IOException;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class getOneInfo extends HttpServlet {

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
		request.setCharacterEncoding("utf-8");
		response.setCharacterEncoding("utf-8");
		String id = (String)request.getParameter("update");
		if(id==null||id.length()==0){
			request.setAttribute("errormsg", "没有选中您的好友！");
			request.getRequestDispatcher("QueryAllFriend").forward(request, response);
			return ;
		}
		DAO.all_interface dao = DB.ObjectFactory.getFriendDao();
		List<Object> frd = dao.GetBy(" id = '"+id+"'");
		valueobject.Friend friend = (valueobject.Friend)frd.get(0);
		request.setAttribute("updatefriend", friend);
		request.getRequestDispatcher("updateForm.jsp").forward(request, response);
		return ;
	}

}
