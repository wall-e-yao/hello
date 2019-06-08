package valueobject;


public class Trace {
	private int account;
	private String date;
	private String remark;
	
	public String getRemark() {
		return remark;
	}
	public void setRemark(String remark) {
		this.remark = remark;
	}
	public int getAccount() {
		return account;
	}
	public String getDate() {
		return date;
	}
	public void setAccount(int account) {
		this.account = account;
	}
	public void setDate(String date) {
		this.date = date;
	}
	
	
}
