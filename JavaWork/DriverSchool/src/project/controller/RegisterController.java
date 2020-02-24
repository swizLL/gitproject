package project.controller;

import org.apache.commons.codec.digest.DigestUtils;
import project.dao.UserDao;
import project.dao.UserDaoImpl;
import project.domin.User;
import project.tools.PanelManager;
import project.view.Panel.AbstractRegisterFrame;

import javax.swing.*;
import java.util.regex.Pattern;

public class RegisterController extends AbstractRegisterFrame {
    private User hasUser1 = null;
    private User hasUser2 = null;
    private UserDao userDao = new UserDaoImpl();
    private static String numPattern = "^[0-9]*$";
    //匹配电话号码
    private static String phonePattern = "^[1](([3][0-9])|([4][5,7,9])|([5][0-9])|([6][6])|([7][3,5,6,7,8])|([8][0-9])|([9][8,9]))[0-9]{8}$";
    //匹配身份证号
    private static String idCardPattern = "(^[1-9]\\d{5}(18|19|20)\\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\\d{3}[0-9Xx]$)|\" +\n" +
            "                \"(^[1-9]\\d{5}\\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\\d{3}$)";
    private static final long serialVersionUID = 1L;

    @Override
    public void register() {
        boolean infoMatch = (Pattern.matches(numPattern, getUidText().getText()) &&
                Pattern.matches(numPattern, getAgeText().getText()) &&
                Pattern.matches(phonePattern, getPhoneText().getText()) &&
                Pattern.matches(idCardPattern, getIdCardText().getText()) &&
                getUidText().getText().length() < 20);
        if (hasNull()) {
            JOptionPane.showMessageDialog(null, "请完善您的信息!");
            return;
        }
        if (!infoMatch) {
            JOptionPane.showMessageDialog(null, "信息填写错误，请仔细检查!");
            return;
        } else {
            Integer user_id = Integer.valueOf(getUidText().getText());
            Integer age = Integer.valueOf(getAgeText().getText());
            if (age < 0 || age > 130) {
                getAgeText().setText("");
                JOptionPane.showMessageDialog(null, "年龄不符合规范，请重新填写！");
                return;
            }
            String password = String.valueOf(getPswText().getPassword());
            String ensurePsw = String.valueOf(getEnsurePswText().getPassword());
            if (!password.equals(ensurePsw)) {
                getEnsurePswText().setText("");
                JOptionPane.showMessageDialog(null, "两次输入的密码不一致，请重新填写！");
                return;
            }
            String md5Pwd = DigestUtils.md5Hex(password).toUpperCase();
            String name = getNameText().getText();
            String sex = getMan().isSelected() ? "男" : "女";
            String phone = getPhoneText().getText();
            Integer exam_status = getLevelBox().getSelectedIndex();
            String id_card = getIdCardText().getText();
            if (!hasSameUser(id_card, user_id)) {
                User user = new User(user_id, id_card, md5Pwd, name, sex, phone, age, exam_status);
                userDao.save(user);
                JOptionPane.showMessageDialog(null, "注册成功！");
                this.setVisible(false);
                new LoginController().setVisible(true);
            } else {
                JOptionPane.showMessageDialog(null, "已存在此用户，请重新注册！");
            }
        }
    }

    @Override
    public void alterUser() {
        boolean infoMatch = (Pattern.matches(numPattern, getAgeText().getText()) &&
                Pattern.matches(phonePattern, getPhoneText().getText()));
        if (alterHasNull()) {
            JOptionPane.showMessageDialog(null, "请完善您的信息!");
            return;
        }
        if (!infoMatch) {
            JOptionPane.showMessageDialog(null, "信息填写错误，请仔细检查!");
            return;
        } else {
            Integer user_id = Integer.valueOf(getUidValue().getText());
            Integer age = Integer.valueOf(getAgeText().getText());
            if (age < 0 || age > 130) {
                getAgeText().setText("");
                JOptionPane.showMessageDialog(null, "年龄不符合规范，请重新填写！");
                return;
            }
            String password = String.valueOf(getPswText().getPassword());
            String ensurePsw = String.valueOf(getEnsurePswText().getPassword());
            if (!password.equals(ensurePsw)) {
                getEnsurePswText().setText("");
                JOptionPane.showMessageDialog(null, "两次输入的密码不一致，请重新填写！");
                return;
            }
            String md5Pwd = DigestUtils.md5Hex(password).toUpperCase();
            if(password.equals("")&&ensurePsw.equals("")){
                md5Pwd=getUser().getPassword();
            }
            String name = getNameValue().getText();
            String sex = getMan().isSelected() ? "男" : "女";
            String phone = getPhoneText().getText();
            Integer exam_status = getLevelBox().getSelectedIndex();
            String id_card = getIdCardValue().getText();
            User user = new User(user_id, id_card, md5Pwd, name, sex, phone, age, exam_status);
            int i = userDao.update(user, user_id);
            if (i != 0) {
                JOptionPane.showMessageDialog(null, "修改成功！");
                PanelManager.getPanelManager().getAbstractMainFrame().setUser(user);
                this.setVisible(false);
            } else {
                JOptionPane.showMessageDialog(null, "修改失败！");
            }
        }
    }

    private boolean hasSameUser(String id_card, Integer user_id) {
        hasUser1 = userDao.getUserByIdCard(id_card);
        hasUser2 = userDao.getUserByID(user_id);
        if (hasUser1 == null && hasUser2 == null) {
            return false;
        } else {
            return true;
        }
    }
    private boolean alterHasNull() {
        if (getAgeText().getText().equals("") || getPhoneText().getText().equals("")) {
            return true;
        }
        return false;
    }
    private boolean hasNull() {
        if (getUidText().getText().equals("") ||
                String.valueOf(getPswText().getPassword()).equals("") ||
                String.valueOf(getEnsurePswText().getPassword()).equals("") ||
                getAgeText().getText().equals("") || getPhoneText().getText().equals("")
                || getIdCardText().getText().equals("")
        ) {
            return true;
        }
        return false;
    }

    @Override
    public void reset() {
        getUidText().setText("");
        getPswText().setText("");
        getEnsurePswText().setText("");
        getNameText().setText("");
        getAgeText().setText("");
        getPhoneText().setText("");
        getLevelBox().setSelectedIndex(0);
        getIdCardText().setText("");
    }

    @Override
    public void cancel() {
        this.setVisible(false);
        new LoginController().init();
    }

    @Override
    public void alterCancel() {
        this.setVisible(false);
    }

    @Override
    public void alterReset() {
        getPswText().setText("");
        getEnsurePswText().setText("");
        getNameValue().setText(getUser().getName());
        getUidValue().setText(getUser().getUser_id().toString());
        getIdCardValue().setText(getUser().getId_card());
        getAgeText().setText(getUser().getAge().toString());
        if (getUser().getSex().equals("男")) {
            getMan().setSelected(true);
        } else {
            getWomen().setSelected(true);
        }
        getPhoneText().setText(getUser().getPhone());
        getLevelBox().setSelectedIndex(getUser().getExam_status());
    }
}
