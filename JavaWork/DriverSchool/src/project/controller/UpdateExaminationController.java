package project.controller;

import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
import project.domin.Examination;
import project.tools.PanelManager;
import project.view.Panel.AbstractUpdateExaminationFrame;

import javax.swing.*;

public class UpdateExaminationController extends AbstractUpdateExaminationFrame {
    ExaminationDao examinationDao=new ExaminationDaoImpl();
    @Override
    public void confirm() {
        Examination examination=new Examination(getAddressText().getText(),getExaminationNameText().getText());
        if(!exitSameExamination(examination)){
            int i=examinationDao.save(examination);
            if(i==0){
                JOptionPane.showMessageDialog(null, "添加失败");
                return;
            }else{
                this.setVisible(false);
                JOptionPane.showMessageDialog(null,"添加成功");
                PanelManager.getPanelManager().getExaminationTablePanel().refresh();
            }
        }else{
            JOptionPane.showMessageDialog(null, "同一地区已存在相同考场！");
        }
    }

    @Override
    public void reset() {
        this.getAddressText().setText("");
        this.getExaminationNameText().setText("");
    }

    @Override
    public void cancel() {
        this.setVisible(false);
        PanelManager.getPanelManager().getExaminationTablePanel().refresh();
    }

    @Override
    public void alter() {
        Examination examination=new Examination(getAddressText().getText(),getExaminationNameText().getText());
        if(!exitSameExamination(examination)){
            int i=examinationDao.updateByID(examination,Integer.valueOf(getExaminationIDValue().getText()));
            if(i==0){
                JOptionPane.showMessageDialog(null, "修改失败");
                return;
            }else{
                this.setVisible(false);
                JOptionPane.showMessageDialog(null,"修改成功");
                PanelManager.getPanelManager().getExaminationTablePanel().refresh();
            }
        }else{
            JOptionPane.showMessageDialog(null, "已存在同样的考场，不需要修改！");
        }
    }
    private boolean exitSameExamination(Examination examination){
        Examination sameExamination=examinationDao.selectByAddressAndName(examination);
        if(sameExamination==null){
            return false;
        }else{
            return true;
        }
    }
}
