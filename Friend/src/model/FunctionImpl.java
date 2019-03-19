package model;
import java.util.LinkedList;
import java.util.List;

import valueobject.Friend;
import valueobject.User;
import DAO.FriendInfo;
import DAO.UserInfo;
import DAO.all_interface;

public class FunctionImpl implements Functions {

	public int Add_Friend(String username,Object friend) {
		// TODO Auto-generated method stub	
		if(friend!=null && friend instanceof valueobject.Friend){
			DAO.all_interface friendDao = DB.ObjectFactory.getFriendDao();
			User one = DB.ObjectFactory.getUserObject();
			one.setName(username);
			int userid = this.GetKey(one);
			Friend frd = (Friend)friend;
			frd.setUserid(userid);
			int i = friendDao.Add((Object)frd);
			if(i==friendDao.OPERATE_SUCCESS){
				return this.OPERATE_OK;
			}else if(i==friendDao.OPERATE_ERROR){
				return this.OPERATE_ERROR;
			}
		}
		return this.OTHER_ERROR;
	}

	public int Add_User(String name, String password) {
		DAO.all_interface userDao = DB.ObjectFactory.getUserDao();
		List<Object> list = userDao.GetBy(" name = '"+name+"'");
		if(list.size()==0){
			valueobject.User newuser = DB.ObjectFactory.getUserObject();
			newuser.setName(name);
			newuser.setPassword(password);
			int i = userDao.Add(newuser);
			if(i==userDao.OPERATE_SUCCESS){
				return this.OPERATE_OK;
			}else if(i==userDao.OPERATE_ERROR){
				return this.OPERATE_ERROR;
			}	
		}else
		{
			//重名
			System.out.println("注册失败，重名");
			return this.REGISTE_FAILED;
		}
		return this.OTHER_ERROR;
	}

	public int Delete_Friend(Object key) {
		// TODO Auto-generated method stub
		DAO.all_interface friendDao = DB.ObjectFactory.getFriendDao();
		int i = friendDao.Delete(key);
		if(i == friendDao.OPERATE_SUCCESS){
			return this.OPERATE_OK;
		}else if(i==friendDao.OPERATE_ERROR){
			return this.OPERATE_ERROR;
		}else{
			return this.OTHER_ERROR;
		}
	}

	public int Delete_User(String name) {
		// TODO Auto-generated method stub
		return 0;
	}

	public int GetKey(Friend friend) {
		
		return this.EMPTY;
	}

	public int GetKey(User user) {//根据姓名找主键 userid
		all_interface dao = new UserInfo();
		List<Object> users = dao.GetBy("name = '"+user.getName()+"'");
		if(users.size()==1){
			User one = (User)users.get(0);
			return one.getUserid();
		}
		return this.EMPTY;
	}

	public List<Friend> Get_All_Friends(String username) {
		// TODO Auto-generated method stub
		return this.Get_Friends_By_Query(username, "");
	}

	public List<Friend> Get_Friends_By_Query(String username,String queryName) {
		// TODO Auto-generated method stub
		String sql_where = "userid in (select userid from user where name='"+username+"')";
		if(queryName!=null&&queryName.length()>0)
			sql_where = sql_where+" and name like '%"+queryName+"%'";
		List<Friend> result = new LinkedList();
		List<Object> friends = DB.ObjectFactory.getFriendDao().GetBy(sql_where);
		for(Object o:friends){
			Friend frd = (Friend)o;
			result.add(frd);
		}
		return result;
	}

	public int Login(String name, String password) {
		if(name==null||name.length()<=0){
			return this.USERNAME_ERROR;
		}
		if(password==null||password.length()<=0){
			return this.PASSWORD_ERROR;
		}
		DAO.all_interface userDao = DB.ObjectFactory.getUserDao();
		List<Object> list = new LinkedList<Object>();
		list = userDao.GetBy(" name = '"+name+"'");
		if(list.size()==1){
			valueobject.User user = (valueobject.User)list.get(0);
			if(user.getPassword().equals(password)){
				return this.LOGIN_OK;
			}
			else{
				return this.PASSWORD_ERROR;
			}
		}else{
			return this.USERNAME_ERROR;
		}
	}

	public int Update_Friend(Object friend) {
		DAO.all_interface friendDao = DB.ObjectFactory.getFriendDao();
		int i = friendDao.Update(friend);
		if(i == friendDao.OPERATE_SUCCESS){
			return this.OPERATE_OK;
		}else if(i==friendDao.OPERATE_ERROR){
			return this.OPERATE_ERROR;
		}else{
			return this.OTHER_ERROR;
		}
	}
	
	public int Update_User(String name, String oldPassword, String newPassword) {
		// TODO Auto-generated method stub
		return 0;
	}
	
	public static void main(String[] args) {
		//System.out.print(new FunctionImpl().Login("ab1c", "11"));
		Friend frd = DB.ObjectFactory.getFriendObject();
		
		System.out.print(new FunctionImpl().Delete_Friend(12));
		
	}

}
