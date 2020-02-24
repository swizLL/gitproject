package project.view.Panel;

import javax.swing.*;
import java.awt.*;
import java.io.File;

/**
 * 模拟考试的界面
 */
public class PracticeTestPanel {
    protected JPanel backgroundPanel;
    private JScrollPane textPanel;
    private JTextArea textArea;

    public PracticeTestPanel(String textString){
        backgroundPanel=new JPanel(new BorderLayout());
        textPanel=new JScrollPane();
        textArea=new JTextArea(textString);
        textArea.setEditable(false);
        textArea.setFont(new Font("黑体",Font.PLAIN,20));
        textPanel.setViewportView(textArea);
        backgroundPanel.add(textPanel,BorderLayout.CENTER);
    }
}
