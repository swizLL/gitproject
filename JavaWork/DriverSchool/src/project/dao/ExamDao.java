package project.dao;

import com.oracle.xmlns.internal.webservices.jaxws_databinding.ExistingAnnotationsType;
import project.domin.Exam;
import project.domin.User;

import java.util.List;

public interface ExamDao {
    public List<Exam> getNowExam();
    public List<Exam> getOldExam();
    public Exam getExamById(Integer examId);
    public List<Exam> getExamByDate(String sqlPram);
    public List<Exam> getExamByNation(String nation);
    public List<Exam> getExamByDateAndNation(String nation,String sqlPram);
    public Exam getExamByTimeAndNationAndItem(String time,Integer examination_id,Integer item);
    public int delete(Integer exam_id);
    public int deleteByExaminationId(Integer examination_id);
    public int update(Exam exam, Integer exam_id);
    public int updateOrderNum(Integer newOrderNum,Integer exam_id);
    public int save(Exam exam);
}
