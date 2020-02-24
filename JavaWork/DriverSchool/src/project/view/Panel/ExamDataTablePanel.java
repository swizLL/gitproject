package project.view.Panel;

import project.controller.UpdateExamController;
import project.dao.ExaminationDao;
import project.dao.ExaminationDaoImpl;
import project.domin.Exam;
import project.domin.Examination;
import project.domin.Order;
import project.tools.PanelManager;
import project.view.TableModel.ExamDataTableModel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.List;

public abstract class ExamDataTablePanel {
    protected JPanel backgroundPanel;
    private JTable table;
    private JPanel topPanel;
    private JScrollPane tablePanel;
    private String panelName;
    //选择的预约记录
    private Order orderUser;
    //选择的考试记录
    public Integer selectExam;

    private Integer user_id;
    private Font buttonFont=new Font("宋体",Font.BOLD,10);
    public ExamDataTablePanel(List<Exam> examList, Integer user_id, String panelName) {
        this.user_id=user_id;
        this.panelName = panelName;
        backgroundPanel = new JPanel(new BorderLayout());
        tablePanel = new JScrollPane();
        table = new JTable(new ExamDataTableModel(examList));
        addTableClick();
        tablePanel.setViewportView(table);
        topPanel = new JPanel();
        topPanel.setLayout(null);
        topPanel.setPreferredSize(new Dimension(1500, 45));
        backgroundPanel.add(topPanel, BorderLayout.NORTH);
        backgroundPanel.add(tablePanel, BorderLayout.CENTER);
        //判断应该怎么初始化顶部按钮
        if (AbstractMainFrame.isAdmin) {
            initAdminTopPanel();
            createSearchInput();
        } else {
            switch (this.panelName) {
                case "order":
                    initUserOrderTopPanel();
                    createSearchInput();
                    break;
            }
        }
        PanelManager.getPanelManager().setExamDataTablePanel(this);
    }

    /**
     * 给表格添加点击事件
     */
    public void addTableClick() {
        table.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                int x = table.getSelectedRow();
                orderUser = getOrderByTable(user_id, (Integer) table.getValueAt(x, 0), (Integer) table.getValueAt(x, 1),
                        (String) table.getValueAt(x, 3), 0);
                selectExam=(Integer)table.getValueAt(x,0);
                System.out.println("考试号："+selectExam);
                System.out.println(orderUser);
            }
        });
    }

    /**
     * 普通用户的预约界面按钮
     */
    public void initUserOrderTopPanel() {
        JButton orderBtn = creatButton("注册.png");
        orderBtn.setLocation(0, 0);
        JLabel label1 = new JLabel("预 约");
        label1.setFont(buttonFont);
        label1.setBounds(0, 30, 30, 15);
        orderBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                userOrder(orderUser);
            }
        });
        topPanel.add(label1);
        topPanel.add(orderBtn);
        JButton refreshBtn = creatButton("刷新.png");
        refreshBtn.setLocation(50, 0);
        refreshBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                refresh();
            }
        });
        JLabel label2 = new JLabel("刷 新");
        label2.setFont(buttonFont);
        label2.setBounds(50, 30, 30, 15);
        topPanel.add(label2);
        topPanel.add(refreshBtn);
    }

    /**
     * 管理员考试界面的按钮
     */
    public void initAdminTopPanel() {
        JButton insertBtn = creatButton("新建.png");
        insertBtn.setLocation(0, 0);
        insertBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                new UpdateExamController().init(UPDATETYPE.INSERT);
            }
        });
        JLabel label1 = new JLabel("新 建");
        label1.setFont(buttonFont);
        label1.setBounds(0, 30, 30, 15);
        topPanel.add(label1);
        topPanel.add(insertBtn);
        JButton deleteBtn = creatButton("删除.png");
        deleteBtn.setLocation(50, 0);
        deleteBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                deleteExam(selectExam);
            }
        });
        JLabel label2 = new JLabel("删 除");
        label2.setFont(buttonFont);
        label2.setBounds(50, 30, 30, 15);
        topPanel.add(label2);
        topPanel.add(deleteBtn);
        JButton updateBtn = creatButton("修改.png");
        updateBtn.setLocation(100, 0);
        updateBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if(selectExam==null){
                    JOptionPane.showMessageDialog(null,"你还没有选择要修改的考试！");
                }else{
                    new UpdateExamController().init(UPDATETYPE.ALTER);
                }
            }
        });
        JLabel label4 = new JLabel("修 改");
        label4.setFont(buttonFont);
        label4.setBounds(100, 30, 30, 15);
        topPanel.add(label4);
        topPanel.add(updateBtn);
        JButton refreshBtn = creatButton("刷新.png");
        refreshBtn.setLocation(150, 0);
        refreshBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                refresh();
            }
        });
        JLabel label5 = new JLabel("刷 新");
        label5.setFont(buttonFont);
        label5.setBounds(150, 30, 30, 15);
        topPanel.add(label5);
        topPanel.add(refreshBtn);
    }

    /**
     * 创建顶部的搜索栏
     */
    public void createSearchInput() {
        JLabel nationLabel = new JLabel("考场：");
        nationLabel.setBounds(800, 20, 50, 20);
        ExaminationDao examinationDao=new ExaminationDaoImpl();
        List<Examination> examinations=examinationDao.selectAllExamination();
        String[] examinationsName=new String[examinations.size()+1];
        examinationsName[0]="";
        for(int i=1;i<=examinations.size();++i){
            examinationsName[i]=examinations.get(i-1).getExamination_name();
        }
        JComboBox nationBox=new JComboBox(examinationsName);
        nationBox.setBounds(835, 20, 140, 23);
        //JTextField nationText = new JTextField(30);
        topPanel.add(nationLabel);
        topPanel.add(nationBox);
        JLabel yearLabel = new JLabel("年：");
        String[] oldYear = {"1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010"
                , "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019"};
        String[] nowYear = {"0", "2020", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030"};
        JComboBox yearBox = null;
        String[] finalYear;
        if (this.panelName.equals("order")) {
            yearBox = new JComboBox(nowYear);
            finalYear = nowYear;
        } else {
            yearBox = new JComboBox(oldYear);
            finalYear = oldYear;
        }
        yearLabel.setBounds(985, 20, 50, 20);
        yearBox.setBounds(1005, 20, 80, 20);
        topPanel.add(yearLabel);
        topPanel.add(yearBox);
        JLabel monthLabel = new JLabel("月：");
        String[] month = {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"};
        JComboBox monthBox = new JComboBox(month);
        monthLabel.setBounds(1085, 20, 50, 20);
        monthBox.setBounds(1105, 20, 60, 20);
        topPanel.add(monthLabel);
        topPanel.add(monthBox);
        JLabel dayLabel = new JLabel("日：");
        String[] day = {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"
                , "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};
        JComboBox dayBox = new JComboBox(day);
        dayLabel.setBounds(1165, 20, 50, 20);
        dayBox.setBounds(1185, 20, 60, 20);
        topPanel.add(dayLabel);
        topPanel.add(dayBox);
        JButton searchButton = new JButton();
        searchButton.setBounds(1250, 10, 30, 30);
        searchButton.setBackground(Color.white);
        ImageIcon icon = new ImageIcon("src/image/搜索.png");
        icon.setImage(icon.getImage().getScaledInstance(30, 30, Image.SCALE_DEFAULT));
        searchButton.setIcon(icon);
        JComboBox finalYearBox = yearBox;
        //注册搜索按钮该调用的方法
        searchButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                searchByDateAndNation(examinationsName[nationBox.getSelectedIndex()], finalYear[finalYearBox.getSelectedIndex()], month[monthBox.getSelectedIndex()], day[dayBox.getSelectedIndex()]);
            }
        });
        topPanel.add(searchButton);
    }

    public JButton creatButton(String iconName) {
        JButton btn = new JButton();
        btn.setSize(30, 30);
        btn.setBackground(Color.white);
        ImageIcon icon = new ImageIcon("src/image/" + iconName);
        icon.setImage(icon.getImage().getScaledInstance(30, 30, Image.SCALE_DEFAULT));
        btn.setIcon(icon);
        return btn;
    }

    public Order getOrderByTable(Integer user_id, Integer exam_id, Integer examination_id, String exam_date, Integer grade) {
        Order order = new Order(user_id, exam_id, examination_id, exam_date, grade);
        return order;
    }

    public JPanel getTopPanel() {
        return topPanel;
    }

    public JScrollPane getTablePanel() {
        return tablePanel;
    }

    public String getPanelName() {
        return panelName;
    }

    public JTable getTable() {
        return table;
    }

    public void setTable(JTable table) {
        this.table = table;
    }

    public void setTopPanel(JPanel topPanel) {
        this.topPanel = topPanel;
    }

    public void setTablePanel(JScrollPane tablePanel) {
        this.tablePanel = tablePanel;
    }

    public void setPanelName(String panelName) {
        this.panelName = panelName;
    }

    public void setOrderUser(Order orderUser) {
        this.orderUser = orderUser;
    }

    public void setSelectExam(Integer selectExam) {
        this.selectExam = selectExam;
    }

    /**
     * 刷新表格界面
     */
    public abstract void refresh();

    public abstract void refresh(List<Exam> examList);

    /**
     * 根据时间和地点搜索考场考试信息
     *
     * @param nation
     * @param year
     * @param month
     * @param day
     */
    public abstract void searchByDateAndNation(String nation, String year, String month, String day);

    /**
     * 预约
     * @param order
     */
    public abstract void userOrder(Order order);

    /**
     * 删除考试
     * @param exam_id
     */
    public abstract void deleteExam(Integer exam_id);
}
