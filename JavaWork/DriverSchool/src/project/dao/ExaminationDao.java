package project.dao;

import project.domin.Exam;
import project.domin.Examination;

import java.util.List;

public interface ExaminationDao {
    public List<Examination> selectAllExamination();
    public Examination selectByID(int id);
    public Examination selectByAddressAndName(Examination examination);
    public List<Examination> selectByAddressStrAndNameStr(String name,String address);
    public Integer save(Examination examination);
    public Integer updateByID(Examination examination,Integer examination_id);
    public Integer delete(Integer examination_id);
}
