using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillItemUI : MonoBehaviour {
    public SkillPos skillPos;
    private Skill skill;
    private Image image;
    private Button btn;

    public Image Image
    {
        get
        {
            if (image == null)
            {
                image = this.GetComponent<Image>();
            }
            return image;
        }
    }
    private void Awake()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    private void Start()
    {
        UpdateShow();
    }
    //更新技能显示的面板
    public void UpdateShow()
    {
        skill = SkillManager._instance.GetSkillByPositon(skillPos);
        Image.sprite = Resources.Load("Skill/" + skill.Icon, typeof(Sprite)) as Sprite;
    }

    void onClick()
    {
        SkillUI._instance.onSkillClick(skill);
    }
}
