package DAO;

import java.util.List;

public interface all_interface {
	public static final int DB_CONNECTION_ERROR = -4;
	public static final int NOT_EXISTS = -1;
	public static final int OPERATE_ERROR = -2;
	public static final int OPERATE_SUCCESS = 1;
	public int Add(Object obj);
	public int Delete(Object key);//Ö÷¼ü
	public int Update(Object obj);
	public List<Object> GetBy(String where);
	public List<Object> GetALL();

}
