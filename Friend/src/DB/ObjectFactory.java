package DB;

import DAO.FriendInfo;

@SuppressWarnings("unused")
public class ObjectFactory {
	
	public static DAO.all_interface getFriendDao(){
//		try {
//			//java ∑¥…‰
//			return (DAO.all_interface)Class.forName("DAO.FriendInfo").newInstance();
//		} catch (Exception e) {
//			System.out.print("FriendDao “Ï≥££°");
//			return null;
//		}
		return new DAO.FriendInfo();
	}
	public static DAO.all_interface getUserDao(){
		return new DAO.UserInfo();
	}
	public static valueobject.User getUserObject(){
		return new valueobject.User();
	}
	public static valueobject.Friend getFriendObject(){
		return new valueobject.Friend();
	}
	public static model.Functions getModelFunction(){
		return new model.FunctionImpl();
	}
	public static void main(String[] args) {
		DAO.FriendInfo dao = (DAO.FriendInfo)ObjectFactory.getFriendDao();
		System.out.println(dao.GetALL().size());
	}
}
