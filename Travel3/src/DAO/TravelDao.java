package DAO;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.LinkedList;
import java.util.List;

import otherclass.TimeConvert;

import valueobject.Travel;

public class TravelDao implements DaoInterface {

	public int Add(Object obj) {
		// TODO Auto-generated method stub
		Travel travel = DB.ObjectFactory.getTravel();
		if(obj instanceof Travel){
			travel = (Travel)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("insert into travelTable(travelid,name,keyword,time,sum,start,end,fee,people,remark,imageAdd) values(?,?,?,?,?,?,?,?,?,?,?)");

				ps.setInt(1, travel.getTravelid());
				ps.setString(2, travel.getName());
				ps.setString(3, travel.getKeyword());
				ps.setString(4, travel.getTime());
				ps.setInt(5, travel.getSum());
				ps.setString(6, travel.getStart());
				ps.setString(7, travel.getEnd());
				ps.setDouble(8, travel.getFee());
				ps.setInt(9, travel.getPeople());
				ps.setString(10, travel.getRemake());
				ps.setString(11, travel.getImageAdd());
				
				
				ps.executeUpdate();
				
				return TravelDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				System.out.println(e);
				return TravelDao.DB_CONNECTION_ERROR;
			}
		}else{
			return TravelDao.OPERATE_ERROR;
		}
	}

	public int Delete(Object key) {
		// TODO Auto-generated method stub
		if(key instanceof Integer){
			Integer i = (Integer)key;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("delete from travelTable where travelid = ?");
				
				ps.setInt(1, i);
				ps.executeUpdate();
				
				return TravelDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return TravelDao.DB_CONNECTION_ERROR;
			}
		}else{
			return TravelDao.OPERATE_ERROR;
		}
	}

	public List<Object> GetAll() {
		// TODO Auto-generated method stub
		return GetBy("");
	}

	public List<Object> GetBy(String where) {
		// TODO Auto-generated method stub
		List<Object> result = new LinkedList<Object>();
		String sql = "select * from travelTable ";
		if(where!=null && where.length()>0){
			sql=sql + " where "+where;
		}
		try {
			
			Connection con = DB.ConnectPool.getInstance().getConnection();
			Statement st = con.createStatement();
			ResultSet rs = st.executeQuery(sql); 
			
			while(rs.next()){
				
				Travel one = DB.ObjectFactory.getTravel();
				
				one.setName(rs.getString("name"));
				one.setEnd(rs.getString("end"));
				one.setFee(rs.getDouble("fee"));
				one.setKeyword(rs.getString("keyword"));
				one.setPeople(rs.getInt("people"));
				one.setRemake(rs.getString("remark"));
				one.setStart(rs.getString("start"));
				one.setSum(rs.getInt("sum"));
				one.setTime(rs.getString("time"));
				one.setTravelid(rs.getInt("travelid"));
				one.setImageAdd(rs.getString("imageAdd"));
				
				result.add(one);
			}
			
		} catch (Exception e) {
			System.out.println(e);
			return new LinkedList<Object>();
		}
		return result;
	}

	public void Show(List<Object> result) {
		// TODO Auto-generated method stub
		
		if(result.size()==0){
			System.out.println("没有找到！");
			return;
		}
		
		for (Object object : result) {
			Travel travel = DB.ObjectFactory.getTravel();
			travel = (Travel)object;
			
			System.out.println("名称："+travel.getName());
			System.out.println("类别："+travel.getKeyword());
			System.out.println("起始点："+travel.getStart());
			System.out.println("终点："+travel.getEnd());
			System.out.println("花费："+travel.getFee());
			System.out.println("时间："+travel.getTime());
			System.out.println("备注："+travel.getRemake());
			
		}
		return ;
		
	}

	public int Update(Object obj) {
		// TODO Auto-generated method stub
		Travel travel = DB.ObjectFactory.getTravel();
		if(obj instanceof Travel){
			travel = (Travel)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("update travelTable set name=?,keyword=?,time=?,sum=?,start=?,end=?,fee=?,people=?,remark = ?, imageAdd =? where travelid=?");
				
				ps.setString(1, travel.getName());
				ps.setString(2, travel.getKeyword());
				ps.setString(3, travel.getTime());
				ps.setInt(4, travel.getSum());
				ps.setString(5, travel.getStart());
				ps.setString(6, travel.getEnd());
				ps.setDouble(7, travel.getFee());
				ps.setInt(8, travel.getPeople());
				ps.setString(9, travel.getRemake());
				ps.setString(10, travel.getImageAdd());
				ps.setInt(11, travel.getTravelid());
				
				ps.executeUpdate();
				return TravelDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				System.out.println(e);
				return TravelDao.DB_CONNECTION_ERROR;
			}
		}else{
			return TravelDao.OPERATE_ERROR;
		}
	}

	/**
	 * @param args
	 */
	@SuppressWarnings("deprecation")
	public static void main(String[] args) {
		// TODO Auto-generated method stub

		Travel travel = DB.ObjectFactory.getTravel();
		
		travel.setFee(288.8);
		travel.setEnd("冰雪大世界");
		travel.setKeyword("哈尔滨");
		travel.setName("冰城");
		travel.setSum(50);
		travel.setTime(TimeConvert.dateToStr(new java.util.Date(2020-1900,2-1,3,9,30)));
		
		
		//System.out.println(new TravelDao().Add(travel)); 
		
		new TravelDao().Show(new TravelDao().GetAll());
		
	}

}
