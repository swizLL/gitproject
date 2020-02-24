package project.view.TableModel;

import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
import project.domin.Exam;
import project.domin.Examination;

import javax.swing.table.AbstractTableModel;
import java.util.List;

public class ExamDataTableModel extends AbstractTableModel {
    private String[] head={"考试号","考场号","考试等级","考试日期","考场名","考场地址","已预约人数","最大容纳人数"};
    private ExaminationDao examinationDao=new ExaminationDaoImpl();
    Object[][] rows;
    public ExamDataTableModel(List<Exam> examList){
        rows = new Object[examList.size()][head.length];
        for(int i=0;i<examList.size();++i){
            Exam exam= examList.get(i);
            Examination examination=examinationDao.selectByID(exam.getExamination_id());
            rows[i][0]=exam.getExam_id();
            rows[i][1]=exam.getExamination_id();
            rows[i][2]=exam.getExam_item();
            rows[i][3]=exam.getExam_date();
            rows[i][4]=examination.getExamination_name();
            rows[i][5]=examination.getAddress();
            rows[i][6]=exam.getOrder_number();
            rows[i][7]=exam.getMax_number();
        }
    }
    @Override
    public int getRowCount() {
        return rows.length;
    }

    @Override
    public int getColumnCount() {
        return head.length;
    }

    @Override
    public Object getValueAt(int rowIndex, int columnIndex) {
        return rows[rowIndex][columnIndex];
    }

    @Override
    public String getColumnName(int column) {
        return head[column];
    }

    @Override
    public boolean isCellEditable(int rowIndex, int columnIndex) {
        return false;
    }
}
