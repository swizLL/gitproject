package project.tools;

import project.view.Panel.AbstractMainFrame;
import project.view.Panel.ExamDataTablePanel;
import project.view.Panel.ExaminationTablePanel;
import project.view.Panel.UserOrderTablePanel;

/**
 * 页面单例管理类，主要是为了管理表格的刷新
 */
public class PanelManager {
    private static PanelManager _panelManager=new PanelManager();
    private ExamDataTablePanel examDataTablePanel;
    private UserOrderTablePanel userOrderTablePanel;
    private ExaminationTablePanel examinationTablePanel;
    private AbstractMainFrame abstractMainFrame;
    private PanelManager(){ }
    public static PanelManager getPanelManager(){
        return  _panelManager;
    }
    public ExamDataTablePanel getExamDataTablePanel() {
        return examDataTablePanel;
    }

    public UserOrderTablePanel getUserOrderTablePanel() {
        return userOrderTablePanel;
    }

    public ExaminationTablePanel getExaminationTablePanel() {
        return examinationTablePanel;
    }

    public void setExamDataTablePanel(ExamDataTablePanel examDataTablePanel) {
        this.examDataTablePanel = examDataTablePanel;
    }

    public void setUserOrderTablePanel(UserOrderTablePanel userOrderTablePanel) {
        this.userOrderTablePanel = userOrderTablePanel;
    }

    public void setExaminationTablePanel(ExaminationTablePanel examinationTablePanel) {
        this.examinationTablePanel = examinationTablePanel;
    }

    public AbstractMainFrame getAbstractMainFrame() {
        return abstractMainFrame;
    }

    public void setAbstractMainFrame(AbstractMainFrame abstractMainFrame) {
        this.abstractMainFrame = abstractMainFrame;
    }
}
