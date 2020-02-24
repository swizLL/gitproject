package project.controller;

import project.dao.ExamDao;
import project.dao.ExamDaoImpl;
import project.dao.OrderDao;
import project.dao.OrderDaoImpl;
import project.domin.Order;
import project.view.Panel.AbstractMarkingFrame;
import project.view.TableModel.AdminGradeTableModel;
import project.view.TableModel.UserGradeTableModel;
import project.view.TableModel.UserOrderTableModel;
import project.view.Panel.UserOrderTablePanel;

import javax.swing.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.List;

public class UserOrderTableController extends UserOrderTablePanel {
    private OrderDao orderDao=new OrderDaoImpl();
    public ExamDao examDao=new ExamDaoImpl();
    public UserOrderTableController(List<Order> orderList, Integer user_id,String panelName) {
        super(orderList, user_id,panelName);
    }

    @Override
    public void refresh() {
        List<Order> orderList=orderDao.selectOrderByUid(user_id);
        this.setTable(new JTable(new UserOrderTableModel(orderList)));
        addTableClick(orderList);
        this.getTablePanel().setViewportView(this.getTable());
        this.setOrderUser(null);
    }

    @Override
    protected void refreshGrade() {
        List<Order> gradeList=orderDao.selectGradeByUid(user_id);
        this.setTable(new JTable(new UserGradeTableModel(gradeList)));
        addTableClick(gradeList);
        this.getTablePanel().setViewportView(this.getTable());
        this.setOrderUser(null);
    }

    @Override
    protected void refreshMarking() {
        List<Order> orderList=orderDao.selectUntreatedOrder();
        this.setTable(new JTable(new AdminGradeTableModel(orderList)));
        this.getTable().addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                new AbstractMarkingFrame().init();
            }
        });
        addTableClick(orderList);
        this.getTablePanel().setViewportView(this.getTable());
        this.setOrderUser(null);
    }

    @Override
    public void cancelOrder(Order order) {
        if(order==null){
            JOptionPane.showMessageDialog(null,"请选择你要取消的预约场次！");
            return;
        }
        Integer cancel= JOptionPane.showConfirmDialog(null,"确定要取消预约吗？");
        if(cancel==0){
            int i=orderDao.deleteOrder(order.getUser_id(),order.getExam_id());
            int newNum=examDao.getExamById(order.getExam_id()).getOrder_number()-1;
            int j=examDao.updateOrderNum(newNum,order.getExam_id());
            if(i==1&&j==1){
                JOptionPane.showMessageDialog(null,"取消预约成功！");
            }else{
                JOptionPane.showMessageDialog(null,"取消预约失败！");
            }
        }
        refresh();
    }

    @Override
    public void setGrade() {
        new AbstractMarkingFrame().init();
    }
}
