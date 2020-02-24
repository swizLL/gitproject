package project.dao;

import org.junit.jupiter.api.Test;
import project.domin.User;

import java.util.List;

public class UserDaoImpl extends BaseDao<User> implements UserDao {

    public User userLogin(Integer id,String pwd){
        String sql="select * from user where user_id=? and password=?";
        return selectObj(sql,id,pwd);
    }
    @Override
    public List<User> getAll() {
        String sql = "select * from user";
        return selectObjs(sql);
    }

    @Override
    public void save(User user) {
        String sql = "insert into user values(?,?,?,?,?,?,?,?)";
        Update(sql, user.getUser_id(), user.getId_card(), user.getPassword(), user.getName(), user.getSex(), user.getPhone(), user.getAge(), user.getExam_status());
    }

    @Override
    public User getUserByID(Integer id) {
        String sql = "select * from user where user_id=?";
        return selectObj(sql, id);
    }

    @Override
    public User getUserByIdCard(String idcard) {
        String sql = "select * from user where id_card=?";
        return selectObj(sql,idcard);
    }

    @Override
    public void delete(Integer id) {
        String sql = "delete from user where user_id=?";
        Update(sql,id);
    }

    @Override
    public Integer update(User user,Integer id) {
        String sql = "update user set id_card=?,password=?,name=?,sex=?,phone=?,age=?,exam_status=? where user_id=?";
        return Update(sql,user.getId_card(), user.getPassword(), user.getName(), user.getSex(), user.getPhone(), user.getAge(), user.getExam_status(),id);
    }
}

