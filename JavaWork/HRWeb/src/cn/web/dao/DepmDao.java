package cn.web.dao;

import cn.web.pojo.Department;
import cn.web.tools.C3P0Conn;
import org.apache.commons.dbutils.QueryRunner;
import org.apache.commons.dbutils.handlers.BeanHandler;
import org.apache.commons.dbutils.handlers.BeanListHandler;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;

public class DepmDao {
    QueryRunner qr=new QueryRunner();//DBUTils工具类，可以执行增删改查操作
    private Connection conn=null;
    public List<Department> selectDepmList(){
        List<Department> list=null;
        conn= C3P0Conn.getConnection();
        String sql="select * from department";
        try {
            list=qr.query(conn, sql, new BeanListHandler<Department>(Department.class));
        } catch (SQLException e) {
            e.printStackTrace();
        }finally{
            C3P0Conn.closeConnction(conn);
        }
        return list;
    }

    public Department selectDempById(int id) {
        Department depm=null;
        conn=C3P0Conn.getConnection();
        String sql="select * from department where id=?";
        try {
            depm=qr.query(conn, sql, new BeanHandler<Department>(Department.class), id);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return depm;
    }

    public int updateDepm(Department depm) {
        int i=0;
        conn=C3P0Conn.getConnection();
        String sql="update department set manager=?,"
                + "telephone=?,address=?,notes=? "
                + "where id=?";

        try {
            i=qr.update(conn, sql, depm.getManager(),depm.getTelephone(),depm.getAddress(),depm.getNotes(),depm.getId());
            System.out.println(i);
        } catch (SQLException e) {
            e.printStackTrace();
        }finally{
            C3P0Conn.closeConnction(conn);
        }
        return i;
    }
}
