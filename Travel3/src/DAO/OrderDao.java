package DAO;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.LinkedList;
import java.util.List;

import otherclass.TimeConvert;

import valueobject.Order;

public class OrderDao implements DaoInterface {

	public int Add(Object obj) {
		// TODO Auto-generated method stub
		Order order = DB.ObjectFactory.getOrder();
		if(obj instanceof Order){
			order = (Order)obj;
			try {
				
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("insert into orderTable(account,time,state,travelid,kid,adult,money,date) values(?,?,?,?,?,?,?,?)");
				
				ps.setInt(1, order.getAccount());
				ps.setString(2, order.getTime());
				ps.setInt(3,order.getState());
				ps.setInt(4,order.getTravelid());
				ps.setInt(5,order.getKid());
				ps.setInt(6,order.getAdult());
				ps.setDouble(7,order.getMoney());
				ps.setString(8,order.getDate());
				
				ps.executeUpdate();
				
				return OrderDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				System.out.println(e);
				return OrderDao.DB_CONNECTION_ERROR;
			}
		}else{
			return OrderDao.OPERATE_ERROR;
		}
	}

	public int Delete(Object key) {
		// TODO Auto-generated method stub
		if(key instanceof Order){
			Order i = (Order)key;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("delete from orderTable where account = ? and travelid = ? and date = ?");
				
				ps.setInt(1, i.getAccount());
				ps.setInt(2, i.getTravelid());
				ps.setString(3, i.getDate());
				
				ps.executeUpdate();
				
				return OrderDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return OrderDao.DB_CONNECTION_ERROR;
			}
		}else{
			return OrderDao.OPERATE_ERROR;
		}
	}

	public List<Object> GetAll() {
		// TODO Auto-generated method stub
		return GetBy("");
	}

	public List<Object> GetBy(String where) {
		// TODO Auto-generated method stub
		List<Object> result = new LinkedList<Object>();
		String sql = "select * from orderTable ";
		if(where!=null && where.length()>0){
			sql=sql + " where "+where;
		}
		try {
			Connection con = DB.ConnectPool.getInstance().getConnection();
			Statement st = con.createStatement();
			ResultSet rs = st.executeQuery(sql); 
			while(rs.next()){
				
				Order one = DB.ObjectFactory.getOrder();
				one.setAccount(rs.getInt("account"));
				one.setTime(rs.getString("time"));
				one.setState(rs.getInt("state"));
				one.setTravelid(rs.getInt("travelid"));
				one.setKid(rs.getInt("kid"));
				one.setAdult(rs.getInt("adult"));
				one.setMoney(rs.getDouble("money"));
				one.setDate(rs.getString("date"));
				result.add(one);
			}
		} catch (Exception e) {
			return result; 
		}
		return result;
	}

	public void Show(List<Object> result) {
		// TODO Auto-generated method stub
		for (Object object : result) {
			Order order = DB.ObjectFactory.getOrder();
			order = (Order)object;
			System.out.println("ÕËºÅ£º"+order.getAccount());
			System.out.println("ÂÃ³Ì£º"+order.getTravelid());
			System.out.println("Ê±¼ä£º"+order.getTime());
			System.out.println("×´Ì¬£º"+order.getState());
		}
		return ;
	}

	public int Update(Object obj) {
		// TODO Auto-generated method stub
		Order order = DB.ObjectFactory.getOrder();
		if(obj instanceof Order){
			order = (Order)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("update orderTable set time = ?,state = ?,kid = ?,adult = ?,money = ?,date = ? where account=? and travelid=?");
				
				ps.setString(1, order.getTime());
				ps.setInt(2, order.getState());
				ps.setInt(3, order.getKid());
				ps.setInt(4, order.getAdult());
				ps.setDouble(5, order.getMoney());
				ps.setString(6, order.getDate());
				ps.setInt(7, order.getAccount());
				ps.setInt(8, order.getTravelid());
				
				ps.executeUpdate();
				return OrderDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return OrderDao.DB_CONNECTION_ERROR;
			}
		}else{
			return OrderDao.OPERATE_ERROR;
		}
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Order order = DB.ObjectFactory.getOrder();
		
		order.setAccount(123456);
		order.setState(0);
		order.setTime(TimeConvert.dateToStr(TimeConvert.getSystemTime()));
		order.setTravelid(2);

		System.out.println(new OrderDao().Add(order));
		//new OrderDao().Show(new OrderDao().GetAll());
	}

}
