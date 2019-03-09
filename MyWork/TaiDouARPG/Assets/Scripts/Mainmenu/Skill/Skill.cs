using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType
{
    Basic,
    Skill
}
public enum SkillPos
{
    Basic,
    One,
    Two,
    Three
}
public class Skill : MonoBehaviour {
    private int id;
    private string name1;
    private string icon;
    private PlayerType playerType;
    private SkillType skillType;
    private SkillPos skillPos;
    private int coldTime;
    private int damage;
    private int level=1;
    #region Getset
    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name1;
        }

        set
        {
            name1 = value;
        }
    }

    public string Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public PlayerType PlayerType
    {
        get
        {
            return playerType;
        }

        set
        {
            playerType = value;
        }
    }

    public SkillType SkillType
    {
        get
        {
            return skillType;
        }

        set
        {
            skillType = value;
        }
    }

    public SkillPos SkillPos
    {
        get
        {
            return skillPos;
        }

        set
        {
            skillPos = value;
        }
    }

    public int ColdTime
    {
        get
        {
            return coldTime;
        }

        set
        {
            coldTime = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }
    #endregion Getset
    public void Upgrade()
    {
        Level++;
    }
}

