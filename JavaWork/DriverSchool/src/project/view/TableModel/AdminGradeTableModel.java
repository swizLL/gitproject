package project.view.TableModel;

import project.dao.*;
import project.domin.Exam;
import project.domin.Examination;
import project.domin.Order;
import project.domin.User;

import javax.swing.table.AbstractTableModel;
import java.util.List;

public class AdminGradeTableModel extends AbstractTableModel {
    private String[] head={"姓名","身份证号","考试科目","考试日期"};
    private ExamDao examDao=new ExamDaoImpl();
    private UserDao userDao=new UserDaoImpl();
    private ExaminationDao examinationDao= new ExaminationDaoImpl();
    Object[][] rows;
    public AdminGradeTableModel(List<Order> orderList){
        rows=new Object[orderList.size()][head.length];
        for(int i=0;i<orderList.size();++i){
            Order order=orderList.get(i);
            User user=userDao.getUserByID(order.getUser_id());
            Exam exam=examDao.getExamById(order.getExam_id());
            rows[i][0]=user.getName();
            rows[i][1]=user.getId_card();
            rows[i][2]=exam.getExam_item();
            rows[i][3]=order.getExam_date();
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
