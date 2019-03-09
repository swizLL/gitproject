using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private Image headSprite;
    private Text levelText;
    private Text nameText;
    private Text powerText;
    private Slider expSlider;
    private Text expNownumText;
    private Text expMaxnumText;
    private Text diamondNumText;
    private Text coinNumText;
    private Text jingshiNumText;
    private Text energyNownumText;
    private Text energyMaxnumText;
    private Text energyRestoreText;
    private Text energyAllRestoreText;
    private Text toughenNownumText;
    private Text toughenMaxnumText;
    private Text toughenRestoreText;
    private Text toughenAllRestoreText;
    private Button changeNameBtn;
    private Button closeBtn;
    private void Awake()
    {
        headSprite = transform.Find("Head").GetComponent<Image>();
        levelText = transform.Find("Level").GetComponent<Text>();
        nameText = transform.Find("Name").GetComponent<Text>();
        powerText = transform.Find("Power/Value").GetComponent<Text>();
        expSlider = transform.Find("Experience/Tiao").GetComponent<Slider>();
        expNownumText = transform.Find("Experience/Tiao/Value/NowValue").GetComponent<Text>();
        expMaxnumText = transform.Find("Experience/Tiao/Value/FullValue").GetComponent<Text>();
        diamondNumText = transform.Find("ItemLabel/Diamond/Value").GetComponent<Text>();
        coinNumText = transform.Find("ItemLabel/Coin/Value").GetComponent<Text>();
        jingshiNumText = transform.Find("ItemLabel/Jingshi/Value").GetComponent<Text>();
        energyNownumText = transform.Find("EnergyLabel/Energy/Value1/NowValue").GetComponentInChildren<Text>();
        energyMaxnumText = transform.Find("EnergyLabel/Energy/Value1/FullValue").GetComponent<Text>();
        energyRestoreText = transform.Find("EnergyLabel/Energyrecovertime/Time").GetComponent<Text>();
        energyAllRestoreText = transform.Find("EnergyLabel/Energyallrecovertime/Time").GetComponent<Text>();
        toughenNownumText = transform.Find("LilianLabel/Lilian/Value1/NowValue").GetComponent<Text>();
        toughenMaxnumText = transform.Find("LilianLabel/Lilian/Value1/FullValue").GetComponent<Text>();
        toughenRestoreText = transform.Find("LilianLabel/Lilianrecovertime/Time").GetComponent<Text>();
        toughenAllRestoreText = transform.Find("LilianLabel/Lilianallrecovertime/Time").GetComponent<Text>();
        changeNameBtn = transform.Find("ChangeName").GetComponent<Button>();
        closeBtn = transform.Find("ButtonClose").GetComponent<Button>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }
    private void Update()
    {
        UpdateEgyAndThn();//更新能量和体力时间
    }
    private void OnDestroy()
    {
        //当物体被销毁时，事件取消注册
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        UpdateBar();
    }
    void UpdateBar()
    {
        PlayerInfo info = PlayerInfo._instance;
        headSprite.sprite = Resources.Load("HeadSprite/" + info.HeadPortrait, typeof(Sprite)) as Sprite;
        levelText.text = info.Level.ToString();
        nameText.text = info.Name.ToString();
        powerText.text = info.Power.ToString();
        expSlider.maxValue = info.maxExp;
        expSlider.value = info.nowExp;
        info.maxExp = GameController.GetRequireExpByLevel(info.Level);
        expMaxnumText.text = info.maxExp.ToString();
        expNownumText.text = info.nowExp.ToString();
        diamondNumText.text = info.Diamond.ToString();
        coinNumText.text = info.Coin.ToString();
        jingshiNumText.text = info.Jingshi.ToString();
        energyMaxnumText.text = info.maxEnergy.ToString();
        energyNownumText.text = info.nowEnergy.ToString();
        toughenMaxnumText.text = info.maxToughen.ToString();
        toughenNownumText.text = info.nowToughen.ToString();
        UpdateEgyAndThn();
    }
    void UpdateEgyAndThn()
    {
        PlayerInfo info = PlayerInfo._instance;
        if (info.nowEnergy >= info.maxEnergy)
        {
            energyRestoreText.text = "00:00:00";
            energyAllRestoreText.text = "00:00:00";
        }
        else
        {
            int remainTime = 60 - (int)info.energyTimer;
            string scdStr = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            energyRestoreText.text = "00:00:" + scdStr;
            //计算最大恢复时间的分钟数与小时数
            int minutes = ((info.maxEnergy - 1) - info.nowEnergy);
            int hours = minutes / 60;
            minutes = minutes % 60;
            string hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            string minStr = minutes <= 9 ? "0" + minutes : minutes.ToString();
            energyAllRestoreText.text = hoursStr + ":" + minStr + ":" + scdStr;
        }
        if (info.nowToughen >= info.maxToughen)
        {
            toughenRestoreText.text = "00:00:00";
            toughenAllRestoreText.text = "00:00:00";
        }
        else
        {
            int remainTime = 60 - (int)info.toughenTimer;
            string scdStr = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            toughenRestoreText.text = "00:00:" + scdStr;
            //计算最大恢复时间的分钟数与小时数
            int minutes = ((info.maxToughen - 1) - info.nowToughen);
            int hours = minutes / 60;
            minutes = minutes % 60;
            string hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            string minStr = minutes <= 9 ? "0" + minutes : minutes.ToString();
            toughenAllRestoreText.text = hoursStr + ":" + minStr + ":" + scdStr;
        }
    }
    public void ChangeName()
    {
        GameController._instance.NameChangeBar.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
