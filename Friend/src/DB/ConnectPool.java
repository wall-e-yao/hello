package DB;
import java.util.*;
import java.sql.*;
import java.io.*;
public class ConnectPool {
	private List<Connection> pool;
	private int Pool_Size = 10;
	private static ConnectPool instance = null;
	//����Ψһ�����ӳض���
	public static ConnectPool getInstance(){
		if(instance==null){
			instance = new ConnectPool();
		}
		return instance;
	}
	//���ӳع��캯��
	private ConnectPool(){
		System.out.print("---------------");
		pool = new LinkedList<Connection>();
		AddToPool();
	}
	//�������ӵ����ӳ�
	private void AddToPool(){
		Properties prop = new Properties();
		try{
			//D:/myeclipse/J2ee_Test/WEB-INF/classes/DB
			InputStream in = new BufferedInputStream(new FileInputStream("D:/myeclipse/myworks_jsp/Friend/src/DB/databaseconfig.properties"));
			prop.load(in);
			}catch (Exception e) {
			System.out.print("�����ļ�����");
			return ;
		}
		try {
			Class.forName(prop.getProperty("Driver"));
			System.out.print(prop.getProperty("Driver"));
		for (int i = 0; i < this.Pool_Size; i++) {
		    Connection con=java.sql.DriverManager.getConnection(prop.getProperty("url")+"/"+prop.getProperty("database_name"),prop.getProperty("username"),prop.getProperty("password"));
		    pool.add(con);
		}
		} catch (Exception e) {
			System.out.print("�������ݿ���� --- in ConnectPool");
		}
	}
	//���ӳ�Ψһ���� ����һ������
	public synchronized Connection getConnection(){
		if(pool.size()==0){
			AddToPool();
		}
		return pool.remove(0);
	}
	//�ͷ�
	public synchronized void Release(Connection con){
		if(con!=null){
			pool.add(con);
		}
	}
}
