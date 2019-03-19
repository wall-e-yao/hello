package model;

import java.util.List;

public interface Functions {
	public static final int LOGIN_OK = 1;
	public static final int REGISTE_OK = 1;
	public static final int REGISTE_FAILED = -1;
	public static final int LOGIN_FAILED = -1;
	public static final int OTHER_ERROR = -2;//����
	public static final int EMPTY = -3;//û���ҵ�
	public static final int OPERATE_OK = 4;//�����ɹ�
	public static final int OPERATE_ERROR = 5;//����ʧ��
	public static final int USERNAME_ERROR = 6;//�û���������
	public static final int PASSWORD_ERROR = 7;//�������
	
	
	public int Login(String name,String password);//��֤
	public int Add_User(String name,String password);//ע��
	public int Update_User(String name,String old_password,String new_password);//�޸�����
	public int Delete_User(String name);//ɾ���û�
	public List<valueobject.Friend> Get_All_Friends(String username); 
	public List<valueobject.Friend> Get_Friends_By_Query(String username,String queryName); //����������ѯ
	public int Delete_Friend(Object key); //ɾ������ 
	public int Update_Friend(Object friend);//��ȡid ����
	public int Add_Friend(String username,Object friend); //��Ӻ���
	public int GetKey(valueobject.Friend friend); //�����û�����������
	public int GetKey(valueobject.User user); //
}
