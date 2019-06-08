package valueobject;

public class Travel {
	
	private int travelid;
	private String name;
	private String keyword;
	private String time;
	private int sum;
	private String start;
	private String end;
	private double fee;
	private int people;
	private String remake;
	private String imageAdd;
	
	public int getTravelid() {
		return travelid;
	}
	public String getName() {
		return name;
	}
	public String getKeyword() {
		return keyword;
	}
	public String getTime() {
		return time;
	}
	public int getSum() {
		return sum;
	}
	public String getStart() {
		return start;
	}
	public String getEnd() {
		return end;
	}
	public double getFee() {
		return fee;
	}
	public int getPeople() {
		return people;
	}
	public String getRemake() {
		return remake;
	}
	public void setTravelid(int travelid) {
		this.travelid = travelid;
	}
	public void setName(String name) {
		this.name = name;
	}
	public void setKeyword(String keyword) {
		this.keyword = keyword;
	}
	public void setTime(String time) {
		this.time = time;
	}
	public void setSum(int sum) {
		this.sum = sum;
	}
	public void setStart(String start) {
		this.start = start;
	}
	public void setEnd(String end) {
		this.end = end;
	}
	public void setFee(double fee) {
		this.fee = fee;
	}
	public void setPeople(int people) {
		this.people = people;
	}
	public void setRemake(String remake) {
		this.remake = remake;
	}
	public String getImageAdd() {
		return imageAdd;
	}
	public void setImageAdd(String imageAdd) {
		this.imageAdd = imageAdd;
	}

}
