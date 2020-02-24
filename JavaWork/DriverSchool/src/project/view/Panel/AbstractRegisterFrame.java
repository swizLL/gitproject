package project.view.Panel;

import com.sun.org.apache.bcel.internal.generic.NEW;
import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.controller.LoginController;
import project.domin.User;
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

public abstract class AbstractRegisterFrame extends JFrame {
    static {
        try
        {
            //设置本属性将改变窗口边框样式定义
            BeautyEyeLNFHelper.frameBorderStyle = BeautyEyeLNFHelper.FrameBorderStyle.translucencyAppleLike;
            org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper.launchBeautyEyeLNF();
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }
    private JPanel panel = new JPanel(null);
    //组件标签部分
    private JLabel Title=new JLabel("用户注册");
    private JLabel uidLable=new JLabel("用户名：");
    private JLabel idTagLable=new JLabel("(只允许输入数字,且少于20位！)");
    private JLabel pswLable=new JLabel("密  码：");
    private JLabel ensurePswLable=new JLabel("确认密码：");
    private JLabel nameLable=new JLabel("姓  名：");
    private JLabel ageLable=new JLabel("年  龄：");
    private JLabel sexLable=new JLabel("性  别：");
    private JLabel idCardLable=new JLabel("身份证号：");
    private JLabel phoneLable=new JLabel("电  话：");
    private JLabel examLable=new JLabel("考试等级：");

    //组件部分
    private JTextField uidText=new JTextField(30);
    private JPasswordField pswText=new JPasswordField(30);
    private JPasswordField ensurePswText=new JPasswordField(30);
    private JTextField nameText=new JTextField(30);
    private JTextField ageText=new JTextField(30);
    //单选按钮
    private JRadioButton man=new JRadioButton("男");
    private JRadioButton women=new JRadioButton("女");
    ButtonGroup bg=new ButtonGroup();
    private JTextField phoneText=new JTextField(30);
    private JTextField idCardText=new JTextField(30);
    Integer level[]={0,1,2,3,4};
    //下拉列表
    private JComboBox levelBox=new JComboBox(level);

    //修改界面的组件
    private JLabel uidValue=new JLabel("");
    private JLabel nameValue=new JLabel("");
    private JLabel idCardValue=new JLabel("");
    //按钮
    private JButton registerBtn = new JButton("注册");
    private JButton cancelBtn = new JButton("取消");
    private JButton resetBtn = new JButton("重置");

    private UPDATETYPE type;
    private User user;
    //初始化窗口
    public void init(UPDATETYPE type){
        this.type=type;
        this.setSize(800, 700);
        this.setBounds(500, 100, 800, 700);
        this.setTitle("驾校考试管理系统");
        this.setResizable(false);
        try{
            this.setIconImage(ImageIO.read(new File("src/image/logo.jpg")));
        } catch (IOException e) {
            e.printStackTrace();
        }
        this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
        this.addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(WindowEvent e) {
                if(type==UPDATETYPE.INSERT){
                    cancel();
                }else{
                    alterCancel();
                }
            }
        });
        addContent();
        this.setVisible(true);
    }

    public void addContent(){
        this.add(this.panel);
        Font font = new Font("黑体", Font.BOLD, 35);
        Title.setFont(font);
        Title.setBounds(325,30,400,50);
        this.panel.add(Title);
        if(type==UPDATETYPE.INSERT){
            this.panel.add(uidText);
            uidText.setBounds(320,100,200,30);
            this.panel.add(idTagLable);
            idTagLable.setBounds(320, 125, 230, 30);
            this.panel.add(nameText);
            nameText.setBounds(320,250,200,30);
            this.panel.add(idCardText);
            idCardText.setBounds(320,500,200,30);
            man.setSelected(true);
        }else{
            Title.setText("用户信息");
            user=PanelManager.getPanelManager().getAbstractMainFrame().getUser();
            this.panel.add(uidValue);
            uidValue.setBounds(320,100,200,30);
            this.panel.add(nameValue);
            nameValue.setBounds(320,250,200,30);
            this.panel.add(idCardValue);
            idCardValue.setBounds(320,500,200,30);
            registerBtn.setText("修改");
            JLabel tagLabel1=new JLabel("(如果不修改密码，则此处不填写即可！)");
            tagLabel1.setBounds(320,175,300,30);
            JLabel tagLabel2=new JLabel("(如果不修改密码，则此处不填写即可！)");
            tagLabel2.setBounds(320,225,300,30);
            this.panel.add(tagLabel1);
            this.panel.add(tagLabel2);
            alterReset();
        }
        this.panel.add(uidLable);
        uidLable.setBounds(250,100,70,30);
        this.panel.add(pswLable);
        pswLable.setBounds(250,150,70,30);
        this.panel.add(pswText);
        pswText.setBounds(320,150,200,30);
        this.panel.add(ensurePswLable);
        ensurePswLable.setBounds(250,200,75,30);
        this.panel.add(ensurePswText);
        ensurePswText.setBounds(320,200,200,30);
        this.panel.add(nameLable);
        nameLable.setBounds(250,250,70,30);
        this.panel.add(ageLable);
        ageLable.setBounds(250,300,70,30);
        this.panel.add(ageText);
        ageText.setBounds(320,300,200,30);
        this.panel.add(sexLable);
        sexLable.setBounds(250,350,70,30);
        this.panel.add(man);
        man.setBounds(320,350,70,30);
        this.panel.add(women);
        bg.add(man);
        bg.add(women);
        women.setBounds(390,350,70,30);
        this.panel.add(phoneLable);
        phoneLable.setBounds(250,400,70,30);
        this.panel.add(phoneText);
        phoneText.setBounds(320,400,200,30);
        this.panel.add(examLable);
        examLable.setBounds(250,450,75,30);
        this.panel.add(levelBox);
        levelBox.setBounds(320,450,70,30);
        this.panel.add(idCardLable);
        idCardLable.setBounds(250,500,75,30);
        this.panel.add(registerBtn);
        registerBtn.setBounds(150,570,80,30);
        this.panel.add(resetBtn);
        resetBtn.setBounds(350,570,80,30);
        this.panel.add(cancelBtn);
        cancelBtn.setBounds(550,570,80,30);
        ImagePanel imagePanel=new ImagePanel(new ImageIcon("src/image/whiteGround.jpg").getImage());
        imagePanel.setBounds(0,0,800,700);
        panel.add(imagePanel);
        addBtnListener();
    }
    //按钮事件注册
    public void addBtnListener(){
        this.registerBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if(type==UPDATETYPE.INSERT){
                    register();
                }else{
                    alterUser();
                }
            }

        });
        this.resetBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if(type==UPDATETYPE.INSERT){
                    reset();
                }else{
                    alterReset();
                }
            }
        });
        this.cancelBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if(type==UPDATETYPE.INSERT){
                    cancel();
                }else{
                    alterCancel();
                }
            }
        });
    }
    public JTextField getUidText() {
        return uidText;
    }

    public JPasswordField getPswText() {
        return pswText;
    }

    public JPasswordField getEnsurePswText() {
        return ensurePswText;
    }

    public JTextField getNameText() {
        return nameText;
    }

    public JTextField getAgeText() {
        return ageText;
    }

    public JRadioButton getMan() {
        return man;
    }

    public JRadioButton getWomen() {
        return women;
    }

    public JTextField getPhoneText() {
        return phoneText;
    }

    public JComboBox getLevelBox() {
        return levelBox;
    }

    public JButton getRegisterBtn() {
        return registerBtn;
    }

    public JButton getCancelBtn() {
        return cancelBtn;
    }

    public JButton getResetBtn() {
        return resetBtn;
    }

    public JTextField getIdCardText() {
        return idCardText;
    }

    public JLabel getUidValue() {
        return uidValue;
    }

    public JLabel getNameValue() {
        return nameValue;
    }

    public JLabel getIdCardValue() {
        return idCardValue;
    }

    public User getUser() {
        return user;
    }

    //抽象方法
    public abstract void register();
    public abstract void reset();
    public abstract void cancel();
    public abstract void alterCancel();
    public abstract void alterUser();
    public abstract void alterReset();
}