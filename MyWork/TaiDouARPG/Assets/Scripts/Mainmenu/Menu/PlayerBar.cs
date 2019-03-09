using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    private Image headSprite;
    private Text nameText;
    private Text levelText;
    private Slider energySlider;
    private Text nowEnergyText;
    private Text maxEnergyText;
    private Slider toughenSlider;
    private Text nowToughtenText;
    private Text maxToughtenText;
    private Button addEnergyBtn;
    private Button addToughenBtn;
    private void Awake()
    {
        //通过路径查找的起始位置是脚本所挂的物体
        headSprite = transform.Find("HeadImage").GetComponent<Image>();
        nameText = transform.Find("Name").GetComponent<Text>();
        levelText = transform.Find("Level").GetComponent<Text>();
        energySlider = transform.Find("TiLi/Tiao").GetComponent<Slider>();
        nowEnergyText = transform.Find("TiLi/Tiao/Value/NowValue").GetComponent<Text>();
        maxEnergyText = transform.Find("TiLi/Tiao/Value/FullValue").GetComponent<Text>();
        toughenSlider = transform.Find("LiLian/Tiao").GetComponent<Slider>();
        nowToughtenText = transform.Find("LiLian/Tiao/Value/NowValue").GetComponent<Text>();
        maxToughtenText = transform.Find("LiLian/Tiao/Value/FullValue").GetComponent<Text>();
        addEnergyBtn = transform.Find("TiLi/AddTiLi").GetComponent<Button>();
        addToughenBtn = transform.Find("LiLian/AddLiLian").GetComponent<Button>();
        //将人物面板的事件注册上去
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }
    private void OnDestroy()
    {
        //当物体被销毁时，事件取消注册
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.All || type == InfoType.Name || type == InfoType.HeadPortrait || type == InfoType.Level || type == InfoType.Energy || type == InfoType.Toughen)
        {
            UpdateBar();
        }
    }
    //更新面板
    void UpdateBar()
    {
        PlayerInfo info = PlayerInfo._instance;
        headSprite.sprite = Resources.Load("HeadSprite/" + info.HeadPortrait, typeof(Sprite)) as Sprite;
        nameText.text = info.Name;
        levelText.text = info.Level.ToString();
        energySlider.maxValue = info.maxEnergy;
        energySlider.value = info.nowEnergy;
        maxEnergyText.text = info.maxEnergy.ToString();
        nowEnergyText.text = info.nowEnergy.ToString();
        toughenSlider.maxValue = info.maxToughen;
        toughenSlider.value = info.nowToughen;
        maxToughtenText.text = info.maxToughen.ToString();
        nowToughtenText.text = info.nowToughen.ToString();
    }
    public void PlayerBarBtn()
    {
        if (GameController._instance.PlayerStatus.active == false)
            GameController._instance.PlayerStatus.SetActive(true);
        else
            GameController._instance.PlayerStatus.SetActive(false);
    }
}
