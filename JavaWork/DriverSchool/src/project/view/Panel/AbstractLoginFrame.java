package project.view.Panel;

import org.apache.commons.codec.digest.DigestUtils;
import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.controller.LoginController;
import project.controller.RegisterController;
import project.dao.UserDao;
import project.dao.UserDaoImpl;
import project.domin.User;
import project.tools.CaptchaUtils;
import project.tools.ImagePanel;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.io.IOException;
import java.util.regex.Pattern;

public abstract class AbstractLoginFrame extends JFrame {
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

    //声明所使用的组件
    private JPanel panel = new JPanel(null);
    //标签
    private JLabel Title=new JLabel("驾校考试管理系统");
    private JLabel userLabel = new JLabel("用户名：");
    private JLabel pwdLabel = new JLabel("密 码：");
    private JLabel captLable=new JLabel("验证码：");
    private JLabel pngLable=new JLabel(new ImageIcon(CaptchaUtils.getCaptchaImage()));
    private JLabel registerLable=new JLabel("注  册");
    //输入框
    private JTextField userText = new JTextField(30);
    private JPasswordField pwdText = new JPasswordField(30);
    private JTextField captText=new JTextField(20);
    //按钮
    private JButton loginBtn = new JButton("登录");
    private JButton cancelBtn = new JButton("取消");
    private JButton resetBtn = new JButton("重置");

    //初始化窗口
    public void init() {
        this.setSize(800, 600);
        this.setBounds(500, 100, 800, 600);
        this.setTitle("驾校考试管理系统");
        this.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        this.setResizable(false);
        try{
            this.setIconImage(ImageIO.read(new File("src/image/logo.jpg")));
        } catch (IOException e) {
            e.printStackTrace();
        }
        addContent();
        this.setVisible(true);
    }

    //添加面板属性
    public void addContent() {
        this.add(this.panel);
        Font font = new Font("黑体", Font.BOLD, 50);
        String iconSrc = "src/image/logo.jpg";
        ImageIcon icon = new ImageIcon(iconSrc);
        icon.setImage(icon.getImage().getScaledInstance(50, 50, Image.SCALE_DEFAULT));
        Title.setFont(font);
        Title.setIcon(icon);
        Title.setBounds(150,100,500,50);
        this.panel.add(Title);
        this.panel.add(userLabel);
        this.userLabel.setBounds(250, 200, 60, 30);
        this.panel.add(userText);
        this.userText.setBounds(300, 200, 200, 30);
        this.panel.add(pwdLabel);
        this.pwdLabel.setBounds(250, 275, 60, 30);
        this.panel.add(registerLable);
        registerLable.setBounds(560,350,60,30);
        Font f=new Font("宋体",Font.PLAIN,16);
        registerLable.setFont(f);
        registerLable.setForeground(Color.blue);
        this.panel.add(pwdText);
        this.pwdText.setBounds(300, 275, 200, 30);
        this.panel.add(loginBtn);
        this.loginBtn.setBounds(200, 425, 90, 30);
        this.panel.add(cancelBtn);
        this.cancelBtn.setBounds(350, 425, 90, 30);
        this.panel.add(resetBtn);
        this.resetBtn.setBounds(500, 425, 90, 30);
        this.panel.add(captLable);
        this.captLable.setBounds(250,350,60,30);
        this.panel.add(captText);
        this.captText.setBounds(300,350,100,30);
        this.panel.add(pngLable);
        this.pngLable.setBounds(400,350,150,30);
        ImagePanel imagePanel=new ImagePanel(new ImageIcon("src/image/whiteGround.jpg").getImage());
        imagePanel.setBounds(0,0,800,600);
        panel.add(imagePanel);
        //注册按钮的方法
        addBtnListener();
    }

    //注册按钮监听事件
    public void addBtnListener() {
        this.loginBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                login();
            }
        });
        this.resetBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                userText.setText("");
                pwdText.setText("");
                captText.setText("");
            }
        });
        this.cancelBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                setVisible(false);
                System.exit(0);
            }
        });
        this.pngLable.addMouseListener((new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                Icon icon= null;
                try {
                    icon = new ImageIcon(ImageIO.read(new File(CaptchaUtils.getCaptchaImage())));
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
                pngLable.setIcon(icon);
            }
        }));
        this.registerLable.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                toRegisterPage();
            }
        });
    }

    public JTextField getUserText() {
        return userText;
    }

    public JPasswordField getPwdText() {
        return pwdText;
    }

    public JTextField getCaptText() {
        return captText;
    }
    public abstract void login();
    public void toRegisterPage(){
        this.setVisible(false);
        new RegisterController().init(UPDATETYPE.INSERT);
    };
}
