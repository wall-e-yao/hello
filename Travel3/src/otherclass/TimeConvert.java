package otherclass;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
public class TimeConvert {

	/**
	 * @param args
	 */
	public static String dateToStr(Date date){
		SimpleDateFormat simple = new SimpleDateFormat("yyyy-MM-dd  HH:mm:ss");
		String result = simple.format(date);
		return result;
	}
	
	public static String getToday(Date date){
		SimpleDateFormat simple = new SimpleDateFormat("yyyy-MM-dd");
		String result = simple.format(date);
		return result;
	}
	
	
	//birthday
	public static Date strToDate(String date){
		SimpleDateFormat simple = new SimpleDateFormat("yyyy-MM-dd");
		Date result = new Date();
		try {
			result = simple.parse(date);
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return result;
	}
	
	
	
	public static Date getSystemTime(){
		SimpleDateFormat simple = new SimpleDateFormat("yyyy-MM-dd  HH:mm:ss");
		Date result = new Date();
		try {
			return simple.parse(simple.format(result));
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
	}
	
	public static void main(String[] args) {
		// TODO Auto-generated method stub
//		Date now = new Date();
//		String datestr = dateToStr(now);
//		System.out.println(datestr);
//		System.out.println(strToDate(datestr));
//		System.out.println(dateToStr(getSystemTime()));
		System.out.println(getToday(new Date()));
	}

}
