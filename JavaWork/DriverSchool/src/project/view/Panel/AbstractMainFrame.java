package project.view.Panel;

import com.sun.org.apache.xpath.internal.operations.Or;
import org.apache.commons.codec.digest.DigestUtils;
import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.controller.*;
import project.dao.*;
import project.domin.Exam;
import project.domin.Examination;
import project.domin.Order;
import project.domin.User;
import project.tools.ImagePanel;
import project.tools.PanelManager;
import project.tools.TextReader;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.io.IOException;
import java.util.List;

public abstract class AbstractMainFrame extends JFrame {
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
    private static User user=new UserDaoImpl().getUserByID(10001);
    public static boolean isAdmin=false;
    final double screenX = Toolkit.getDefaultToolkit().getScreenSize().getWidth();
    final double screenY = Toolkit.getDefaultToolkit().getScreenSize().getHeight();

    private JPanel backgroundPanel = new JPanel(null);
    private JPanel topPanel = new JPanel(null);
    private JPanel centerPanel = new JPanel(null);
    private JPanel topLeftPanel, topRightPanel;

    private JLabel menuHomeLabel = new JLabel("首  页");
    private JLabel menuOrderLabel = new JLabel("考试预约");
    private JLabel menuGradeLabel = new JLabel("成绩查询");
    private JLabel menuTestLabel = new JLabel("考试模拟");
    //管理员界面的标签
    private JLabel menuExamLabel=new JLabel("考试管理");
    private JLabel menuExaminationLabel=new JLabel("考场管理");
    private JLabel menuAdmGradeLabel=new JLabel("考试打分");
    private JLabel[] adminLabels={menuHomeLabel,menuExamLabel,menuExaminationLabel,menuAdmGradeLabel};
    private JLabel[] menuLabels = {menuHomeLabel, menuOrderLabel, menuGradeLabel, menuTestLabel};
    private JLabel welcomeLabel = new JLabel("欢迎您，");
    private JLabel nameLabel = new JLabel(user.getName());
    private JLabel centerTitleLabel = new JLabel("驾校考试管理系统");

    private String subject1Str;
    private String subject2Str;
    private String subject3Str;
    private String subject4Str;
    /**
     * 初始化整个窗口的方法
     */
    public void init() {
        if(user.getUser_id()==10001){
            isAdmin=true;
        }
        subject1Str=TextReader.readTxtFile("src\\Text\\Subject1.txt");
        subject2Str=TextReader.readTxtFile("src\\Text\\Subject2.txt");
        subject3Str=TextReader.readTxtFile("src\\Text\\Subject3.txt");
        subject4Str=TextReader.readTxtFile("src\\Text\\Subject4.txt");
        this.setSize(new Dimension((int) (screenX * 0.7), (int) (screenY * 0.7)));
        this.setLocation(300, 100);
        this.setTitle("驾校考试管理系统");
        this.setResizable(false);
        try {
            this.setIconImage(ImageIO.read(new File("src/image/logo.jpg")));
        } catch (IOException e) {
            e.printStackTrace();
        }
        addContent();
        this.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        this.setVisible(true);
        PanelManager.getPanelManager().setAbstractMainFrame(this);
    }

    /**
     * 给窗口添加组件的方法
     */
    public void addContent() {
        this.add(this.backgroundPanel);
        backgroundPanel.setLayout(new BorderLayout());
        initCenterPanel();
        if(isAdmin){
            initAdminTopPanel();
        }else{
            initTopPanel();
        }
    }

    /**
     * 初始化顶部菜单栏的方法
     */
    public void initTopPanel() {
        backgroundPanel.add(topPanel, BorderLayout.NORTH);
        topPanel.setPreferredSize(new Dimension(backgroundPanel.getWidth(), 35));
        topPanel.setLayout(new BorderLayout());
        topLeftPanel = new JPanel();
        topLeftPanel.setPreferredSize(new Dimension(580, 50));
        topRightPanel = new JPanel();
        topRightPanel.setPreferredSize(new Dimension(300, 50));
        topPanel.add(topLeftPanel, BorderLayout.WEST);
        topPanel.add(topRightPanel, BorderLayout.EAST);
        createMenuLabel(menuHomeLabel, "house.png", "home", topLeftPanel);
        createMenuLabel(menuOrderLabel, "order.png", "order", topLeftPanel);
        createMenuLabel(menuGradeLabel, "grade.png", "grade", topLeftPanel);
        createMenuLabel(menuTestLabel, "car.png", "test", topLeftPanel);
        createMenuLabel(welcomeLabel, "student.png", "welcome", topRightPanel);
        createMenuLabel(nameLabel, "", "name", topRightPanel);
        nameLabel.setForeground(new Color(100, 149, 237));
        nameLabel.setText(user.getName());
    }

    /**
     * 初始化管理员顶部菜单栏
     */
    public void initAdminTopPanel(){
        backgroundPanel.add(topPanel, BorderLayout.NORTH);
        topPanel.setPreferredSize(new Dimension(backgroundPanel.getWidth(), 35));
        topPanel.setLayout(new BorderLayout());
        topLeftPanel = new JPanel();
        topLeftPanel.setPreferredSize(new Dimension(580, 50));
        topRightPanel = new JPanel();
        topRightPanel.setPreferredSize(new Dimension(300, 50));
        topPanel.add(topLeftPanel, BorderLayout.WEST);
        topPanel.add(topRightPanel, BorderLayout.EAST);
        topPanel.add(topRightPanel, BorderLayout.EAST);
        createMenuLabel(menuHomeLabel, "house.png", "home", topLeftPanel);
        createMenuLabel(menuExamLabel, "考试.png", "order", topLeftPanel);
        createMenuLabel(menuExaminationLabel, "场地.png", "examination", topLeftPanel);
        createMenuLabel(menuAdmGradeLabel, "打分.png", "admGrade", topLeftPanel);
        createMenuLabel(welcomeLabel, "管理员.png", "welcome", topRightPanel);
        createMenuLabel(nameLabel, "", "name", topRightPanel);
        nameLabel.setForeground(new Color(100, 149, 237));
        nameLabel.setText(user.getName());
    }
    /**
     * 初始化中间显示面板的方法
     */
    public void initCenterPanel() {
        backgroundPanel.add(centerPanel, BorderLayout.CENTER);
        centerPanel.setPreferredSize(new Dimension(backgroundPanel.getWidth(), backgroundPanel.getHeight() - 35));
        centerPanel.setLayout(new BorderLayout());
        initHomePanel();
    }

    /**
     * 添加菜单栏的组件的方法
     *
     * @param label    文本
     * @param iconName 图片路径
     * @param name     组件名字
     * @param panel    添加到的面板
     */
    public void createMenuLabel(JLabel label, String iconName, String name, JPanel panel) {
        Font font = new Font("宋体", Font.BOLD, 18);
        JLabel line = new JLabel("<html>&nbsp;<font color='#515151'>|</font>&nbsp;</html>");
        String iconSrc = "src/image/" + iconName;
        ImageIcon icon = new ImageIcon(iconSrc);
        icon.setImage(icon.getImage().getScaledInstance(20, 20, Image.SCALE_DEFAULT));
        label.setIcon(icon);
        label.setFont(font);
        label.setPreferredSize(new Dimension(100, 35));
        panel.add(label);
        if (!(name.equals("test") || name.equals("welcome") ||name.equals("admGrade")|| iconName.equals(""))) {
            panel.add(line);
        }
        switch (name) {
            case "home":
                label.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        initHomePanel();
                    }
                });
                break;
            case "order":
                label.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        initOrderPanel();
                    }
                });
                break;
            case "grade":
                label.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        initGradePanel();
                    }
                });
                break;
            case "examination":
                label.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        initExaminationPanel();
                    }
                });
                break;
            case "admGrade":
                label.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        initAdminGradePanel();
                    }
                });
                break;
            case "test":
                label.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        initTestPanel();
                    }
                });
                break;
            case "name":
                if(!isAdmin){
                    label.addMouseListener(new MouseAdapter() {
                        @Override
                        public void mouseClicked(MouseEvent e) {
                            String psw=JOptionPane.showInputDialog("请输入密码：");
                            if(psw==null){
                                return;
                            }
                            String md5Pwd= DigestUtils.md5Hex(psw).toUpperCase();
                            if(md5Pwd.equals(user.getPassword())){
                                new RegisterController().init(UPDATETYPE.ALTER);
                            }else{
                                JOptionPane.showMessageDialog(null,"密码错误！");
                            }
                        }
                    });
                }
                break;
        }
    }

    /**
     * 初始化首页界面
     */
    public void initHomePanel() {
        //清空面板
        centerPanel.removeAll();
        centerPanel.repaint();
        if(!isAdmin){
            setMenuFontColor(menuLabels, 0);
        }else{
            setMenuFontColor(adminLabels,0);
        }
        Image image = null;
        try {
            image = ImageIO.read(new File("src/image/background.jpg"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        ImagePanel imagePanel = new ImagePanel(image);
        imagePanel.setPreferredSize(new Dimension(backgroundPanel.getWidth(), backgroundPanel.getHeight() - 35));
        imagePanel.setLayout(null);
        centerPanel.add(imagePanel, BorderLayout.CENTER);
        Font font = new Font("黑体", Font.PLAIN, 80);
        String iconSrc = "src/image/logo.jpg";
        ImageIcon icon = new ImageIcon(iconSrc);
        icon.setImage(icon.getImage().getScaledInstance(80, 80, Image.SCALE_DEFAULT));
        centerTitleLabel.setFont(font);
        centerTitleLabel.setIcon(icon);
        centerTitleLabel.setForeground(Color.orange);
        centerTitleLabel.setBounds(300, 100, 800, 100);
        imagePanel.add(centerTitleLabel);
        //重绘面板
        centerPanel.revalidate();
    }

    /**
     * 初始化考试预约界面
     */
    public void initOrderPanel() {
        //清空面板
        centerPanel.removeAll();
        centerPanel.repaint();
        if(!isAdmin){
            setMenuFontColor(menuLabels, 1);
        }else{
            setMenuFontColor(adminLabels,1);
        }
        //可预约考试选项卡
        JTabbedPane orderTabPanel = new JTabbedPane(JTabbedPane.TOP, JTabbedPane.WRAP_TAB_LAYOUT);
        ExamDao examDao=new ExamDaoImpl();
        List<Exam> exams=examDao.getNowExam();
        orderTabPanel.add("可预约考试", new ExamDataTableController(exams,user.getUser_id(),"order").backgroundPanel);
        OrderDao orderDao=new OrderDaoImpl();
        if(!isAdmin){
            List<Order> orders=orderDao.selectOrderByUid(user.getUser_id());
            orderTabPanel.add("我的预约",new UserOrderTableController(orders,user.getUser_id(),"order").backgroundPanel);
        }
        //设置选项卡切换时刷新页面
        orderTabPanel.addChangeListener(e -> {
            if(e.getSource() instanceof JTabbedPane){
                JTabbedPane pane=(JTabbedPane) e.getSource();
                List<Order> newOrders=orderDao.selectOrderByUid(user.getUser_id());
                List<Exam> newExams=examDao.getNowExam();
                if(pane.getSelectedIndex()==0){
                    pane.setComponentAt(pane.getSelectedIndex(),new ExamDataTableController(newExams,user.getUser_id(),"order").backgroundPanel);
                }else if(pane.getSelectedIndex()==1){
                    pane.setComponentAt(pane.getSelectedIndex(),new UserOrderTableController(newOrders,user.getUser_id(),"order").backgroundPanel);
                }
            }
        });
        centerPanel.add(orderTabPanel, BorderLayout.CENTER);
        //重绘面板
        centerPanel.revalidate();
    }

    /**
     * 初始化成绩查询界面
     */
    public void initGradePanel(){
        //清空面板
        centerPanel.removeAll();
        centerPanel.repaint();
        setMenuFontColor(menuLabels, 2);
        JTabbedPane orderTabPanel = new JTabbedPane(JTabbedPane.TOP, JTabbedPane.WRAP_TAB_LAYOUT);
        OrderDao orderDao=new OrderDaoImpl();
        List<Order> orders=orderDao.selectGradeByUid(user.getUser_id());
        orderTabPanel.add("我的成绩",new UserOrderTableController(orders,user.getUser_id(),"grade").backgroundPanel);
        centerPanel.add(orderTabPanel,BorderLayout.CENTER);
        //重绘面板
        centerPanel.revalidate();
    }

    /**
     * 初始化考场管理界面
     */
    public void initExaminationPanel(){
        //清空面板
        centerPanel.removeAll();
        centerPanel.repaint();
        setMenuFontColor(adminLabels,2);
        JTabbedPane examinationTabPanel = new JTabbedPane(JTabbedPane.TOP, JTabbedPane.WRAP_TAB_LAYOUT);
        ExaminationDao examinationDao=new ExaminationDaoImpl();
        List<Examination> examinations=examinationDao.selectAllExamination();
        examinationTabPanel.add("考场信息",new ExaminationTableController(examinations).backgroundPanel);
        centerPanel.add(examinationTabPanel,BorderLayout.CENTER);
        //重绘面板
        centerPanel.revalidate();
    }

    /**
     * 初始化管理员打分界面
     */
    public void initAdminGradePanel(){
        //清空面板
        centerPanel.removeAll();
        centerPanel.repaint();
        setMenuFontColor(adminLabels,3);
        JTabbedPane examinationTabPanel = new JTabbedPane(JTabbedPane.TOP, JTabbedPane.WRAP_TAB_LAYOUT);
        OrderDao orderDao=new OrderDaoImpl();
        List<Order> orders=orderDao.selectUntreatedOrder();
        //创建选项卡
        examinationTabPanel.add("成绩打分",new UserOrderTableController(orders,user.getUser_id(),"admGrade").backgroundPanel);
        centerPanel.add(examinationTabPanel,BorderLayout.CENTER);
        //重绘面板
        centerPanel.revalidate();
    }

    /**
     *
     */
    public void initTestPanel(){
        centerPanel.removeAll();
        centerPanel.repaint();
        setMenuFontColor(menuLabels,3);
        JTabbedPane practiceTabPanel = new JTabbedPane(JTabbedPane.LEFT, JTabbedPane.WRAP_TAB_LAYOUT);
        //TODO 添加选项卡
        practiceTabPanel.add("科目一",new PracticeTestPanel(subject1Str).backgroundPanel);
        practiceTabPanel.add("科目二",new PracticeTestPanel(subject2Str).backgroundPanel);
        practiceTabPanel.add("科目三",new PracticeTestPanel(subject3Str).backgroundPanel);
        practiceTabPanel.add("科目四",new PracticeTestPanel(subject4Str).backgroundPanel);
        centerPanel.add(practiceTabPanel,BorderLayout.CENTER);
        //重绘面板
        centerPanel.revalidate();
    }
    /**
     * 设置菜单栏字体颜色
     * @param menuLabels
     * @param index
     */
    public void setMenuFontColor(JLabel[] menuLabels, int index) {
        menuLabels[index].setForeground(new Color(0, 197, 205));
        for (int i = 0; i < menuLabels.length; ++i) {
            if (i != index) {
                menuLabels[i].setForeground(Color.black);
            }
        }
    }

    public static void setUser(User user) {
        AbstractMainFrame.user = user;
    }

    public static User getUser() {
        return user;
    }

/*    public static void main(String[] args) {
        new MainViewController().init();
    }*/
}
