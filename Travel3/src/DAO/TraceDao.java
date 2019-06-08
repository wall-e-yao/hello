package DAO;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.LinkedList;
import java.util.List;

import otherclass.SystemIp;
import otherclass.TimeConvert;

import valueobject.Trace;

public class TraceDao implements DaoInterface {

	public int Add(Object obj) {
		// TODO Auto-generated method stub
		Trace trace = DB.ObjectFactory.getTrace();
		if(obj instanceof Trace){
			trace = (Trace)obj;
			try {
				Connection con = DB.ConnectPool.getInstance().getConnection();
				PreparedStatement ps = con.prepareStatement("insert into logintable(account,date,remark) values(?,?,?)");
				ps.setInt(1, trace.getAccount());
				ps.setString(2, TimeConvert.dateToStr(TimeConvert.getSystemTime()));
				ps.setString(3, trace.getRemark());
				ps.executeUpdate();
				
				return TraceDao.OPERATE_SUCCESS;
			} catch (Exception e) {
				// TODO: handle exception
				System.out.println(e);
				return TraceDao.DB_CONNECTION_ERROR;
			}
		}else{
			return TraceDao.OPERATE_ERROR;
		}
	}

	public int Delete(Object key) {
		// TODO Auto-generated method stub
		return TraceDao.OPERATE_ERROR;
	}

	public List<Object> GetAll() {
		// TODO Auto-generated method stub
		return GetBy("");
	}

	public List<Object> GetBy(String where) {
		// TODO Auto-generated method stub
		List<Object> result = new LinkedList<Object>();
		String sql = "select * from loginTable ";
		if(where!=null && where.length()>0){
			sql=sql + " where "+where;
		}
		try {
			Connection con = DB.ConnectPool.getInstance().getConnection();
			Statement st = con.createStatement();
			ResultSet rs = st.executeQuery(sql); 
			while(rs.next()){
				Trace one = DB.ObjectFactory.getTrace();
				one.setAccount(rs.getInt("account"));
				one.setDate(rs.getString("date"));
				one.setRemark(rs.getString("remark"));
				result.add(one);
			}
		} catch (Exception e) {
			System.out.println("²éÑ¯Ê§°Ü£¡");
			return null;
		}
		return result;
	}

	public void Show(List<Object> result) {
		// TODO Auto-generated method stub
		for (Object object : result) {
			Trace trace = DB.ObjectFactory.getTrace();
			trace = (Trace)object;
			System.out.println("ÕËºÅ£º"+trace.getAccount());
			System.out.println("µÇÂ¼Ê±¼ä£º"+trace.getDate());
			System.out.println("±¸×¢£º"+trace.getRemark());
		}
		return ;
	}

	public int Update(Object obj) {
		// TODO Auto-generated method stub
		
		return TraceDao.OPERATE_ERROR;
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Trace trace = DB.ObjectFactory.getTrace();
		
		trace.setAccount(123456);
		trace.setDate(TimeConvert.dateToStr(TimeConvert.getSystemTime()));
		trace.setRemark("username:"+SystemIp.getHostName()+";IP:"+SystemIp.getHostAdd());
		
		System.out.println(new TraceDao().Add(trace));
		new TraceDao().Show(new TraceDao().GetBy("date like '%9:20%'"));
	}

}
