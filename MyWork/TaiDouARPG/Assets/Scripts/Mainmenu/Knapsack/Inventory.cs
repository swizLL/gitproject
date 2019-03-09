using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物品类型
public enum InventoryType
{
    Equip,
    Drug,
    Box
}
public enum EquipType
{
    Helm,//头盔
    Cloth,//服装
    Weapon,//武器
    Shoes,//靴子
    Necklace,//项链
    Bracelet,//手镯
    Ring,//戒指
    Wings//翅膀
}
public class Inventory
{
    private int id;//ID
    private string name;//名称
    private string icon;//图像名称
    private InventoryType inventoryType;//物品类型
    private EquipType equipType;//装备类型
    private int level = 1;//等级
    private int count = 1;//数量
    private int price = 0;//价格
    private int starLevel = 1;//星级
    private int quality = 1;//品质
    private int damage = 0;//伤害
    private int hp = 0;//生命
    private int power = 0;//战斗力
    private InfoType infoType;//作用类型
    private int applyValue;//作用值
    private string des;//描述

    #region GetSet
    public int ID
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
            return name;
        }
        set
        {
            name = value;
        }
    }
    public string ICON
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
    public InventoryType InventoryType
    {
        get
        {
            return inventoryType;
        }
        set
        {
            inventoryType = value;
        }
    } 
    public EquipType EquipType
    {
        get
        {
            return equipType;
        }
        set
        {
            equipType = value;
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
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price  = value;
        }
    }
    public int  StarLevel
    {
        get
        {
            return starLevel;
        }
        set
        {
            starLevel = value;
        }
    }
    public int Quality
    {
        get
        {
            return quality;
        }
        set
        {
            quality = value;
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
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    public int Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }
    public InfoType InfoType
    {
        get
        {
            return infoType;
        }
        set
        {
            infoType = value;
        }
    }
    public int ApplyValue
    {
        get
        {
            return applyValue;
        }
        set
        {
            applyValue = value;
        }
    }
    public string DES
    {
        get
        {
            return des;
        }
        set
        {
            des = value;
        }
    }
    #endregion
}
