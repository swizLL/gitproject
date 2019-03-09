using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo:WeaponManager
{
    public int ID;
    public string weaponName;
    public string iconName;
    public WeaponType weaponType;
    public int damage;
    public int shootSpeed;
    public int shootDistance;
    public float critiPoss;//暴击率 
    public int bulletId;//子弹id
    public int bowTime;//弓箭蓄力时间
    public int sgBulltNum;//霰弹枪子弹数量
    public float hotLimit;//步枪温度上限
    //重载等于号
    public void InfoCopy(WeaponInfo info)
    {
        ID = info.ID;
        weaponName = info.weaponName;
        iconName = info.iconName;
        weaponType = info.weaponType;
        damage = info.damage;
        shootSpeed = info.shootSpeed;
        shootDistance = info.shootDistance;
        critiPoss = info.critiPoss;
        bulletId = info.bulletId;
        bowTime= info.bowTime;
        sgBulltNum = info.sgBulltNum;
        hotLimit = info.hotLimit;
    }
}
