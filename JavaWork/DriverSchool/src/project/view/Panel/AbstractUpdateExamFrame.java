package project.view.Panel;


import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.controller.UpdateExamController;
import project.dao.ExamDao;
import project.dao.ExamDaoImpl;
import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
import project.domin.Exam;
import project.domin.Examination;
import project.tools.DateChooserJButton;
import project.tools.ImagePanel;
import project.tools.PanelManager;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.File;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.List;

enum UPDATETYPE {
    ALTER,
    INSERT
}

public abstract class AbstractUpdateExamFrame extends JFrame {
    static {
        try {
            //设置本属性将改变窗口边框样式定义
            BeautyEyeLNFHelper.frameBorderStyle = BeautyEyeLNFHelper.FrameBorderStyle.translucencyAppleLike;
            org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper.launchBeautyEyeLNF();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private JPanel panel = new JPanel(null);
    //组件标签部分
    private JLabel Title=new JLabel("添加考试场次");
    private JLabel examIDLabel = new JLabel("考试号：");
    private JLabel examIDValue = new JLabel("");
    private JLabel examinationLabel = new JLabel("考  场：");
    private JLabel examItemLabel = new JLabel("科  目：");
    private JLabel dateLabel = new JLabel("日  期：");
    private JLabel maxNumLabel = new JLabel("最大人数：");


    ExaminationDao examinationDao = new ExaminationDaoImpl();
    ExamDao examDao=new ExamDaoImpl();
    //组件部分
    //获取考场名和考场号的Map
    Map<String, Integer> examinationList = getNewExaminationList();
    //将map的keySet转化位字符转数组
    String[] examinationArray = examinationList.keySet().toArray(new String[examinationList.keySet().size()]);
    Integer[] examinationIDArray = examinationList.values().toArray(new Integer[examinationList.values().size()]);
    private JComboBox examinationBox = new JComboBox(examinationArray);
    Integer level[] = {1, 2, 3, 4};
    private JComboBox levelBox = new JComboBox(level);
    private DateChooserJButton dateChooser = new DateChooserJButton(new SimpleDateFormat("yyyy/MM/dd HH"), "2020/1/1 0");
    Integer maxNumber[] = {25, 30, 35, 40, 45, 50, 55, 60};
    private JComboBox maxNumBox = new JComboBox(maxNumber);
    //按钮
    private JButton confirmBtn = new JButton("添加");
    private JButton cancelBtn = new JButton("取消");
    private JButton resetBtn = new JButton("重置");

    private UPDATETYPE type;

    //初始化窗口
    public void init(UPDATETYPE updateType) {
        this.type = updateType;
        this.setBounds(600, 200, 800, 600);
        this.setTitle("驾校考试管理系统");
        this.setResizable(false);
        try {
            this.setIconImage(ImageIO.read(new File("src/image/logo.jpg")));
        } catch (IOException e) {
            e.printStackTrace();
        }
        this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
        this.addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(WindowEvent e) {
                cancel();
            }
        });
        addContent();
        this.setVisible(true);
    }

    public void addContent() {
        this.add(this.panel);
        Font font = new Font("黑体", Font.BOLD, 35);
        Title.setFont(font);
        Title.setBounds(275,30,400,50);
        this.panel.add(Title);
        //初始化界面值为选择的考试记录的值
        if (type == UPDATETYPE.ALTER) {
            Title.setText("考试信息修改");
            examIDLabel.setBounds(250, 100, 70, 30);
            this.panel.add(examIDLabel);
            examIDValue.setText(PanelManager.getPanelManager().getExamDataTablePanel().selectExam.toString());
            examIDValue.setBounds(320, 100, 200, 30);
            this.panel.add(examIDValue);
            //设置现在的控件的值
            Exam exam=examDao.getExamById(PanelManager.getPanelManager().getExamDataTablePanel().selectExam);//获取当前选择的exam；
            int idIndex=getIndex(examinationIDArray,exam.getExamination_id());
            getExaminationBox().setSelectedIndex(idIndex);
            int levelIndex=getIndex(level,exam.getExam_item());
            getLevelBox().setSelectedIndex(levelIndex);
            SimpleDateFormat format=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
            try {
                Date date=format.parse(exam.getExam_date());
                getDateChooser().setDate(date);
            } catch (ParseException e) {
                e.printStackTrace();
            }
            int maxNumIndex=getIndex(maxNumber,exam.getMax_number());
            getMaxNumBox().setSelectedIndex(maxNumIndex);
        }
        this.panel.add(examinationLabel);
        examinationLabel.setBounds(250, 150, 70, 30);
        this.panel.add(examinationBox);
        examinationBox.setBounds(320, 150, 200, 30);
        this.panel.add(examItemLabel);
        examItemLabel.setBounds(250, 200, 70, 30);
        this.panel.add(levelBox);
        levelBox.setBounds(320, 200, 200, 30);
        this.panel.add(dateLabel);
        dateLabel.setBounds(250, 250, 70, 30);
        this.panel.add(dateChooser);
        dateChooser.setBounds(320, 250, 200, 30);
        this.panel.add(maxNumLabel);
        maxNumLabel.setBounds(250, 300, 75, 30);
        this.panel.add(maxNumBox);
        maxNumBox.setBounds(320, 300, 200, 30);
        if(type==UPDATETYPE.ALTER){
            confirmBtn.setText("修改");
        }
        confirmBtn.setBounds(200, 400, 80, 30);
        this.panel.add(confirmBtn);
        this.panel.add(resetBtn);
        resetBtn.setBounds(350, 400, 80, 30);
        this.panel.add(cancelBtn);
        cancelBtn.setBounds(500, 400, 80, 30);
        ImagePanel imagePanel=new ImagePanel(new ImageIcon("src/image/whiteGround.jpg").getImage());
        imagePanel.setBounds(0,0,800,600);
        panel.add(imagePanel);
        addBtnListener();
    }

    //按钮事件注册
    public void addBtnListener() {
        this.confirmBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if(type==UPDATETYPE.INSERT){
                    confirm();
                }else{
                    alter();
                }
            }

        });
        this.resetBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                reset();
            }
        });
        this.cancelBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                cancel();
            }
        });
    }

    /**
     * 获取所有考场的考场名和考场地址
     *
     * @return
     */
    public Map<String, Integer> getNewExaminationList() {
        List<Examination> examinations = examinationDao.selectAllExamination();
        Map<String, Integer> examinationListTemp = new HashMap<>();
        for (int i = 0; i < examinations.size(); ++i) {
            String examination = examinations.get(i).getExamination_name() + "(" + examinations.get(i).getExamination_id() + ")";
            examinationListTemp.put(examination, examinations.get(i).getExamination_id());
        }
        return examinationListTemp;
    }

    public int getIndex(Object[] arr, Object value){
        for(int i=0;i<arr.length;++i){
            if(arr[i].equals(value)){
                return i;
            }
        }
        return -1;
    }
    public Integer[] getExaminationIDArray() {
        return examinationIDArray;
    }

    public JComboBox getExaminationBox() {
        return examinationBox;
    }

    public Integer[] getLevel() {
        return level;
    }

    public JComboBox getLevelBox() {
        return levelBox;
    }

    public DateChooserJButton getDateChooser() {
        return dateChooser;
    }

    public Integer[] getMaxNumber() {
        return maxNumber;
    }

    public JComboBox getMaxNumBox() {
        return maxNumBox;
    }

    public abstract void confirm();

    public abstract void reset();

    public abstract void cancel();

    public abstract void alter();
/*
    public static void main(String[] args) {
        new UpdateExamController().init(UPDATETYPE.INSERT);
    }*/

}
