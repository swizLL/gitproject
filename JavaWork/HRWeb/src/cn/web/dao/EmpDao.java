package cn.web.dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import org.apache.commons.dbutils.QueryRunner;
import org.apache.commons.dbutils.handlers.BeanHandler;
import org.apache.commons.dbutils.handlers.BeanListHandler;

import cn.web.pojo.Employee;
import cn.web.tools.C3P0Conn;

public class EmpDao {
	QueryRunner qr=new QueryRunner();//DBUTils工具类，可以执行增删改查操作
	private Connection conn=null;
	/**
	 * 员 工登录查询
	 * @param num 工号
	 * @param pwd 密码
	 * @return 返回登录成功的员工信息
	 */
	public Employee selectLogin(int num,String pwd){
		Employee emp=null;
		conn=C3P0Conn.getConnection();
		String sql="select * from employee where employee_number=? and password=?";
		try {
			emp=qr.query(conn, sql, new BeanHandler<Employee>(Employee.class), num,pwd);
		} catch (SQLException e) {
			e.printStackTrace();
		}finally{
			C3P0Conn.closeConnction(conn);
		}
		return emp;
	}
	/**
	 * 查询所有的用户
	 * @return
	 */
	public List<Employee> selectEmpList(){
		List<Employee> list=null;
		conn=C3P0Conn.getConnection();
		String sql="select * from employee";
		try {
			list=qr.query(conn, sql, new BeanListHandler<Employee>(Employee.class));
		} catch (SQLException e) {
			e.printStackTrace();
		}finally{
			C3P0Conn.closeConnction(conn);
		}
		return list;
	}
	/**
	 * 根据员工ID删除员工信息
	 * @param id
	 * @return 返回删除的结果 >0 说明删除成功
	 */
	public int delEmp(int id){
		int i=0;
		conn=C3P0Conn.getConnection();
		String sql="delete from employee where id=?";
		try {
			i=qr.update(conn, sql, id);
		} catch (SQLException e) {
			e.printStackTrace();
		}finally{
			C3P0Conn.closeConnction(conn);
		}
		return i;
	}
	/**
	 * 增加员工信息
	 * @param emp
	 * @return
	 */
	public int insertEmp(Employee emp){
		Date date=new Date();
		SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd");
		String sdate=sdf.format(date);
		int i=0;
		conn=C3P0Conn.getConnection();
		String sql="insert into employee(employee_number,name,password,gender,birthday,telephone,"
				+ "email,address,education,department_number,"
				+ "position_number,in_time) values(?,?,?,?,?,?,?,?,?,?,?,?)";
		try {
			i=qr.update(conn, sql, emp.getEmployee_number(),emp.getName(),emp.getPassword(),emp.getGender(),emp.getBirthday(),
					emp.getTelephone(),emp.getEmail(),emp.getAddress(),emp.getEducation(),emp.getDepartment_number(),emp.getPosition_number(),sdate);
		} catch (SQLException e) {
			e.printStackTrace();
		}finally{
			C3P0Conn.closeConnction(conn);
		}
		return i;
	}
	/**
	 * 根据员工ID查询员工信息
	 * @param id
	 * @return
	 */
	public Employee selectEmpById(int id){
		Employee emp=null;
		conn=C3P0Conn.getConnection();
		String sql="select * from employee where id=?";
		try {
			emp=qr.query(conn, sql, new BeanHandler<Employee>(Employee.class), id);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return emp;
	}
	/**
	 * 根据员工ID修改员工信息
	 * @param emp
	 * @return
	 */
	public int updateEmp(Employee emp){
		int i=0;
		conn=C3P0Conn.getConnection();
		String sql="update employee set name=?,password=?,gender=?,"
				+ "birthday=?,telephone=?,email=?,address=?,education=? "
				+ "where id=?";
	
		try {
			i=qr.update(conn, sql, emp.getName(),emp.getPassword(),emp.getGender(),emp.getBirthday()
					,emp.getTelephone(),emp.getEmail(),emp.getAddress(),emp.getEducation(),emp.getId());
			System.out.println(i);
		} catch (SQLException e) {
			e.printStackTrace();
		}finally{
			C3P0Conn.closeConnction(conn);
		}
		return i;
	}
	
//	public int updateEmp(Employee emp){
//		int i=0;
//		
//		try {
//			Class.forName("com.mysql.jdbc.Driver");
//			conn=DriverManager.getConnection("jdbc:mysql://127.0.0.1:3306/db01","root","123456");
//			String sql="update employee set name=?,password=?,gender=?,"
//					+ "birthday=?,telephone=?,email=?,address=?,education=? "
//					+ "where id=?";
//			PreparedStatement pstmt=conn.prepareStatement(sql);
//			pstmt.setString(1, emp.getName());
//			pstmt.setString(2, emp.getPassword());
//			pstmt.setString(3, emp.getGender());
//			pstmt.setString(4, emp.getBirthday());
//			pstmt.setString(5, emp.getTelephone());
//			pstmt.setString(6, emp.getEmail());
//			pstmt.setString(7, emp.getAddress());
//			pstmt.setString(8, emp.getEducation());
//			pstmt.setInt(9, emp.getId());
//			i=pstmt.executeUpdate();
//			System.out.println("---"+i);
//			pstmt.close();
//			conn.close();
//		} catch (Exception e) {
//			e.printStackTrace();
//		}
//		return i;
//	}
	
	
	/*public static void main(String[] args) {
		EmpDao dao=new EmpDao();
		Employee emp=new Employee();
		emp.setId(9);
		emp.setName("aa");
		int i=dao.updateEmp(emp);
	}*/
}
