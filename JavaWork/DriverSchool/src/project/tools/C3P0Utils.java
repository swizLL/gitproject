package project.tools;

import com.mchange.v2.c3p0.ComboPooledDataSource;
import com.mchange.v2.c3p0.DataSources;
import org.junit.jupiter.api.Test;

import javax.sql.DataSource;
import java.beans.PropertyVetoException;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class C3P0Utils {
   /*
   //使用c3p0连接
    @Test
    public void c3p0GetConnection() throws PropertyVetoException, SQLException {
        ComboPooledDataSource cpds = new ComboPooledDataSource();
        cpds.setDriverClass("com.mysql.cj.jdbc.Driver");
        cpds.setJdbcUrl("jdbc:mysql://localhost:3306/lanqiao?useSSL=false&serverTimezone=UTC&useUnicode=true&characterEncoding=utf8");
        cpds.setUser("root");
        cpds.setPassword("123456");
        cpds.setMinPoolSize(5);
        cpds.setAcquireIncrement(5);
        cpds.setMaxPoolSize(20);
        Connection conn = cpds.getConnection();
        System.out.println(conn);
    }
    //使用配置文件和c3p0连接数据库
    @Test
    public void c3p0ConnectionTest() throws SQLException {//使用配置文件
        //创建数据源
        ComboPooledDataSource cpds = new ComboPooledDataSource("mysql");
        //获取链接
        Connection conn = cpds.getConnection();
        System.out.println(conn);
    }

    */

   //单例数据库连接
    public static ComboPooledDataSource ds=new ComboPooledDataSource();
    //自动加载c3p0-config文件,获取数据源
    public static DataSource getDataSources(){
        return ds;
    }
    //从C3P0获取连接的方法
    public static Connection getConnection() throws SQLException {
        return ds.getConnection();
    }
    //关闭连接的方法
    public static void closeAll(Connection conn, Statement st, ResultSet rs){
        //负责关闭
        if(conn!=null){
            try {
                conn.close();
            }catch (SQLException e){
                e.printStackTrace();
            }
        }
        //关闭statment
        if(st!=null){
            try{
                st.close();
            }catch (SQLException e){
                e.printStackTrace();
            }
        }
        //关闭ResultSet
        if(rs!=null){
            try{
                rs.close();
            }catch (SQLException e){
                e.printStackTrace();
            }
        }
    }
}
