package DB;

import model.UserFunction;
import valueobject.*;
import DAO.DaoInterface;

public class ObjectFactory{
	 
	public static User getUser(){
		try {
			//java 反射
			return (valueobject.User) Class.forName("valueobject.User").newInstance();
		} catch (Exception e) {
			System.out.print("User 工厂异常！");
			return null;
		}
		//return new valueobject.User();
	}
		
	public static Order getOrder(){
		try {
			return (Order) Class.forName("valueobject.Order").newInstance();
		} catch (Exception e) {
			System.out.print("Order 工厂异常！");
			return null;
		}
	}
	
	public static Trace getTrace(){
		try {
			//java 反射
			return (Trace) Class.forName("valueobject.Trace").newInstance();
		} catch (Exception e) {
			System.out.print("Trace 工厂异常！");
			return null;
		}
	}
	
	public static Travel getTravel(){
		try {
			return (Travel) Class.forName("valueobject.Travel").newInstance();
		} catch (Exception e) {
			// TODO: handle exception
			System.out.print("Travel 工厂异常！");
			return null;
		}
	}
	
	public static DaoInterface  getUserDao(){
		try {
			//java 反射
			return (DaoInterface)Class.forName("DAO.UserDao").newInstance();
		} catch (Exception e) {
			System.out.print("UserDao 异常！");
			return null;
		}
	}
	
	public static DaoInterface  getTraceDao(){
		try {
			//java 反射
			return (DaoInterface)Class.forName("DAO.TraceDao").newInstance();
		} catch (Exception e) {
			System.out.print("TraceDao 异常！");
			return null;
		}
	}

	public static DaoInterface  getOrderDao(){
		try {
			//java 反射
			return (DaoInterface)Class.forName("DAO.OrderDao").newInstance();
		} catch (Exception e) {
			System.out.print("OrderDao 异常！");
			return null;
		}
	}

	public static DaoInterface  getTravelDao(){
		try {
			//java 反射
			return (DaoInterface)Class.forName("DAO.TravelDao").newInstance();
		} catch (Exception e) {
			System.out.print("TravelDao 异常！");
			return null;
		}
	}

	public static UserFunction  getUserFunction(){
		try {
			//java 反射
			return (UserFunction)Class.forName("model.UserImplement").newInstance();
		} catch (Exception e) {
			System.out.print("UserImplement 异常！");
			return null;
		}
	}
	
	
}