package project.dao;

import project.domin.Order;

import java.util.List;

public class OrderDaoImpl extends BaseDao<Order> implements OrderDao{
    @Override
    public Order selectOrderByUidAndEid(Integer user_id, Integer exam_id) {
        String sql="select * from userorder where user_id=? and exam_id=?";
        return selectObj(sql,user_id,exam_id);

    }

    @Override
    public List<Order> selectOrderByUid(Integer user_id) {
        String sql="select * from userorder where user_id=? and exam_date>=NOW()";
        return selectObjs(sql,user_id);
    }

    @Override
    public List<Order> selectGradeByUid(Integer user_id) {
        String sql="select * from userorder where user_id=? and exam_date<NOW() and not grade=0";
        return selectObjs(sql,user_id);
    }

    @Override
    public List<Order> selectUntreatedOrder() {
        String sql="select * from userorder where exam_date<NOW() and grade = 0";
        return selectObjs(sql);
    }

    @Override
    public List<Order> selectOrderByLevel(Integer exam_item) {
        String sql="select * from userorder where exam_id in (select exam_id from exam where exam_item=?) and exam_date>=NOW()";
        return selectObjs(sql,exam_item);
    }

    @Override
    public int addOrder(Order order) {
        String sql="insert into userorder values(?,?,?,?,?)";
        return Update(sql,order.getUser_id(),order.getExam_id(),order.getExamination_id(),order.getExam_date(),order.getGrade());
    }

    @Override
    public int deleteOrder(Integer user_id, Integer exam_id) {
        String sql="delete from userorder where user_id=? and exam_id=?";
        return Update(sql,user_id,exam_id);
    }

    @Override
    public int adminMarking(Integer user_id, Integer exam_id, Integer grade) {
        String sql="update userorder set grade=? where user_id=? and exam_id=?";
        return Update(sql,grade,user_id,exam_id);
    }
}
