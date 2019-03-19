package DAO;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.LinkedList;
import java.util.List;

import valueobject.Friend;

public class FriendInfo implements all_interface {

	public int Add(Object obj) {
		Friend frd = new Friend();
		if(obj instanceof Friend){
			frd = (Friend)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("insert into myfriend(userid,name,sex,age,qq,telephone,email,address) values(?,?,?,?,?,?,?,?)");
				ps.setInt(1, frd.getUserid());
				ps.setString(2, frd.getName());
				ps.setString(3, frd.getSex());
				ps.setInt(4, frd.getAge());
				ps.setString(5, frd.getQq());
				ps.setString(6, frd.getTelephone());
				ps.setString(7, frd.getEmail());
				ps.setString(8, frd.getAddress());
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

	public int Delete(Object obj) {
		
		if(obj instanceof Integer){
			Integer frd_id = (Integer)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("delete from myfriend where id = ?");
				ps.setInt(1, frd_id.intValue());//×Ô¶¯²ðÏä
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
		Friend frd = new Friend();
		if(obj instanceof Friend){
			frd = (Friend)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("update myfriend set userid = ?,name=?,sex=?,age=?,qq=?,telephone=?,email=?,address=? where id=?");
				ps.setInt(1, frd.getUserid());
				ps.setString(2, frd.getName());
				ps.setString(3, frd.getSex());
				ps.setInt(4, frd.getAge());
				ps.setString(5, frd.getQq());
				ps.setString(6, frd.getTelephone());
				ps.setString(7, frd.getEmail());
				ps.setString(8, frd.getAddress());
				ps.setInt(9, frd.getId());
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
		String sql = "select * from myfriend ";
		if(where!=null && where.length()>0){
			sql=sql + " where "+where;
		}
		try {
			Connection con = DB.ConnectPool.getInstance().getConnection();
			Statement st = con.createStatement();
			ResultSet rs = st.executeQuery(sql); 
			while(rs.next()){
				Friend one = new Friend();
				one.setId(rs.getInt("id"));
				one.setUserid(rs.getInt("userid"));
				one.setAge(rs.getInt("age"));
				one.setAddress(rs.getString("address"));
				one.setEmail(rs.getString("email"));
				one.setName(rs.getString("name"));
				one.setQq(rs.getString("qq"));
				one.setTelephone(rs.getString("telephone"));
				one.setSex(rs.getString("sex"));
				result.add(one);
			}
		} catch (Exception e) {
			return null;
		}
		return result;
	}

	public static void main(String[] args) {
		System.out.print(new FriendInfo().Delete("12"));
	}
}
