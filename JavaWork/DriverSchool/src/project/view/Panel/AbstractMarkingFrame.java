package project.view.Panel;

import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.dao.OrderDao;
import project.dao.OrderDaoImpl;
import project.tools.ImagePanel;
import project.tools.PanelManager;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.io.IOException;

public  class AbstractMarkingFrame extends JFrame {
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
    private JPanel backgroundPanel = new JPanel(null);
    private JLabel nameLabel=new JLabel("姓名：");
    private JLabel nameValue=new JLabel("");
    private JLabel levelLabel=new JLabel("科目：");
    private JLabel levelValue=new JLabel("0");
    private JLabel gradeLabel=new JLabel("分数：");
    private String[] gradeLevel={"100","95","90","85","80","未通过"};
    private JComboBox gradeBox=new JComboBox(gradeLevel);
    private JButton ensureBtn=new JButton("确认");

    private int selectRow;
    private UserOrderTablePanel userOrderTablePanel=PanelManager.getPanelManager().getUserOrderTablePanel();


    OrderDao orderDao=new OrderDaoImpl();

    public void init() {
        selectRow=userOrderTablePanel.getTable().getSelectedRow();
        this.setSize(400,300);
        this.setLocation(750, 300);
        this.setTitle("驾校考试管理系统");
        this.setResizable(false);
        try {
            this.setIconImage(ImageIO.read(new File("src/image/logo.jpg")));
        } catch (IOException e) {
            e.printStackTrace();
        }
        addContent();
        this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
        this.setVisible(true);
    }
    public void addContent(){
        this.add(this.backgroundPanel);
        nameLabel.setBounds(50,50,50,20);
        nameValue.setBounds(100,50,100,20);
        nameValue.setText(userOrderTablePanel.getTable().getValueAt(selectRow,0).toString());
        backgroundPanel.add(nameLabel);
        backgroundPanel.add(nameValue);
        levelLabel.setBounds(200,50,50,20);
        levelValue.setBounds(250,50,50,20);
        levelValue.setText(userOrderTablePanel.getTable().getValueAt(selectRow,2).toString());
        backgroundPanel.add(levelLabel);
        backgroundPanel.add(levelValue);
        gradeLabel.setBounds(50,100,50,20);
        gradeBox.setBounds(100,100,160,20);
        backgroundPanel.add(gradeLabel);
        backgroundPanel.add(gradeBox);
        ensureBtn.setBounds(125,150,100,30);
        ensureBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                marking();
            }
        });
        backgroundPanel.add(ensureBtn);
        ImagePanel imagePanel=new ImagePanel(new ImageIcon("src/image/whiteGround.jpg").getImage());
        imagePanel.setBounds(0,0,400,300);
        backgroundPanel.add(imagePanel);
    }

    /**
     * 打分
     */
    public void marking(){
        int x=gradeBox.getSelectedIndex();
        Integer grade;
        if(x==5) grade=59;
        else grade=Integer.valueOf(gradeLevel[x]);
        int i=orderDao.adminMarking(userOrderTablePanel.getOrderUser().getUser_id(),
                userOrderTablePanel.getOrderUser().getExam_id(),
                grade);
        if(i!=0){
            this.setVisible(false);
            JOptionPane.showMessageDialog(null,"打分成功！");
            userOrderTablePanel.refreshMarking();
            this.userOrderTablePanel=null;
            this.selectRow=-1;
        }else{
            JOptionPane.showMessageDialog(null,"打分未成功！");
        }
    }
}
