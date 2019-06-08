package controller;

import java.io.*;
import javax.servlet.*;
import javax.servlet.http.*;
import java.text.SimpleDateFormat;
import java.util.*;
import com.jspsmart.upload.*;

public class UpdateTravel extends HttpServlet {

	
	   ServletConfig config;
		/**
		 * Initialization of the servlet. <br>
		 *
		 * @throws ServletException if an error occurs
		 */
	   public void init(ServletConfig config) throws ServletException {
	       super.init(config);//初始化servlet，主要目的是得到初始化信息
	//将得到的config保存为成员变量，目的是下一步使用
	       this.config = config;
	   }
	
	
	/**
	 * Constructor of the object.
	 */
	public UpdateTravel() {
		super();
	}

	/**
	 * Destruction of the servlet. <br>
	 */
	public void destroy() {
		super.destroy(); // Just puts "destroy" string in log
		// Put your code here
	}

	/**
	 * The doDelete method of the servlet. <br>
	 *
	 * This method is called when a HTTP delete request is received.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doDelete(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {

		// Put your code here
	}

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
		PrintWriter out = response.getWriter();
		
		request.setCharacterEncoding("utf-8");
		response.setCharacterEncoding("utf-8");
		
        String msg="",fileName = null;
        SmartUpload uploader = new SmartUpload();
        
        
        try {
            uploader.initialize(config, request, response);// 初始化上载器
            uploader.upload();		// 上载表单数据
            
            
            String tid = uploader.getRequest().getParameter("tidd");
            String imageadd = uploader.getRequest().getParameter("imageadd");
            String tname = uploader.getRequest().getParameter("tname");
     		String keyword = uploader.getRequest().getParameter("keyword");
     		String start = uploader.getRequest().getParameter("start");
     		String end = uploader.getRequest().getParameter("end");
     		String date = uploader.getRequest().getParameter("date");
     		String price = uploader.getRequest().getParameter("price");
     		String sum = uploader.getRequest().getParameter("sum");
     		String people = uploader.getRequest().getParameter("people");
     		String remark = uploader.getRequest().getParameter("remark");
             
     		valueobject.Travel travel = DB.ObjectFactory.getTravel();
             try {
                 travel.setTravelid(Integer.parseInt(tid));
                 travel.setEnd(end);
                 travel.setFee(Double.parseDouble(price));
                 travel.setImageAdd(imageadd);
                 travel.setKeyword(keyword);
                 travel.setName(tname);
                 travel.setPeople(Integer.parseInt(people));
                 travel.setRemake(remark);
                 travel.setStart(start);
                 travel.setSum(Integer.parseInt(sum));
                 travel.setTime(date);
                 
     		} catch (Exception e1) {
     			// TODO: handle exception
     			msg="what data?";
     			request.setAttribute("errormsg",msg);
                request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
                return;
     		}
     		DAO.DaoInterface travelDao = DB.ObjectFactory.getTravelDao();
             int flag = travelDao.Update((Object)travel);
             if(flag==travelDao.DB_CONNECTION_ERROR){
             	request.setAttribute("errormsg","DB_CONNECTION_ERROR");	//存储提示信息
                 request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
                 return;
             }else if(flag==travelDao.OPERATE_SUCCESS){
             	request.setAttribute("errormsg","upload success");	//存储提示信息
                 request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
                 return;
             }else{
             	request.setAttribute("errormsg","other error!");	//存储提示信息
                 request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
                 return;
             }
             
        } catch (Exception e) {
            msg="image upload failed";
            request.setAttribute("errormsg",msg);
            request.getRequestDispatcher("admin.jsp?f=3").forward(request, response);
            return;
        }
        
        	
        
    }		

	/**
	 * The doPut method of the servlet. <br>
	 *
	 * This method is called when a HTTP put request is received.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doPut(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		// Put your code here
	}

	/**
	 * Returns information about the servlet, such as 
	 * author, version, and copyright. 
	 *
	 * @return String information about this servlet
	 */
	public String getServletInfo() {
		return "This is my default servlet created by Eclipse";
	}



}
