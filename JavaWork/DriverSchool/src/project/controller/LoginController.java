package project.controller;

import org.apache.commons.codec.digest.DigestUtils;
import project.dao.UserDao;
import project.dao.UserDaoImpl;
import project.domin.User;
import project.tools.CaptchaUtils;
import project.view.Panel.AbstractLoginFrame;

import javax.swing.*;
import java.util.regex.Pattern;

public class LoginController extends AbstractLoginFrame {
    private static String idPattern = "^[0-9]*$";
    private static final long serialVersionUID=1L;
    @Override
    public void login() {
        UserDao userDao = new UserDaoImpl();
        boolean isMatch = Pattern.matches(idPattern,getUserText().getText());
        String psw = String.valueOf(super.getPwdText().getPassword());
        String md5Pwd= DigestUtils.md5Hex(psw).toUpperCase();
        //验证码
        String capt= CaptchaUtils.getCapt().toLowerCase();
        String enterCapt=getCaptText().getText().toLowerCase();
        if(!capt.equals(enterCapt)){
            getCaptText().setText("");
            JOptionPane.showMessageDialog(null, "验证码错误！");
            return;
        }
        if (psw.length() == 0) {
            JOptionPane.showMessageDialog(null, "密码不能为空！");
            return;
        }
        if (!isMatch) {
            getUserText().setText("");
            getPwdText().setText("");
            JOptionPane.showMessageDialog(null, "输入的用户名不正确！");
            return;
        } else {
            Integer id = Integer.valueOf(getUserText().getText());
            User user = userDao.userLogin(id, md5Pwd);
            if (user == null) {
                getPwdText().setText("");
                JOptionPane.showMessageDialog(null, "用户名或密码错误！");
            } else {
                JOptionPane.showMessageDialog(null, "登录成功！");
                MainViewController.setUser(user);
                this.setVisible(false);
                new MainViewController().init();
            }
        }
    }
}
