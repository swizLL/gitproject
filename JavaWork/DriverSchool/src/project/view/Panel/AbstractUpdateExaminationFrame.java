package project.view.Panel;

import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.controller.UpdateExaminationController;
import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
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

public abstract class AbstractUpdateExaminationFrame extends JFrame {
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
    private JLabel Title=new JLabel("添加考场");
    private JLabel examinationIDLabel = new JLabel("考场号：");
    private JLabel examinationIDValue = new JLabel("");
    private JLabel addressLabel = new JLabel("考场地址：");
    private JLabel examinationNameLabel = new JLabel("考场名：");


    ExaminationDao examinationDao = new ExaminationDaoImpl();
    //组件部分
    private JTextField addressText=new JTextField();
    private JTextField examinationNameText=new JTextField();
    //按钮
    private JButton confirmBtn = new JButton("添加");
    private JButton cancelBtn = new JButton("取消");
    private JButton resetBtn = new JButton("重置");

    private UPDATETYPE type;

    //初始化窗口
    public void init(UPDATETYPE updateType) {
        this.type = updateType;
        this.setBounds(700, 250, 600, 500);
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
        Title.setBounds(200,30,400,50);
        this.panel.add(Title);
        //初始化界面值为选择的考试记录的值
        if (type == UPDATETYPE.ALTER) {
            Title.setText("修改考场");
            examinationIDLabel.setBounds(100, 100, 70, 30);
            this.panel.add(examinationIDLabel);
            examinationIDValue.setText(PanelManager.getPanelManager().getExaminationTablePanel().getSelectExamination().getExamination_id().toString());
            examinationIDValue.setBounds(200, 100, 200, 30);
            this.panel.add(examinationIDValue);
            addressText.setText(PanelManager.getPanelManager().getExaminationTablePanel().getSelectExamination().getAddress());
            examinationNameText.setText(PanelManager.getPanelManager().getExaminationTablePanel().getSelectExamination().getExamination_name());
        }
        this.panel.add(addressLabel);
        addressLabel.setBounds(100, 150, 75, 30);
        this.panel.add(addressText);
        addressText.setBounds(200, 150, 200, 30);
        this.panel.add(examinationNameLabel);
        examinationNameLabel.setBounds(100, 200, 70, 30);
        this.panel.add(examinationNameText);
        examinationNameText.setBounds(200, 200, 200, 30);
        if(type==UPDATETYPE.ALTER){
            confirmBtn.setText("修改");
        }
        confirmBtn.setBounds(80, 300, 80, 30);
        this.panel.add(confirmBtn);
        this.panel.add(resetBtn);
        resetBtn.setBounds(230, 300, 80, 30);
        this.panel.add(cancelBtn);
        cancelBtn.setBounds(380, 300, 80, 30);
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

    public JLabel getExaminationIDValue() {
        return examinationIDValue;
    }

    public void setExaminationIDValue(JLabel examinationIDValue) {
        this.examinationIDValue = examinationIDValue;
    }

    public JTextField getAddressText() {
        return addressText;
    }

    public void setAddressText(JTextField addressText) {
        this.addressText = addressText;
    }

    public JTextField getExaminationNameText() {
        return examinationNameText;
    }

    public void setExaminationNameText(JTextField examinationNameText) {
        this.examinationNameText = examinationNameText;
    }

    /**
     * 添加考场
     */
    public abstract void confirm();

    /**
     * 重置
     */
    public abstract void reset();
    /**
     * 取消
     */
    public abstract void cancel();

    /**
     * 修改考场
     */
    public abstract void alter();

    /*public static void main(String[] args) {
        new UpdateExaminationController().init(UPDATETYPE.INSERT);
    }*/
}
