package controller;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class DeleteFriends extends HttpServlet {

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
		String[] ids =null;
		ids = (String[])request.getParameterValues("delete");
		if(ids==null){
			request.setAttribute("errormsg", "没有选中您的好友！");
			request.getRequestDispatcher("QueryAllFriend").forward(request, response);
			return ;
		}
		model.Functions model = DB.ObjectFactory.getModelFunction();
		for(int i = 0;i<ids.length;i++){
			int id = Integer.parseInt(ids[i]);
			model.Delete_Friend(id);
		}
		request.setAttribute("errormsg", "成功删除好友！");
		request.getRequestDispatcher("QueryAllFriend").forward(request, response);
		return ;
	}

}
