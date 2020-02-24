package project.view.Panel;

import project.controller.UpdateExaminationController;
import project.domin.Examination;
import project.tools.PanelManager;
import project.view.TableModel.ExaminationTableModel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.List;

public abstract class ExaminationTablePanel {
    protected JPanel backgroundPanel;
    private JTable table;
    private JPanel topPanel;
    private JScrollPane tablePanel;
    private Font buttonFont=new Font("宋体",Font.BOLD,10);
    //所选择的考场信息
    private Examination selectExamination;
    public ExaminationTablePanel(List<Examination> examinationList){
        backgroundPanel=new JPanel(new BorderLayout());
        tablePanel=new JScrollPane();
        table=new JTable(new ExaminationTableModel(examinationList));
        addTableClick(examinationList);
        tablePanel.setViewportView(table);
        topPanel = new JPanel();
        topPanel.setLayout(null);
        topPanel.setPreferredSize(new Dimension(1500, 45));
        backgroundPanel.add(topPanel, BorderLayout.NORTH);
        backgroundPanel.add(tablePanel, BorderLayout.CENTER);
        initExaminationTopPanel();
        createSearchInput();
        PanelManager.getPanelManager().setExaminationTablePanel(this);
    }
    /**
     * 给表格添加点击事件
     */
    public void addTableClick(List<Examination> examinationList) {
        table.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                int x = table.getSelectedRow();
                selectExamination = examinationList.get(x);
                System.out.println(selectExamination);
            }
        });
    }

    /**
     * 初始化顶部按钮
     */
    public void initExaminationTopPanel(){
        JButton insertBtn = creatButton("新建.png");
        insertBtn.setLocation(0, 0);
        insertBtn.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                new UpdateExaminationController().init(UPDATETYPE.INSERT);
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
                delExamination();
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
                if(selectExamination==null){
                    JOptionPane.showMessageDialog(null,"请选择要修改的考场！");
                }else{
                    new UpdateExaminationController().init(UPDATETYPE.ALTER);
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
        JLabel addressLabel=new JLabel("考场地址：");
        addressLabel.setBounds(800,20,80,20);
        JTextField addressText=new JTextField();
        addressText.setBounds(870,15,150,25);
        JLabel nameLabel=new JLabel("考场名：");
        nameLabel.setBounds(1020,20,80,20);
        JTextField nameText=new JTextField();
        nameText.setBounds(1090,15,150,25);
        JButton searchButton = new JButton();
        searchButton.setBounds(1250, 10, 30, 30);
        searchButton.setBackground(Color.white);
        ImageIcon icon = new ImageIcon("src/image/搜索.png");
        icon.setImage(icon.getImage().getScaledInstance(30, 30, Image.SCALE_DEFAULT));
        searchButton.setIcon(icon);
        searchButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                search(nameText.getText(),addressText.getText());
            }
        });
        topPanel.add(nameLabel);
        topPanel.add(nameText);
        topPanel.add(addressLabel);
        topPanel.add(addressText);
        topPanel.add(searchButton);
    }
    /**
     * 创建按钮
     * @param iconName
     * @return 按钮
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

    public void setTable(JTable table) {
        this.table = table;
    }

    public JTable getTable() {
        return table;
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

    public Examination getSelectExamination() {
        return selectExamination;
    }

    public void setSelectExamination(Examination selectExamination) {
        this.selectExamination = selectExamination;
    }

    /**
     * 删除考场
     */
    public abstract void delExamination();
    /**
     * 刷新表格
     */
    public abstract void refresh();
    public abstract void refresh(List<Examination> examinationList);
    /**
     * 搜索考场
     */
    public abstract void search(String name,String address);
}
