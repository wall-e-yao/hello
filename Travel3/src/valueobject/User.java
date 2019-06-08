package valueobject;

import java.sql.Date;

public class User {

	/**
	 * @param args
	 */
	private String name;
	private int account;
	private String password;
	private String sex;
	private String id;
	private String telephone;
	private Date birthday;
	private String profession;
	private String address;
	private int flag;
	
	public String getTelephone() {
		return telephone;
	}
	
	public void setTelephone(String telephone) {
		this.telephone = telephone;
	}
	
	public String getName() {
		return name;
	}
	public int getAccount() {
		return account;
	}
	public String getPassword() {
		return password;
	}
	public String getSex() {
		return sex;
	}
	public String getId() {
		return id;
	}
	public Date getBirthday() {
		return birthday;
	}
	public String getProfession() {
		return profession;
	}
	public String getAddress() {
		return address;
	}
	public int getFlag() {
		return flag;
	}
	public void setName(String name) {
		this.name = name;
	}
	public void setAccount(int account) {
		this.account = account;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public void setSex(String sex) {
		this.sex = sex;
	}
	public void setId(String id) {
		this.id = id;
	}
	public void setBirthday(java.util.Date birthday) {
		this.birthday = new Date(birthday.getTime());
	}
	public void setProfession(String profession) {
		this.profession = profession;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public void setFlag(int flag) {
		this.flag = flag;
	}
		
}
