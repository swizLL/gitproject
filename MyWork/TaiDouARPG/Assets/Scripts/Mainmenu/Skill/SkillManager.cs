using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {
    public static SkillManager _instance; 

    public TextAsset skillInfoText;
    private ArrayList skillList = new ArrayList();
    private void Awake()
    {
        _instance = this;
        InitSkill();
    }
    void InitSkill()
    {
        string[] skillArray = skillInfoText.ToString().Split('\n');
        foreach(string str in skillArray)
        {
            string[] proArray = str.Split(',');
            Skill skill = new Skill();
            skill.Id = int.Parse(proArray[0]);
            skill.Name = proArray[1];
            skill.Icon = proArray[2];
            switch (proArray[3])
            {
                case "Warrior":
                    skill.PlayerType = PlayerType.Warrior;
                    break;
                case "FemaleAssassin":
                    skill.PlayerType = PlayerType.FemaleAssassin;
                    break;
            }
            switch (proArray[4])
            {
                //普通攻击
                case "Basic":
                    skill.SkillType = SkillType.Basic;
                    break;
                case "Skill":
                    skill.SkillType = SkillType.Skill;
                    break;
            }
            switch(proArray[5])
            {
                case "Basic":
                    skill.SkillPos = SkillPos.Basic;
                    break;
                case "One":
                    skill.SkillPos = SkillPos.One;
                    break;
                case "Two":
                    skill.SkillPos = SkillPos.Two;
                    break;
                case "Three":
                    skill.SkillPos = SkillPos.Three;
                    break;
            }
            skill.ColdTime = int.Parse(proArray[6]);
            skill.Damage = int.Parse(proArray[7]);
            skill.Level = 1;
            skillList.Add(skill);
        }
    }
    public Skill GetSkillByPositon(SkillPos skillPos)
    {
        PlayerInfo info = PlayerInfo._instance;
        foreach(Skill skill in skillList)
        {
            if (skill.PlayerType == info.PlayerType && skill.SkillPos == skillPos)
            {
                return skill;
            } 
        }
        return null;
    }
}
