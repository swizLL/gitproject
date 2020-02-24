package project.view.Panel;

import project.dao.OrderDao;
import project.dao.OrderDaoImpl;
import project.domin.Order;
import project.tools.PanelManager;
import project.view.TableModel.AdminGradeTableModel;
import project.view.TableModel.UserGradeTableModel;
import project.view.TableModel.UserOrderTableModel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.List;

public abstract class UserOrderTablePanel {
    protected JPanel backgroundPanel;
    private JTable table;
    private JPanel topPanel;
    private JScrollPane tablePanel;
    private OrderDao orderDao = new OrderDaoImpl();
    protected Integer user_id;
    private Font buttonFont=new Font("宋体",Font.BOLD,10);
    //选择的预约记录
    private Order orderUser;

    public UserOrderTablePanel(List<Order> orderList, Integer user_id, String panelName) {
        this.user_id = user_id;
        backgroundPanel = new JPanel(new BorderLayout());
        tablePanel = new JScrollPane();
        switch (panelName) {
            case "order":
                table = new JTable(new UserOrderTableModel(orderList));
                break;
            case "grade":
                table = new JTable(new UserGradeTableModel(orderList));
                break;
            case "admGrade":
                table=new JTable(new AdminGradeTableModel(orderList));
                table.addMouseListener(new MouseAdapter() {
                    @Override
                    public void mouseClicked(MouseEvent e) {
                        setGrade();
                    }
                });
                break;
        }
        addTableClick(orderList);
        tablePanel.setViewportView(table);
        topPanel = new JPanel();
        topPanel.setLayout(null);
        topPanel.setPreferredSize(new Dimension(1500, 45));
        backgroundPanel.add(topPanel, BorderLayout.NORTH);
        backgroundPanel.add(tablePanel, BorderLayout.CENTER);
        //判断应该怎么初始化顶部按钮
        if (AbstractMainFrame.isAdmin) {
            initMarkingTopPanel();
        } else {
            switch (panelName) {
                case "order":
                    initUserOrderTopPanel();
                    break;
                case "grade":
                    initGradeTopPanel();
                    break;
            }
        }
        PanelManager.getPanelManager().setUserOrderTablePanel(this);
    }

    /**
     * 给表格添加点击事件
     */
    public void addTableClick(List<Order> orderList) {
        table.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                int x = table.getSelectedRow();
                orderUser = orderList.get(x);
                System.out.println(orderUser);
            }
        });
    }

    /**
     * 普通用户的我的预约界面按钮
     */
    public void initUserOrderTopPanel() {
        JButton orderBtn = creatButton("删除.png");
        orderBtn.setLocation(0, 0);
        JLabel label1 = new JLabel("取 消");
        label1.setBounds(0, 30, 30, 15);
        label1.setFont(buttonFont);
        orderBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                cancelOrder(orderUser);
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
     * 普通用户的成绩查询界面按钮
     */
    public void initGradeTopPanel() {
        JButton refreshBtn = creatButton("刷新.png");
        refreshBtn.setLocation(0, 0);
        refreshBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                refreshGrade();
            }
        });
        JLabel label1 = new JLabel("刷 新");
        label1.setFont(buttonFont);
        label1.setBounds(0, 30, 30, 15);
        topPanel.add(label1);
        topPanel.add(refreshBtn);
    }
    /**
     * 管理员打分界面按钮
     */
    public void initMarkingTopPanel() {
        JButton refreshBtn = creatButton("刷新.png");
        refreshBtn.setLocation(0, 0);
        refreshBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                refreshMarking();
            }
        });
        JLabel label1 = new JLabel("刷 新");
        label1.setFont(buttonFont);
        label1.setBounds(0, 30, 30, 15);
        topPanel.add(label1);
        topPanel.add(refreshBtn);
    }

    /**
     * 创建按钮
     * @param iconName
     * @return
     */
    public JButton creatButton(String iconName) {
        JButton btn = new JButton();
        btn.setSize(30, 30);
        btn.setBackground(Color.white);
        ImageIcon icon = new ImageIcon("src/image/" + iconName);
        icon.setImage(icon.getImage().getScaledInstance(30, 30, Image.SCALE_DEFAULT));
        btn.setIcon(icon);
        return btn;
    }

    public JPanel getBackgroundPanel() {
        return backgroundPanel;
    }

    public void setBackgroundPanel(JPanel backgroundPanel) {
        this.backgroundPanel = backgroundPanel;
    }

    public JTable getTable() {
        return table;
    }

    public void setTable(JTable table) {
        this.table = table;
    }

    public JPanel getTopPanel() {
        return topPanel;
    }

    public void setTopPanel(JPanel topPanel) {
        this.topPanel = topPanel;
    }

    public JScrollPane getTablePanel() {
        return tablePanel;
    }

    public void setTablePanel(JScrollPane tablePanel) {
        this.tablePanel = tablePanel;
    }

    public OrderDao getOrderDao() {
        return orderDao;
    }

    public void setOrderDao(OrderDao orderDao) {
        this.orderDao = orderDao;
    }

    public Integer getUser_id() {
        return user_id;
    }

    public void setUser_id(Integer user_id) {
        this.user_id = user_id;
    }

    public Order getOrderUser() {
        return orderUser;
    }

    public void setOrderUser(Order orderUser) {
        this.orderUser = orderUser;
    }

    /**
     * 刷新表格界面
     */
    public abstract void refresh();

    /**
     * 刷新成绩界面
     */
    protected abstract void refreshGrade();
    /**
     * 刷新管理员打分界面
     */
    protected abstract void refreshMarking();
    /**
     * 取消预约
     */
    public abstract void cancelOrder(Order order);

    /**
     * 成绩打分
     */
    public abstract void setGrade();
}
