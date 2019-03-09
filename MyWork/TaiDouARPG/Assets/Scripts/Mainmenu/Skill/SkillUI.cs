using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    public static SkillUI _instance;

    private Text skillNameText;
    private Text skillDesText;
    private Button closeBtn;
    private Button upgrandBtn;
    private Text upgradeBtnText;
    private Skill skill;

    private void Awake()
    {
        _instance = this;
        skillNameText = transform.Find("BG/NameText").GetComponent<Text>();
        skillDesText = transform.Find("BG/DesText").GetComponent<Text>();
        closeBtn = transform.Find("ButtonClose").GetComponent<Button>();
        closeBtn.onClick.AddListener(Close);
        upgrandBtn = transform.Find("UpGradeBtn").GetComponent<Button>();
        upgrandBtn.onClick.AddListener(onUpgrade);
        upgradeBtnText = transform.Find("UpGradeBtn/Text").GetComponent<Text>();

        skillNameText.text = "";
        skillDesText.text = "";
        disableUpgradeBtn("选择技能 ");
    }
    void disableUpgradeBtn(string str = "")
    {
        upgrandBtn.interactable = false;
        if (str != "")
        {
            upgradeBtnText.text = str;
        }
    }
    void enableUpgradeBtn(string str = "")
    {
        upgrandBtn.interactable = true;
        if (str != "")
        {
            upgradeBtnText.text = str;
        }
    }
    public void onSkillClick(Skill skill)
    {
        this.skill = skill;
        if ((500 * (skill.Level + 1)) < PlayerInfo._instance.Coin)
        {
            if (skill.Level < PlayerInfo._instance.Coin)
            {
                enableUpgradeBtn("升 级");
            }
            else
            {
                disableUpgradeBtn("最大等级");
            }
        }
        else
        {
            disableUpgradeBtn("金币不足");
        }
        skillNameText.text = skill.Name + " Lv." + skill.Level.ToString();
        skillDesText.text = "当前攻击力：" + (skill.Damage * skill.Level).ToString() + "\n下一级攻击力：" + (skill.Damage * (skill.Level + 1)).ToString() + "\n升级所需金币：" + ((500 * (skill.Level + 1)).ToString());
    }
    void onUpgrade()
    {
        if (skill.Level < PlayerInfo._instance.Level)
        {
            int coinNeed = (500 * (skill.Level + 1));
            if (PlayerInfo._instance.GetCoin(coinNeed))
            {
                skill.Upgrade();
                onSkillClick(skill);
            }
            else
            {
                disableUpgradeBtn("金币不足");
            }
        }
        else
        {
            disableUpgradeBtn("最大等级");
        }
    }
    public void Show()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        skillNameText.text = "";
        skillDesText.text = "";
        disableUpgradeBtn("选择技能 ");
    }
    public void Close()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(1200, 0, 0);
    }
}
