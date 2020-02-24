package cn.web.dao;

import cn.web.pojo.Employee;
import cn.web.pojo.History;
import cn.web.tools.C3P0Conn;
import org.apache.commons.dbutils.QueryRunner;
import org.apache.commons.dbutils.handlers.BeanHandler;
import org.apache.commons.dbutils.handlers.BeanListHandler;

import java.sql.*;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class HistoryDao {
    QueryRunner qr=new QueryRunner();//DBUTils工具类，可以执行增删改查操作
    private Connection conn=null;
    public List<History> selectHistoryList(){
        List<History> list=null;
        conn= C3P0Conn.getConnection();
        String sql="select * from history";
        try {
            list=qr.query(conn,sql,new BeanListHandler<History>(History.class));
        } catch (SQLException e) {
            e.printStackTrace();
        }finally{
            C3P0Conn.closeConnction(conn);
        }
        return list;
    }
    public History selectHisById(int id){
        History his=null;
        conn=C3P0Conn.getConnection();
        String sql="select * from history where id=?";
        try {
            his=qr.query(conn, sql, new BeanHandler<History>(History.class), id);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return his;
    }
    public int updateHistory(History his){
        int i=0;
        conn=C3P0Conn.getConnection();
        String sql="update history set name=?,gender=?,"
                + "birthday=?,telephone=?,email=?,address=?,education=? "
                + "where id=?";

        try {
            i=qr.update(conn, sql, his.getName(),his.getGender(),his.getBirthday()
                    ,his.getTelephone(),his.getEmail(),his.getAddress(),his.getEducation(),his.getId());
            System.out.println(i);
        } catch (SQLException e) {
            e.printStackTrace();
        }finally{
            C3P0Conn.closeConnction(conn);
        }
        return i;
    }
    /**
     * 根据员工ID删除员工信息
     * @param id
     * @return 返回删除的结果 >0 说明删除成功
     */
    public int delHis(int id){
        int i=0;
        conn=C3P0Conn.getConnection();
        String sql="delete from history where id=?";
        try {
            i=qr.update(conn, sql, id);
        } catch (SQLException e) {
            e.printStackTrace();
        }finally{
            C3P0Conn.closeConnction(conn);
        }
        return i;
    }
    public int insertHis(History his){
        java.util.Date date=new Date();
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd");
        String sdate=sdf.format(date);
        int i=0;
        conn=C3P0Conn.getConnection();
        String sql="insert into history(employee_number,name,gender,birthday,telephone,"
                + "email,address,education,department_number,"
                + "position_number,out_time) values(?,?,?,?,?,?,?,?,?,?,?)";
        try {
            i=qr.update(conn, sql, his.getEmployee_number(),his.getName(),his.getGender(),his.getBirthday(),
                    his.getTelephone(),his.getEmail(),his.getAddress(),his.getEducation(),his.getDepartment_number(),his.getPosition_number(),sdate);
        } catch (SQLException e) {
            e.printStackTrace();
        }finally{
            C3P0Conn.closeConnction(conn);
        }
        return i;
    }
   /* public List<History> selectHistoryList(){
        List<History> list=new ArrayList<History>();
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            conn = DriverManager.getConnection("jdbc:mysql://127.0.0.1:3306/db01?useSSL=false&serverTimezone=UTC&", "root", "123456");

            String sql="select * from history";
            PreparedStatement pstmt=conn.prepareStatement(sql);

            ResultSet rs=pstmt.executeQuery();
            while(rs.next()){
                History h=new History();
                h.setId(rs.getInt("id"));
                h.setName(rs.getString("name"));
                h.setGender(rs.getString("gender"));
                h.setBirthday(rs.getString("birthday"));
                h.setTelephone(rs.getString("telephone"));
                h.setEmail(rs.getString("email"));
                h.setAddress(rs.getString("address"));
                h.setPhoto(rs.getString("photo"));
                h.setEducation(rs.getString("education"));
                h.setDepartment_number(rs.getInt("department_number"));
                h.setPosition_number(rs.getInt("position_number"));
                h.setIn_time(rs.getString("in_time"));
                h.setOut_time(rs.getString("out_time"));
                list.add(h);
            }
            rs.close();
            pstmt.close();
            conn.close();
        }catch(Exception e){
            System.out.println(e);
        }


        return list;
    }*/

    public static void main(String[] args) {
        HistoryDao historyDao=new HistoryDao();
        History his =historyDao.selectHisById(1);
        System.out.println(his);
    }
}
