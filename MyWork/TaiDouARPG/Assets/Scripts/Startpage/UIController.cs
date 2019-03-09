using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
   static public UIController _instance;//单例模式

    private bool isRefreshSever;
    private string[] severName = {"望月湖","南门口","黄兴路","悦方ID","赤岗冲",
                                  "扫把塘","荣湾镇","燕子岭","太平街","火宫殿",
                                  "坡子街","梅溪湖","司门口","河边头","伍家岭",
                                  "古道巷","天剑路","扫把塘","狮子山","大枫塘"};

    public GameObject startPage;//开始界面
    public GameObject loginPage;//登陆界面
    public GameObject registerPage;//注册界面
    public GameObject severPage;//服务器选择界面
    public GameObject characterSelPage;//人物选择界面
    public GameObject characterChangePage;//切换人物界面
    public GameObject severRed;//红色服务器
    public GameObject severGreen;//绿色服务器
    public GameObject hasSelecSever;//已选择的服务器

     

    public Transform severGrid;
    //登录界面
    public InputField loginUserNameFld;//登陆用户名输入框
    public InputField loginPasswordFld;//登陆密码输入框
    public InputField regiterUserNameFld;//用户名输入框
    public InputField registerPasswordFld;//密码输入框
    public InputField registerRePasswordFld;//确认密码输入框
    //角色切换界面
    public InputField playerNameInputFld;//输入角色名字输入框
    //角色选择界面
    public Text playerNameText;//角色名
    public Text level;//等级
    public GameObject Selecetchracter;
    public int nowIndex = 0;//当前角色序列
    public int charaIndex=-1;//选择角色的序列
    public GameObject[] character;//角色数组 

    public Text nameText;//名字框
    public Text severText;//服务器框
    

    void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        reFreshSever(); 
    }
    //初始化服务器列表
    void reFreshSever()
    {
        if (isRefreshSever) return; 
        for(int i=0;i!=20;i++)
        {
            string ip = "192.168.1.23";
            string name = +(i + 1) + "区 " + severName[i];
            int userNum = Random.Range(0, 100);
            GameObject ga = null;
            if(userNum<50)
            {
                ga = Instantiate(severGreen, severGrid);//实例化这个物体为severGrid的子物体
            }
            else
            {
                ga = Instantiate(severRed, severGrid);
            }
            severInfo SI = ga.GetComponent<severInfo>();
            SI.IP = ip;
            SI.name = name;
        }
        isRefreshSever = true;
    }
}
