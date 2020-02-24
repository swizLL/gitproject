package project.dao;

import org.apache.commons.dbutils.QueryRunner;
import org.apache.commons.dbutils.handlers.BeanHandler;
import org.apache.commons.dbutils.handlers.BeanListHandler;
import project.tools.C3P0Utils;

import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.sql.SQLException;
import java.util.List;

public class BaseDao<T> {
    private QueryRunner qr= new QueryRunner(C3P0Utils.getDataSources());
    private Class<T> clazz;
    /**
     * 构造方法
     */
    public BaseDao() {
        Type superClass = this.getClass().getGenericSuperclass();
        if(superClass instanceof ParameterizedType){
            ParameterizedType parameterizedType = (ParameterizedType) superClass;
            Type[] typeArgs = parameterizedType.getActualTypeArguments();
            if(typeArgs != null && typeArgs.length>0){
                if(typeArgs[0] instanceof Class){
                    clazz = (Class<T>) typeArgs[0];
                }
            }
        }
    }
    /**
     * 更新数据
     */
    public int Update(String sql,Object ...args){
        int i=0;
        try {
            i=qr.update(sql, args);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return i;
    }
    /**
     *查询数据
     */
    public T selectObj(String sql,Object...args){
        try {
            return qr.query(sql,new BeanHandler<>(clazz),args);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }
    /**
     * 查询多个数据
     */
    public List<T> selectObjs(String sql,Object...args){
        try {
            return qr.query(sql,new BeanListHandler<>(clazz),args);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }
}
