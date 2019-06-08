package DB;

import model.UserFunction;
import valueobject.*;
import DAO.DaoInterface;

public class ObjectFactory{
	 
	public static User getUser(){
		try {
			//java ����
			return (valueobject.User) Class.forName("valueobject.User").newInstance();
		} catch (Exception e) {
			System.out.print("User �����쳣��");
			return null;
		}
		//return new valueobject.User();
	}
		
	public static Order getOrder(){
		try {
			return (Order) Class.forName("valueobject.Order").newInstance();
		} catch (Exception e) {
			System.out.print("Order �����쳣��");
			return null;
		}
	}
	
	public static Trace getTrace(){
		try {
			//java ����
			return (Trace) Class.forName("valueobject.Trace").newInstance();
		} catch (Exception e) {
			System.out.print("Trace �����쳣��");
			return null;
		}
	}
	
	public static Travel getTravel(){
		try {
			return (Travel) Class.forName("valueobject.Travel").newInstance();
		} catch (Exception e) {
			// TODO: handle exception
			System.out.print("Travel �����쳣��");
			return null;
		}
	}
	
	public static DaoInterface  getUserDao(){
		try {
			//java ����
			return (DaoInterface)Class.forName("DAO.UserDao").newInstance();
		} catch (Exception e) {
			System.out.print("UserDao �쳣��");
			return null;
		}
	}
	
	public static DaoInterface  getTraceDao(){
		try {
			//java ����
			return (DaoInterface)Class.forName("DAO.TraceDao").newInstance();
		} catch (Exception e) {
			System.out.print("TraceDao �쳣��");
			return null;
		}
	}

	public static DaoInterface  getOrderDao(){
		try {
			//java ����
			return (DaoInterface)Class.forName("DAO.OrderDao").newInstance();
		} catch (Exception e) {
			System.out.print("OrderDao �쳣��");
			return null;
		}
	}

	public static DaoInterface  getTravelDao(){
		try {
			//java ����
			return (DaoInterface)Class.forName("DAO.TravelDao").newInstance();
		} catch (Exception e) {
			System.out.print("TravelDao �쳣��");
			return null;
		}
	}

	public static UserFunction  getUserFunction(){
		try {
			//java ����
			return (UserFunction)Class.forName("model.UserImplement").newInstance();
		} catch (Exception e) {
			System.out.print("UserImplement �쳣��");
			return null;
		}
	}
	
	
}