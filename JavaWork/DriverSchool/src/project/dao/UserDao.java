package project.dao;
import project.domin.User;

import java.net.InterfaceAddress;
import java.util.List;

/**
 * 数据库操作接口
 */
public interface UserDao {
    public List<User> getAll();
    public void save(User user);
    public User getUserByID(Integer id);
    public User getUserByIdCard(String idcard);
    public void delete(Integer id);
    public Integer update(User user, Integer id);
    public User userLogin(Integer id,String pwd);
}
