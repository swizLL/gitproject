using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public enum WeaponType
    {
        musket,//步枪
        AWP,//狙击枪
        shotgun,//霰弹枪
        bow,//弓箭
        bullet//子弹
    }
    private TextAsset weaponInfo;
    //储存所有武器信息
    private Dictionary<int, WeaponInfo> weaponDict = new Dictionary<int, WeaponInfo>();
    //存储所有武器种类信息
    private Dictionary<WeaponType, List<int>> weaponTypeDict = new Dictionary<WeaponType, List<int>>();
    public static WeaponManager _instance;
    private void Awake()
    {
        weaponInfo = Resources.Load("weapon", typeof(TextAsset))as TextAsset;
        _instance = this;
        getInfo();
    }
    void getInfo()
    {
        //往武器种类字典添加信息
        List<int> wpTypeList;
        wpTypeList = new List<int>();
        weaponTypeDict.Add(WeaponType.musket, wpTypeList);
        wpTypeList = new List<int>();
        weaponTypeDict.Add(WeaponType.AWP, wpTypeList);
        wpTypeList = new List<int>();
        weaponTypeDict.Add(WeaponType.shotgun, wpTypeList);
        wpTypeList = new List<int>();
        weaponTypeDict.Add(WeaponType.bow, wpTypeList);
        wpTypeList = new List<int>();
        weaponTypeDict.Add(WeaponType.bullet, wpTypeList);
        //切分文本文件
        string[] weaponStr = weaponInfo.text.Split('\n');
        foreach(string str in weaponStr)
        {
            string[] wpInfoStr=str.Split(',');
            WeaponInfo info = new WeaponInfo();
            info.ID = int.Parse(wpInfoStr[0]);
            info.weaponName = wpInfoStr[1];
            info.iconName = wpInfoStr[2];
            
            switch(wpInfoStr[3])
            {
                case "Musket":
                    info.weaponType = WeaponType.musket;
                    weaponTypeDict[WeaponType.musket].Add(info.ID);
                    break;
                case "AWP":
                    info.weaponType = WeaponType.AWP;
                    weaponTypeDict[WeaponType.AWP].Add(info.ID);
                    break;
                case "Shotgun":
                    info.weaponType = WeaponType.shotgun;
                    weaponTypeDict[WeaponType.shotgun].Add(info.ID);
                    break;
                case "Bow":
                    info.weaponType = WeaponType.bow;
                    weaponTypeDict[WeaponType.bow].Add(info.ID);
                    break;
                case "Bullet":
                    info.weaponType = WeaponType.bullet;
                    weaponTypeDict[WeaponType.bullet].Add(info.ID);
                    break;
            }
            switch(info.weaponType)
            {
                case WeaponType.musket:
                    info.damage = int.Parse(wpInfoStr[4]);
                    info.shootSpeed = int.Parse(wpInfoStr[5]);
                    info.shootDistance = int.Parse(wpInfoStr[6]);
                    info.critiPoss = float.Parse(wpInfoStr[7]);
                    info.bulletId = int.Parse(wpInfoStr[8]);
                    info.hotLimit = float.Parse(wpInfoStr[11]);
                    break;
                case WeaponType.AWP:
                    info.damage = int.Parse(wpInfoStr[4]);
                    info.shootSpeed = int.Parse(wpInfoStr[5]);
                    info.shootDistance = int.Parse(wpInfoStr[6]);
                    info.critiPoss = float.Parse(wpInfoStr[7]);
                    info.bulletId = int.Parse(wpInfoStr[8]);
                    break;
                case WeaponType.bow:
                    info.damage = int.Parse(wpInfoStr[4]);
                    info.shootSpeed = int.Parse(wpInfoStr[5]);
                    info.shootDistance = int.Parse(wpInfoStr[6]);
                    info.critiPoss = float.Parse(wpInfoStr[7]);
                    info.bulletId = int.Parse(wpInfoStr[8]);
                    info.bowTime = int.Parse(wpInfoStr[9]);
                    break;
                case WeaponType.shotgun:
                    info.damage = int.Parse(wpInfoStr[4]);
                    info.shootSpeed = int.Parse(wpInfoStr[5]);
                    info.shootDistance = int.Parse(wpInfoStr[6]);
                    info.critiPoss = float.Parse(wpInfoStr[7]);
                    info.bulletId = int.Parse(wpInfoStr[8]);
                    info.sgBulltNum = int.Parse(wpInfoStr[10]);
                    break;
            }
            weaponDict.Add(info.ID, info);
        }
    }
    //根据id获取物品
    public WeaponInfo getobjectinfobyid(int id)
    {
        WeaponInfo info = null;
        if (weaponDict.ContainsKey(id))
        {
            info = weaponDict[id];
        }
        else
        {
            Debug.Log("objectdict don't have this object!");

        }
        return info;
    }
    //根据品种查找物品清单
    public List<int> Getobjtypelist(WeaponType type)
    {
        return weaponTypeDict[type];
    }
}
