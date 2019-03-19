package model;

import java.util.List;

public interface Functions {
	public static final int LOGIN_OK = 1;
	public static final int REGISTE_OK = 1;
	public static final int REGISTE_FAILED = -1;
	public static final int LOGIN_FAILED = -1;
	public static final int OTHER_ERROR = -2;//错误
	public static final int EMPTY = -3;//没有找到
	public static final int OPERATE_OK = 4;//操作成功
	public static final int OPERATE_ERROR = 5;//操作失败
	public static final int USERNAME_ERROR = 6;//用户名不存在
	public static final int PASSWORD_ERROR = 7;//密码错误
	
	
	public int Login(String name,String password);//验证
	public int Add_User(String name,String password);//注册
	public int Update_User(String name,String old_password,String new_password);//修改密码
	public int Delete_User(String name);//删除用户
	public List<valueobject.Friend> Get_All_Friends(String username); 
	public List<valueobject.Friend> Get_Friends_By_Query(String username,String queryName); //根据条件查询
	public int Delete_Friend(Object key); //删除好友 
	public int Update_Friend(Object friend);//获取id 更新
	public int Add_Friend(String username,Object friend); //添加好友
	public int GetKey(valueobject.Friend friend); //根据用户名返回主键
	public int GetKey(valueobject.User user); //
}
