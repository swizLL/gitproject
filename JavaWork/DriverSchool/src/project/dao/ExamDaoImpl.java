package project.dao;

import com.sun.javafx.binding.StringFormatter;
import project.domin.Exam;
import project.domin.Examination;

import java.util.Date;
import java.util.List;

public class ExamDaoImpl extends BaseDao<Exam> implements ExamDao {
    @Override
    public List<Exam> getNowExam() {
        String sql="select * from exam where exam_date>=NOW() order by exam_date";
        return selectObjs(sql);
    }

    @Override
    public List<Exam> getOldExam() {
        String sql="select * from exam where exam_date<NOW() order by exam_date";
        return selectObjs(sql);
    }

    @Override
    public Exam getExamById(Integer examId) {
        String sql="select * from exam where exam_id=? order by exam_date";
        return selectObj(sql,examId);
    }

    @Override
    public List<Exam> getExamByDate(String sqlPram) {
        String sql="select * from exam where exam_date like concat(?,'%') and exam_date>=NOW() order by exam_date";
        return selectObjs(sql,sqlPram);
    }

    @Override
    public List<Exam> getExamByNation(String nation) {
        String sql="select * from exam where examination_id in " +
                "(select examination_id from examination where examination_name like concat('%',?,'%'))" +
                "and exam_date>=NOW() order by exam_date";
        return selectObjs(sql,nation);
    }

    @Override
    public List<Exam> getExamByDateAndNation(String nation,String sqlPram) {
        String sql="select * from exam where examination_id in " +
                "(select examination_id from examination where examination_name like concat('%',?,'%'))" +
                "and  exam_date like concat(?,'%')" +
                "and exam_date>=NOW() order by exam_date";
        return selectObjs(sql,nation,sqlPram);
    }

    @Override
    public Exam getExamByTimeAndNationAndItem(String time, Integer examination_id, Integer item) {
        String sql="select * from exam where exam_date=? and examination_id=? and exam_item=? order by exam_date";
        return selectObj(sql,time,examination_id,item);
    }

    @Override
    public int delete(Integer exam_id) {
        String sql="delete from exam where exam_id=?";
        return Update(sql,exam_id);
    }

    @Override
    public int deleteByExaminationId(Integer examination_id) {
        String sql="delete from exam where examination_id=? and exam_date>NOW()";
        return Update(sql,examination_id);
    }

    @Override
    public int update(Exam exam, Integer exam_id) {
        String sql="update exam set examination_id=?,exam_item=?,exam_date=?,max_number=? where exam_id=?";
        return Update(sql,exam.getExamination_id(),exam.getExam_item(),exam.getExam_date(),exam.getMax_number(),exam_id);
    }

    @Override
    public int updateOrderNum(Integer newOrderNum, Integer exam_id) {
        String sql="update exam set order_number=? where exam_id=?";
        return Update(sql,newOrderNum,exam_id);
    }

    @Override
    public int save(Exam exam) {
        String sql="insert into exam(examination_id,exam_item,exam_date,order_number,max_number) values(?,?,?,?,?)";
        return Update(sql,exam.getExamination_id(),exam.getExam_item(),
                exam.getExam_date(),exam.getOrder_number(),exam.getMax_number());
    }
}
