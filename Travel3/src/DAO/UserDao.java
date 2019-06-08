package DAO;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.Date;
import java.util.LinkedList;
import java.util.List;

import valueobject.User;


public class UserDao implements DaoInterface {

	public int Add(Object obj) {
		// TODO Auto-generated method stub
		User user = DB.ObjectFactory.getUser();
		if(obj instanceof User){
			user = (User)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("insert into userTable(name,account,password,sex,id,telephone,birthday,profession,address,flag) values(?,?,?,?,?,?,?,?,?,?)");
				ps.setString(1, user.getName());
				ps.setInt(2, user.getAccount());
				ps.setString(3, user.getPassword());
				ps.setString(4, user.getSex());
				ps.setString(5, user.getId());
				ps.setString(6, user.getTelephone());
				ps.setDate(7,user.getBirthday());
				ps.setString(8, user.getProfession());
				ps.setString(9, user.getAddress());
				ps.setInt(10, user.getFlag());
				
				ps.executeUpdate();
				
				return UserDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				System.out.println(e);
				return UserDao.DB_CONNECTION_ERROR;
			}
		}else{
			return UserDao.OPERATE_ERROR;
		}
	}

	public int Delete(Object key){
		// TODO Auto-generated method stub
		if(key instanceof Integer){
			Integer i = (Integer)key;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("delete from userTable where account = ?");
				ps.setInt(1, i);
				ps.executeUpdate();
				return UserDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				System.out.println(e);
				return UserDao.DB_CONNECTION_ERROR;
			}
		}else{
			return UserDao.OPERATE_ERROR;
		}
	}
	
	public int Update(Object obj) {
		User user = DB.ObjectFactory.getUser();
		if(obj instanceof User){
			user = (User)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("update userTable set name=?,password=?,sex=?,id=?,telephone=?,birthday=?,profession=?,address=?,flag=? where account=?");
				ps.setString(1, user.getName());
				ps.setString(2, user.getPassword());
				ps.setString(3, user.getSex());
				ps.setString(4, user.getId());
				ps.setString(5, user.getTelephone());
				ps.setDate(6,user.getBirthday());
				ps.setString(7, user.getProfession());
				ps.setString(8, user.getAddress());
				ps.setInt(9, user.getFlag());
				ps.setInt(10, user.getAccount());
				ps.executeUpdate();
				return UserDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				return UserDao.DB_CONNECTION_ERROR;
			}
		}else{
			return UserDao.OPERATE_ERROR;
		}
	}
	
	@SuppressWarnings("deprecation")
	public static void main(String[] args) {
		
		User user = DB.ObjectFactory.getUser();
		user.setAccount(123456);
		user.setAddress("黑龙江哈尔滨");
		user.setBirthday(new Date(1998-1900,9-1,1));
		user.setFlag(0);
		user.setId("123456789123456789");
		user.setName("王五");
		user.setPassword("123456");
		user.setProfession("学生");
		user.setSex("F");
		user.setTelephone("18823123645");

		System.out.println(new UserDao().Add(user)); 
		//System.out.println(new UserDao().Delete(new Integer(123456))); 
		//System.out.println(new UserDao().Update(user)); 	
		//new UserDao().Show(new UserDao().GetBy("name = '张三'"));
	}


	
	
	public List<Object> GetAll() {
	// TODO Auto-generated method stub
	return GetBy("");
	}

	public List<Object> GetBy(String where) {
	// TODO Auto-generated method stub
		List<Object> result = new LinkedList<Object>();
		String sql = "select * from userTable ";
		if(where!=null && where.length()>0){
			sql=sql + " where "+where;
		}
		try {
			Connection con = DB.ConnectPool.getInstance().getConnection();
			Statement st = con.createStatement();
			ResultSet rs = st.executeQuery(sql); 
			while(rs.next()){
				User one = DB.ObjectFactory.getUser();
				one.setAccount(rs.getInt("account"));
				one.setAddress(rs.getString("address"));
				one.setBirthday(rs.getDate("birthday"));
				one.setFlag(rs.getInt("flag"));
				one.setId(rs.getString("id"));
				one.setName(rs.getString("name"));
				one.setPassword(rs.getString("password"));
				one.setProfession(rs.getString("profession"));
				one.setSex(rs.getString("sex"));
				one.setTelephone(rs.getString("telephone"));
				result.add(one);
			}
		} catch (Exception e) {
			return null;
		}
		return result;
	}

	public void Show(List<Object> result) {
	// TODO Auto-generated method stub
		
		for (Object object : result) {
			User user = DB.ObjectFactory.getUser();
			user = (User)object;
			System.out.println("账号："+user.getAccount());
			System.out.println("姓名："+user.getName());
			System.out.println("住址："+user.getAddress());
			System.out.println("id："+user.getId());
			System.out.println("职业："+user.getProfession());
			System.out.println("性别："+("m".equalsIgnoreCase(user.getSex())?"男":"女"));
			System.out.println("电话："+user.getTelephone());
			System.out.println("生日："+user.getBirthday());
		}
		return ;
		
	}

}
