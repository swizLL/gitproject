package project.app;

import org.jb2011.lnf.beautyeye.BeautyEyeLNFHelper;
import project.controller.LoginController;

public class MainApp {
    public static void main(String[] args){
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
        new LoginController().init();
    }
}
