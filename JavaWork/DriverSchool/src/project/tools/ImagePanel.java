package project.tools;

import javax.swing.*;
import java.awt.*;


public class ImagePanel extends JPanel {
    private Image image;
    public ImagePanel(Image img){
        this.image=img;
    }
    public void paintComponent(Graphics g){
        super.paintComponent(g);
        g.drawImage(image,0,0,this.getWidth(),this.getHeight(),this);
    }
}
