package model;


public interface UserFunction {
	public static final int LOGIN_OK = 1;
	public static final int MANAGER_LOGIN_OK = 2;
	public static final int REGISTE_OK = 1;
	public static final int REGISTE_FAILED = -1;
	public static final int LOGIN_FAILED = -1;
	public static final int LOGIN_ILLEGAL = -3;
	
	public static final int OTHER_ERROR = -2;//����
	public static final int OPERATE_OK = 4;//�����ɹ�
	public static final int OPERATE_ERROR = 5;//����ʧ��
	public static final int OLDPWD_ERROR = 6;//����ʧ��

	
	public int Login(int account,String password);//��֤
	public int Update_User(int account,String old_password,String new_password);//�޸�����
	public int Delete_User(int account);//ע���û�
	public int OrderTravel(Object order);//ԤԼ����
	public int PayOrder(Object order);//֧������
	public String getNameByAccount(Object account);

}
