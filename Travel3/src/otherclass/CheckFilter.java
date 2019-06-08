package otherclass;
import java.io.IOException;
import javax.servlet.*;
import javax.servlet.http.*;


public class CheckFilter implements Filter {
    public void doFilter(ServletRequest request, ServletResponse response,   
            FilterChain chain) throws IOException, ServletException {   
        HttpServletRequest hreq=(HttpServletRequest) request;   
        HttpServletResponse hres=(HttpServletResponse) response;   
        HttpSession session = hreq.getSession();     
        String add1 = hreq.getContextPath()+"/register.jsp";
        String add2 = hreq.getContextPath()+"/Login";
        String add4 = hreq.getContextPath()+"/Register";
        String add3 = hreq.getContextPath()+"/login.jsp";
        String add5 = hreq.getContextPath()+"/Logout";
        if(session!=null&&session.getAttribute("account")!=null)
        	chain.doFilter(request, response); 
    	//请求地址为登陆表单和登陆验证页面，同样正常处理请求链内容
        
        
    	else if(add1.equals(hreq.getRequestURI())||add2.equals(hreq.getRequestURI())||add3.equals(hreq.getRequestURI())||add4.equals(hreq.getRequestURI())||add5.equals(hreq.getRequestURI())) 
    		
    		chain.doFilter(request, response);   
    	//请求非法，一律转向登陆表单页面
    	else {
    		request.getRequestDispatcher("login.jsp").forward(request, response);
    		//hres.sendRedirect(hreq.getContextPath()+"/pages/login.jsp");
    	}
    }

	public void destroy() {
		// TODO Auto-generated method stub
	}

	public void init(FilterConfig arg0) throws ServletException {
		// TODO Auto-generated method stub
		
	}  
}
