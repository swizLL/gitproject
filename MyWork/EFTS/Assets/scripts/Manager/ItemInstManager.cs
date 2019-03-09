using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物品生成的管理类,开箱子或者商店刷新
public class ItemInstManager : MonoBehaviour
{
    public static ItemInstManager _instance;
    private void Awake()
    {
        _instance = this;
    }
    public int GetItemId()
    {
        List<int> weaponList = new List<int>();
        float possible = Random.value;
        Debug.Log(possible);
        if (possible < 0.1f)
        {
            Debug.Log("狙击枪");
            weaponList = WeaponManager._instance.Getobjtypelist(WeaponManager.WeaponType.AWP);
        }
        else if (0.1f < possible && 0.3f > possible)
        {
            Debug.Log("弓箭");
            weaponList = WeaponManager._instance.Getobjtypelist(WeaponManager.WeaponType.bow);
        }
        else if (0.3f < possible && 0.6f > possible)
        {
            Debug.Log("霰弹枪");
            weaponList = WeaponManager._instance.Getobjtypelist(WeaponManager.WeaponType.shotgun);
        }
        else
        {
            Debug.Log("步枪");
            weaponList = WeaponManager._instance.Getobjtypelist(WeaponManager.WeaponType.musket);
        }
        Debug.Log(weaponList.Count);
        if (weaponList.Count!=0)
        {
            int randomNum = Random.Range(0, weaponList.Count-1);
            Debug.Log(randomNum);
            return weaponList[randomNum];
        }
        else
        {
            Debug.Log("这里面没东西！");
            return -1;
        }        
    }
}
