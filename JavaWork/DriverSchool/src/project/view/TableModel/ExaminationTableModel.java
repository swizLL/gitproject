package project.view.TableModel;

import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
import project.domin.Exam;
import project.domin.Examination;
import project.domin.Order;
import project.domin.User;

import javax.swing.table.AbstractTableModel;
import java.util.List;

public class ExaminationTableModel extends AbstractTableModel {
    private String[] head={"考场号","考场地址","考场名"};
    private ExaminationDao examinationDao= new ExaminationDaoImpl();
    Object[][] rows;
    public ExaminationTableModel(List<Examination> examinationList){
        rows=new Object[examinationList.size()][head.length];
        for(int i=0;i<examinationList.size();++i){
            Examination examination=examinationList.get(i);
            rows[i][0]=examination.getExamination_id();
            rows[i][1]=examination.getAddress();
            rows[i][2]=examination.getExamination_name();
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
