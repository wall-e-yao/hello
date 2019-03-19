package DAO;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.Date;
import java.util.LinkedList;
import java.util.List;

import valueobject.User;


public class UserInfo implements all_interface {

	
	public int Add(Object obj) {
		// TODO Auto-generated method stub
		User user = new User();
		if(obj instanceof User){
			user = (User)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("insert into user(name,password) values(?,?)");
				ps.setString(1, user.getName());
				ps.setString(2, user.getPassword());
				ps.executeUpdate();
				return this.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return UserInfo.DB_CONNECTION_ERROR;
			}
		}else{
			return UserInfo.OPERATE_ERROR;
		}
	}

	public int Delete(Object key) {
		if(key instanceof Integer){
			Integer i = (Integer)key;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("delete from user where id = ?");
				ps.setInt(1, i);
				ps.executeUpdate();
				return this.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return UserInfo.DB_CONNECTION_ERROR;
			}
		}else{
			return UserInfo.OPERATE_ERROR;
		}
	}
	
	public int Update(Object obj) {
		User user = new User();
		if(obj instanceof User){
			user = (User)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("update user set name=?,password=? where userid = ?");
				ps.setString(1, user.getName());
				ps.setString(2, user.getPassword());
				ps.setInt(3, user.getUserid());
				ps.executeUpdate();
				return this.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return UserInfo.DB_CONNECTION_ERROR;
			}
		}else{
			return UserInfo.OPERATE_ERROR;
		}
	}
	
	public List<Object> GetALL() {
		return GetBy("");
	}

	public List<Object> GetBy(String where) {
		List<Object> result = new LinkedList<Object>();
		String sql = "select * from user ";
		if(where!=null && where.length()>0){
			sql=sql + " where "+where;
			System.out.println(sql);
		}
		try {
			Connection con = DB.ConnectPool.getInstance().getConnection();
			Statement st = con.createStatement();
			ResultSet rs = st.executeQuery(sql); 
			while(rs.next()){
				User one = DB.ObjectFactory.getUserObject();
				one.setUserid(rs.getInt("userid"));
				one.setName(rs.getString("name"));
				one.setPassword(rs.getString("password"));
				result.add(one);
			}
		} catch (Exception e) {
			return null;
		}
		return result;
	}
	
	public static void main(String[] args) {
		List<Object> list = new LinkedList<Object>();
		list = new UserInfo().GetBy(" name = 'abc'");
		User user = (User)list.get(0);
		System.out.println("name"+user.getName());
		System.out.println("pass"+user.getPassword());
		System.out.println("id"+user.getUserid());

	}
}
