package model;

import java.util.LinkedList;
import java.util.List;

import DAO.DaoInterface;

import otherclass.SystemIp;
import otherclass.TimeConvert;

import valueobject.Trace;
import valueobject.User;

public class UserImplement implements UserFunction {


	public int Delete_User(int account) {
		// TODO Auto-generated method stub
		DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
		int flag = userDao.Delete(account);
		if(flag==DaoInterface.OPERATE_SUCCESS){
			return UserImplement.OPERATE_OK;
		}else if(flag==DaoInterface.OPERATE_ERROR)
			return UserImplement.OPERATE_ERROR;
		else
			return UserImplement.OTHER_ERROR;
	}

	public int Login(int account, String password) {
		// TODO Auto-generated method stub
		if(password==null||password.length()<=0){
			return UserImplement.LOGIN_FAILED;
		}
		DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
		List<Object> list = new LinkedList<Object>();
		list = userDao.GetBy("account = "+account);
		if(list.size()==1){
			valueobject.User user = (valueobject.User)list.get(0);
			if(user.getPassword().equals(password)){
				DAO.DaoInterface enterDao =  DB.ObjectFactory.getTraceDao();
				
				Trace trace = DB.ObjectFactory.getTrace();
				trace.setAccount(account);
				trace.setDate(TimeConvert.dateToStr(TimeConvert.getSystemTime()));
				trace.setRemark("username:"+SystemIp.getHostName()+";IP:"+SystemIp.getHostAdd()+";");
				
				enterDao.Add(trace);
				
				if(user.getFlag()==0)
					return UserImplement.LOGIN_OK;
				else if(user.getFlag()==1)
					return UserImplement.MANAGER_LOGIN_OK;
				else if(user.getFlag()==2)
					return UserImplement.LOGIN_ILLEGAL;
			}
			else{
				return UserImplement.LOGIN_FAILED;
			}
		}
		
		return UserImplement.LOGIN_FAILED;
	}

	public int OrderTravel(Object order) {
		// TODO Auto-generated method stub
		return 0;
	}

	public int PayOrder(Object order) {
		// TODO Auto-generated method stub
		return 0;
	}

	public int Update_User(int account, String oldPassword, String newPassword) {
		// TODO Auto-generated method stub
		if(oldPassword==null||oldPassword.length()<=0||newPassword==null||newPassword.length()<=0){
			return UserImplement.OPERATE_ERROR;
		}
		DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
		List<Object> list = userDao.GetBy("account = "+account);
		if(list.size()==1){
			User user = (User)list.get(0);
			if(user.getPassword().equals(oldPassword)==true){
				user.setPassword(newPassword);
				int flag = userDao.Update(user);
				if(flag == DaoInterface.OPERATE_SUCCESS){
					return UserImplement.OPERATE_OK;
				}else if(flag==DaoInterface.OPERATE_ERROR){
					return UserImplement.OPERATE_ERROR;
				}
				else
					return UserImplement.OTHER_ERROR;
			}
			return UserImplement.OLDPWD_ERROR;
		}
		return UserImplement.OPERATE_ERROR;

		
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

	public String getNameByAccount(Object account) {
		// TODO Auto-generated method stub
		if(account instanceof Integer){
			Integer tmp = (Integer)account;
			DAO.DaoInterface userDao = DB.ObjectFactory.getUserDao();
			List<Object> list = userDao.GetBy("account = "+tmp.intValue());
			if(list.size()==1){
				User user = (User)list.get(0);
				return user.getName();
			}else
				return "";
		}
		return "";
	}

}
