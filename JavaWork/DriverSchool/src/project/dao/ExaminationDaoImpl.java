package project.dao;

import com.sun.xml.internal.bind.v2.model.core.ID;
import project.domin.Examination;

import java.util.List;

public class ExaminationDaoImpl extends BaseDao<Examination> implements ExaminationDao {

    @Override
    public List<Examination> selectAllExamination() {
        String sql="select * from examination order by examination_id asc";
        return selectObjs(sql);
    }

    @Override
    public Examination selectByID(int id) {
        String sql="select * from examination where examination_id=?";
        return selectObj(sql,id);
    }

    @Override
    public Examination selectByAddressAndName(Examination examination) {
        String sql="select * from examination where address=? and examination_name=?";
        return selectObj(sql,examination.getAddress(),examination.getExamination_name());
    }

    @Override
    public List<Examination> selectByAddressStrAndNameStr(String name, String address) {
        String sql="select * from examination where examination_name like concat('%',?,'%')"+
                "and  address like concat('%',?,'%')";
        return selectObjs(sql,name,address);
    }

    @Override
    public Integer save(Examination examination) {
        String sql="insert into examination(address,examination_name) values(?,?)";
        return Update(sql,examination.getAddress(),examination.getExamination_name());
    }

    @Override
    public Integer updateByID(Examination examination, Integer examination_id) {
        String sql="update examination set address=?,examination_name=? where examination_id=?";
        return Update(sql,examination.getAddress(),examination.getExamination_name(),examination_id);
    }

    @Override
    public Integer delete(Integer examination_id) {
        String sql="delete from examination where examination_id=?";
        return Update(sql,examination_id);
    }

}
