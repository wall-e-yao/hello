package otherclass;

import java.net.InetAddress;
import java.net.UnknownHostException;

public class SystemIp {

	/**
	 * @param args
	 */
	public static String getHostAdd(){
		try {
			InetAddress inet = InetAddress.getLocalHost();
			return inet.getHostAddress();
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return "0.0.0.0";
		}
		
	}
	public static String getHostName(){
		try {
			InetAddress inet = InetAddress.getLocalHost();
			return inet.getHostName();
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return "admin";
		}
		
	}
	

}
