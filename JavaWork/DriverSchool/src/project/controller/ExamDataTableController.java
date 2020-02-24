package project.controller;

import project.dao.ExamDao;
import project.dao.ExamDaoImpl;
import project.dao.OrderDao;
import project.dao.OrderDaoImpl;
import project.domin.Exam;
import project.domin.Order;
import project.view.TableModel.ExamDataTableModel;
import project.view.Panel.ExamDataTablePanel;

import javax.swing.*;
import java.util.List;

public class ExamDataTableController extends ExamDataTablePanel {
    private ExamDao examDao = new ExamDaoImpl();
    private OrderDao orderDao = new OrderDaoImpl();

    public ExamDataTableController(List<Exam> examList, Integer user_id, String panelName) {
        super(examList, user_id, panelName);
    }

    @Override
    public void refresh() {
        List<Exam> examList = examDao.getNowExam();
        this.setTable(new JTable(new ExamDataTableModel(examList)));
        addTableClick();
        this.getTablePanel().setViewportView(this.getTable());
        this.setOrderUser(null);
        this.setSelectExam(null);
    }

    @Override
    public void refresh(List<Exam> examList) {
        this.setTable(new JTable(new ExamDataTableModel(examList)));
        addTableClick();
        this.getTablePanel().setViewportView(this.getTable());
        this.setOrderUser(null);
        this.setSelectExam(null);
    }

    @Override
    public void searchByDateAndNation(String nation, String year, String month, String day) {
        String sqlPram = "";
        if (!year.equals("0")) {
            sqlPram += year;
            if (!month.equals("00")) {
                sqlPram += ("-" + month);
                if (!day.equals("00")) sqlPram += "-" + (day);
            }
        }
        List<Exam> examList = examDao.getExamByDateAndNation(nation, sqlPram);
        refresh(examList);
    }

    @Override
    public void userOrder(Order order) {
        if (order == null) {
            JOptionPane.showMessageDialog(null, "请选择预约的考试！");
            return;
        }
        Order tempOrder = orderDao.selectOrderByUidAndEid(order.getUser_id(), order.getExam_id());
        Exam exam = examDao.getExamById(order.getExam_id());
        List<Order> levelOrder = orderDao.selectOrderByLevel(exam.getExam_item());
        if (tempOrder != null) {
            JOptionPane.showMessageDialog(null, "您已预约了这场考试！");
            return;
        } else {
            if (levelOrder.size() != 0) {
                JOptionPane.showMessageDialog(null, "您已预约了此等级的考试！");
                return;
            } else {
                if (exam.getOrder_number() < exam.getMax_number()) {
                    orderDao.addOrder(order);
                    int newNum = exam.getOrder_number() + 1;
                    examDao.updateOrderNum(newNum, exam.getExam_id());
                    JOptionPane.showMessageDialog(null, "预约成功！");
                } else {
                    JOptionPane.showMessageDialog(null, "预约人数已满！");
                }
            }
        }
        refresh();
    }

    @Override
    public void deleteExam(Integer exam_id) {
        int delete=JOptionPane.showConfirmDialog(null, "确定要删除这一场考试吗？");
        if(delete==0){
            int i=examDao.delete(exam_id);
            if(i==1){
                JOptionPane.showMessageDialog(null,"删除成功！");
            }else {
                JOptionPane.showMessageDialog(null,"删除失败！");
            }
        }
        refresh();
    }
}
