package project.dao;

import project.domin.Order;

import java.net.InterfaceAddress;
import java.util.List;

public interface OrderDao {
    public Order selectOrderByUidAndEid(Integer user_id,Integer exam_id);
    public List<Order> selectOrderByUid(Integer user_id);
    public List<Order> selectGradeByUid(Integer user_id);
    public List<Order> selectUntreatedOrder();
    public List<Order> selectOrderByLevel(Integer exam_item);
    public int addOrder(Order order);
    public int deleteOrder(Integer user_id,Integer exam_id);
    public int adminMarking(Integer user_id,Integer exam_id,Integer grade);
}
