package valueobject;


public class Order {
	
	private int account;
	private String time;
	private int state;
	private int travelid;
	private int kid;
	private int adult;
	private double money;
	private String date;
	
	public String getDate() {
		return date;
	}
	public void setDate(String date) {
		this.date = date;
	}
	public int getAccount() {
		return account;
	}
	public String getTime() {
		return time;
	}
	public int getState() {
		return state;
	}
	public int getTravelid() {
		return travelid;
	}
	public void setAccount(int account) {
		this.account = account;
	}
	public void setTime(String time) {
		this.time = time;
	}
	public void setState(int state) {
		this.state = state;
	}
	public void setTravelid(int travelid) {
		this.travelid = travelid;
	}
	public int getKid() {
		return kid;
	}
	public int getAdult() {
		return adult;
	}
	public double getMoney() {
		return money;
	}
	public void setKid(int kid) {
		this.kid = kid;
	}
	public void setAdult(int adult) {
		this.adult = adult;
	}
	public void setMoney(double money) {
		this.money = money;
	}
	
	
}
