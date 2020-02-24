package project.tools;

import com.wf.captcha.SpecCaptcha;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;

public class CaptchaUtils {
    private static String capt="";
    public static String getCaptchaImage(){
        OutputStream outputStream=null;
        String path="src/captchaImage/captcha.png";
        try{
            //验证码图片的输出流
            outputStream=new FileOutputStream(path);
            //png类型的验证码
            SpecCaptcha captcha=new SpecCaptcha(130,48);
            //获取验证码的字符
            //System.out.println(captcha.text());
            //输出验证码图片
            captcha.out(outputStream);
            capt=captcha.text();
        }catch (FileNotFoundException e){
            e.printStackTrace();
        }finally {
            try {
                outputStream.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        return path;
    }
    public static String getCapt(){
        return capt;
    }
}
