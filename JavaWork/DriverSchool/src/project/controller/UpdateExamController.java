package project.controller;

import project.dao.ExamDao;
import project.dao.ExamDaoImpl;
import project.domin.Exam;
import project.tools.PanelManager;
import project.view.Panel.AbstractUpdateExamFrame;

import javax.swing.*;
import java.util.Date;

public class UpdateExamController extends AbstractUpdateExamFrame {
    //数字匹配
    private static String numPattern = "^[0-9]*$";
    ExamDao examDao = new ExamDaoImpl();

    @Override
    public void confirm() {
        Date enterDate = getDateChooser().getDate();
        Date nowDate = new Date();
        boolean isDateAfter = enterDate.after(nowDate);
        if (!isDateAfter) {
            JOptionPane.showMessageDialog(null, "您输入的考试日期不正确！");
            return;
        }
        Exam exam = new Exam(getExaminationIDArray()[getExaminationBox().getSelectedIndex()],
                getLevel()[getLevelBox().getSelectedIndex()],
                getDateChooser().getText(), 0, getMaxNumber()[getMaxNumBox().getSelectedIndex()]);
        if (!exitSameExam(exam)) {
            int i = examDao.save(exam);
            if (i == 0) {
                JOptionPane.showMessageDialog(null, "添加失败");
                return;
            } else {
                this.setVisible(false);
                JOptionPane.showMessageDialog(null, "添加成功");
                PanelManager.getPanelManager().getExamDataTablePanel().refresh();
            }
        } else {
            JOptionPane.showMessageDialog(null, "已存在此场考试！");
        }
    }

    @Override
    public void reset() {
        this.getExaminationBox().setSelectedIndex(0);
        this.getLevelBox().setSelectedIndex(0);
        this.getDateChooser().setDate(new Date());
        this.getMaxNumBox().setSelectedIndex(0);
    }

    @Override
    public void cancel() {
        this.setVisible(false);
        PanelManager.getPanelManager().getExamDataTablePanel().refresh();
    }

    @Override
    public void alter() {
        Date enterDate = getDateChooser().getDate();
        Date nowDate = new Date();
        boolean isDateAfter = enterDate.after(nowDate);
        if (!isDateAfter) {
            JOptionPane.showMessageDialog(null, "您输入的考试日期不正确！");
            return;
        }
        Exam exam = new Exam(getExaminationIDArray()[getExaminationBox().getSelectedIndex()],
                getLevel()[getLevelBox().getSelectedIndex()],
                getDateChooser().getText(), 0, getMaxNumber()[getMaxNumBox().getSelectedIndex()]);
        if (!exitSameExam(exam)) {
            int ensure = JOptionPane.showConfirmDialog(null, "你确定要修改吗？");
            if (ensure == 0) {
                int i = examDao.update(exam, PanelManager.getPanelManager().getExamDataTablePanel().selectExam);
                if (i == 0) {
                    JOptionPane.showMessageDialog(null, "修改失败！");
                    return;
                } else {
                    this.setVisible(false);
                    JOptionPane.showMessageDialog(null, "修改成功！");
                    PanelManager.getPanelManager().getExamDataTablePanel().refresh();
                }
            }
        }else {
            JOptionPane.showMessageDialog(null, "存在相同的考试信息，不需要修改！");
        }
    }


    public boolean exitSameExam(Exam exam) {
        Exam tempExam = examDao.getExamByTimeAndNationAndItem(exam.getExam_date(), exam.getExamination_id(), exam.getExam_item());
        if (tempExam == null) {
            return false;
        } else {
            return true;
        }
    }
}
