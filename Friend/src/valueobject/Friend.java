package valueobject;

public class Friend {
	private int id,userid,age;
	private String name,sex,qq,telephone,email,address;
	public Friend() {
		// TODO Auto-generated constructor stub
		this.id = 0;
		this.userid = 0;
		this.age = 0;
		this.name = "";
		this.sex = "";
		this.qq = "";
		this.telephone = "";
		this.email = "";
		this.address = "";
	}
	public Friend(int id, int userid, int age, String name, String sex,
			String qq, String telephone, String email, String address) {
		this.id = id;
		this.userid = userid;
		this.age = age;
		this.name = name;
		this.sex = sex;
		this.qq = qq;
		this.telephone = telephone;
		this.email = email;
		this.address = address;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public int getUserid() {
		return userid;
	}
	public void setUserid(int userid) {
		this.userid = userid;
	}
	public int getAge() {
		return age;
	}
	public void setAge(int age) {
		this.age = age;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getSex() {
		return sex;
	}
	public void setSex(String sex) {
		this.sex = sex;
	}
	public String getQq() {
		return qq;
	}
	public void setQq(String qq) {
		this.qq = qq;
	}
	public String getTelephone() {
		return telephone;
	}
	public void setTelephone(String telephone) {
		this.telephone = telephone;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	
}