package project.controller;

import project.dao.ExamDao;
import project.dao.ExamDaoImpl;
import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
import project.domin.Examination;
import project.view.TableModel.ExaminationTableModel;
import project.view.Panel.ExaminationTablePanel;

import javax.swing.*;
import java.util.List;

public class ExaminationTableController extends ExaminationTablePanel {
    private ExaminationDao examinationDao=new ExaminationDaoImpl();
    private ExamDao examDao=new ExamDaoImpl();
    public ExaminationTableController(List<Examination> examinationList) {
        super(examinationList);
    }

    @Override
    public void delExamination() {
        // 先删除考试记录中有关考场的记录，再删除考场信息
        int ensure=JOptionPane.showConfirmDialog(null,"你确定要删除考场及此考场相关的考试信息吗？");
        if(ensure==0){
            int i=examDao.deleteByExaminationId(this.getSelectExamination().getExamination_id());
            int j=examinationDao.delete(this.getSelectExamination().getExamination_id());
            if(i==0||j==0){
                JOptionPane.showMessageDialog(null,"删除失败！");
            }else{
                JOptionPane.showMessageDialog(null,"删除成功！");
            }
            refresh();
        }
    }

    @Override
    public void refresh() {
        List<Examination> examinations=examinationDao.selectAllExamination();
        this.setTable(new JTable(new ExaminationTableModel(examinations)));
        addTableClick(examinations);
        this.getTablePanel().setViewportView(this.getTable());
        this.setSelectExamination(null);
    }
    @Override
    public void refresh(List<Examination> examinationList) {
        this.setTable(new JTable(new ExaminationTableModel(examinationList)));
        addTableClick(examinationList);
        this.getTablePanel().setViewportView(this.getTable());
        this.setSelectExamination(null);
    }
    @Override
    public void search(String name,String address) {
        List<Examination> examinations=examinationDao.selectByAddressStrAndNameStr(name,address);
        refresh(examinations);
    }
}
