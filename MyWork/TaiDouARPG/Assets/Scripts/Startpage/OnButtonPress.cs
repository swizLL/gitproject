using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // public AudioSource click;
    private Text tex;
    private bool isPress = false;
    private Color col;

    public bool isSeverPress = false;

    public Color32 pressColor = new Color32(0, 0, 0, 255);
    public static string UserName;//玩家账户名
    public static string Password;//玩家密码
    public static string PlayerName;//玩家姓名
    public static severInfo sinfo;
    private void Awake()
    {
        tex = this.GetComponentInChildren<Text>();
        col = tex.color;

    }
    public void Update()
    {
        if (isPress)
        {
            tex.color = pressColor;
        }
        else if (isSeverPress)
        {
            tex.color = sinfo.transform.Find("Text").GetComponent<Text>().color;
        }
        else tex.color = col;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }
    public void startGame()
    {
        //1.连接服务器，验证用户名和服务器
        //To do

        Debug.Log("start game");
        //2.进入角色选择界面
        hidePage(UIController._instance.startPage);
        showPage(UIController._instance.characterSelPage);
        //To do
    }
    public void reseverSelect()
    {
        //选择服务器
        hidePage(UIController._instance.startPage);
        showPage(UIController._instance.severPage);
    }
    public void changeToLogin()
    {
        //从开始界面转到登陆界面
        hidePage(UIController._instance.startPage);
        showPage(UIController._instance.loginPage);
    }
    public void Login()
    {
        //得到用户名和密码，存储起来，在开始界面进行验证
        UserName = UIController._instance.loginUserNameFld.text;
        Password = UIController._instance.loginPasswordFld.text;
        UIController._instance.nameText.text = UserName;
        //返回开始界面
        hidePage(UIController._instance.loginPage);
        showPage(UIController._instance.startPage);
    }
    public void changeToRegister()
    {
        //从登陆界面转到注册界面
        hidePage(UIController._instance.loginPage);
        showPage(UIController._instance.registerPage);
    }
    public void cancel()
    {
        //取消注册，转到登陆界面
        showPage(UIController._instance.loginPage);
        hidePage(UIController._instance.registerPage);
    }
    public void registerAndLogin()
    {
        //1.本地校验（两次输入密码是否相同），连接服务器验证（todo）
        //2.链接失败（密码不同或者用户名已经被注册）（todo）
        //3.连接成功
        //储存信息，返回主界面
        UserName = UIController._instance.regiterUserNameFld.text;
        Password = UIController._instance.registerPasswordFld.text;
        UIController._instance.nameText.text = UserName;
        hidePage(UIController._instance.registerPage);
        showPage(UIController._instance.startPage);
    }
    public void severSelect(GameObject chosSever)
    {
        sinfo = chosSever.GetComponent<severInfo>();
        //将已选择服务器的贴图换成选择的贴图。
        UIController._instance.hasSelecSever.GetComponent<Image>().sprite = sinfo.GetComponent<Image>().sprite;
        //将名字替换掉
        UIController._instance.hasSelecSever.transform.Find("Text").GetComponent<Text>().text = sinfo.transform.Find("Text").GetComponent<Text>().text;
        //将字体颜色替换
        UIController._instance.hasSelecSever.transform.Find("Text").GetComponent<Text>().color = sinfo.transform.Find("Text").GetComponent<Text>().color;
        //有个bug已经解决,因为在前面的代码中，在每一帧都判断是否为判断状态，所以将字体颜色颜色始终控制为红色；
    }
    //确认服务器的选择
    public void ensureSever()
    {
        UIController._instance.severText.text = UIController._instance.hasSelecSever.transform.Find("Text").GetComponent<Text>().text;
        hidePage(UIController._instance.severPage);
        showPage(UIController._instance.startPage);
    }
    public void changeCharacter()
    {
        hidePage(UIController._instance.characterSelPage);
        showPage(UIController._instance.characterChangePage);
    }
    public void ensureCharacter()
    {

        if (UIController._instance.charaIndex == UIController._instance.nowIndex || UIController._instance.charaIndex == -1)
        {
            showPage(UIController._instance.characterSelPage);
            hidePage(UIController._instance.characterChangePage);
            UIController._instance.nowIndex = UIController._instance.charaIndex;
            UIController._instance.playerNameInputFld.text = "";
        }
        else
        {
            PlayerName = UIController._instance.playerNameInputFld.text;
            UIController._instance.playerNameText.text = PlayerName;
            UIController._instance.level.text = "LV.1";
            UIController._instance.nowIndex = UIController._instance.charaIndex;
            GameObject.Destroy(UIController._instance.Selecetchracter.GetComponentInChildren<RawImage>().gameObject);
            GameObject.Instantiate(UIController._instance.character[UIController._instance.charaIndex], UIController._instance.Selecetchracter.transform);
        }
        hidePage(UIController._instance.characterChangePage);
        showPage(UIController._instance.characterSelPage);
    }
    void showPage(GameObject needShow)
    {
        needShow.SetActive(true);
    }
    void hidePage(GameObject needHide)
    {
        needHide.SetActive(false);
    }
}