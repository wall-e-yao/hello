package DB;
import java.util.*;
import java.sql.*;
import java.io.*;
public class ConnectPool {
	private List<Connection> pool;
	private int Pool_Size = 10;
	private static ConnectPool instance = null;
	//返回唯一的连接池对象
	public static ConnectPool getInstance(){
		if(instance==null){
			instance = new ConnectPool();
		}
		return instance;
	}
	//连接池构造函数
	private ConnectPool(){
		System.out.print("---------------");
		pool = new LinkedList<Connection>();
		AddToPool();
	}
	//申请连接到连接池
	private void AddToPool(){
		Properties prop = new Properties();
		try{
			//D:/myeclipse/J2ee_Test/WEB-INF/classes/DB
			InputStream in = new BufferedInputStream(new FileInputStream("D:/myeclipse/myworks_jsp/Friend/src/DB/databaseconfig.properties"));
			prop.load(in);
			}catch (Exception e) {
			System.out.print("配置文件错误");
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
			System.out.print("连接数据库错误 --- in ConnectPool");
		}
	}
	//连接池唯一对象 申请一个连接
	public synchronized Connection getConnection(){
		if(pool.size()==0){
			AddToPool();
		}
		return pool.remove(0);
	}
	//释放
	public synchronized void Release(Connection con){
		if(con!=null){
			pool.add(con);
		}
	}
}
