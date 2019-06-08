package DAO;
import java.util.List;

public interface DaoInterface {

		public static final int DB_CONNECTION_ERROR = -4;
		public static final int NOT_EXISTS = -3;
		public static final int OPERATE_ERROR = -2;
		public static final int OPERATE_SUCCESS = 1;
		public int Add(Object obj);
		public int Delete(Object key);//����
		public int Update(Object obj);
		public List<Object> GetBy(String where);// ����  where ��������
		public List<Object> GetAll();
		public void Show(List<Object> result);//�鿴

	}
