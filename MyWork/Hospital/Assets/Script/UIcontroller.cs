using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIcontroller : MonoBehaviour {
    public static UIcontroller _uiCotl;
    public string logPermission=null;

    public GameObject mainpage;//主界面，也就是登录界面
    public GameObject zhucepage;//注册界面
    public GameObject doctorPage;//医生选择功能的界面
    public GameObject nursePage;//护士选择功能的界面
    public GameObject patientPage;//患者选择功能的界面
    public GameObject userPage;//管理员选择功能的界面
    public GameObject paitientInfoPage;//查询到的患者信息界面
    public GameObject pnoInputPage;//查询患者的医疗证号输入界面
    public GameObject pswChangepage;//修改密码的界面
    public GameObject npnoInputPage;//护士修改病人出入院信息时的医疗编号输入界面
    public GameObject nursechangePage;//护士修改出入院时间的界面
    public GameObject dpnoInputPage;//医生修改病人病情信息时的医疗编号输入界面
    public GameObject docchangePage;//医生修改病情的界面
    public GameObject deletePage;//删除病人的界面
    public GameObject doctorContent;//根据可是查找医生护士的医生content(需要清空他下面的子物体)
    public GameObject nurseContent;//根据可是查找医生护士的护士content(需要清空他下面的子物体)
    public GameObject nullBedContent;//查找出的空房间的content(需要清空他下面的子物体)

    public Text[] patientInfo;

    public Text info;

    private float textlife=1.2f;
    private float timer = 0;
    // Use this for initialization
    private void Awake()
    {
        _uiCotl = this;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            timer = 2;
        }
        if (timer > textlife)
        {
            info.enabled = false;
        }
	}
    public void Login()
    {
        mainpage.SetActive(false);
        Debug.Log("登录成功！！！！");
        switch(logPermission)
        {
            case "医生":
                doctorPage.SetActive(true);
                break;
            case "护士":
                nursePage.SetActive(true);
                break;
            case "管理员":
                userPage.SetActive(true);
                break;
        }
    }
    public void showInfo(string Info)
    {
        info.text = Info;
        info.enabled=true;
        timer = 0;           
    }
    public void closePage(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void openPage(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void destroyChild(GameObject obj)
    {
        for (int i=0; i<obj.transform.childCount; i++)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
}
