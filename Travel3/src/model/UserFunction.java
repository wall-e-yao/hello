package model;


public interface UserFunction {
	public static final int LOGIN_OK = 1;
	public static final int MANAGER_LOGIN_OK = 2;
	public static final int REGISTE_OK = 1;
	public static final int REGISTE_FAILED = -1;
	public static final int LOGIN_FAILED = -1;
	public static final int LOGIN_ILLEGAL = -3;
	
	public static final int OTHER_ERROR = -2;//错误
	public static final int OPERATE_OK = 4;//操作成功
	public static final int OPERATE_ERROR = 5;//操作失败
	public static final int OLDPWD_ERROR = 6;//操作失败

	
	public int Login(int account,String password);//验证
	public int Update_User(int account,String old_password,String new_password);//修改密码
	public int Delete_User(int account);//注销用户
	public int OrderTravel(Object order);//预约旅行
	public int PayOrder(Object order);//支付订单
	public String getNameByAccount(Object account);

}
