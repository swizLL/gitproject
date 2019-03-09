using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KnaspackRole : MonoBehaviour
{
    private KnaspackRoleEquip HelmImage;
    private KnaspackRoleEquip ClothImage;
    private KnaspackRoleEquip WeaponImage;
    private KnaspackRoleEquip ShoesImage;
    private KnaspackRoleEquip NecklaceImage;
    private KnaspackRoleEquip BraceletImage;
    private KnaspackRoleEquip RingImage;
    private KnaspackRoleEquip WingsImage;

    private Text hpText;
    private Text damageText;
    private Text maxExpText;
    private Text nowExpText;
    private Slider expSlider;

    private void Awake()
    {
        HelmImage = transform.Find("Helm").GetComponent<KnaspackRoleEquip>();
        ClothImage = transform.Find("Cloth").GetComponent<KnaspackRoleEquip>();
        WeaponImage = transform.Find("Weapon").GetComponent<KnaspackRoleEquip>();
        ShoesImage = transform.Find("Shoes").GetComponent<KnaspackRoleEquip>();
        NecklaceImage = transform.Find("Necklace").GetComponent<KnaspackRoleEquip>();
        BraceletImage = transform.Find("Bracelet").GetComponent<KnaspackRoleEquip>();
        RingImage = transform.Find("Ring").GetComponent<KnaspackRoleEquip>();
        WingsImage = transform.Find("Wings").GetComponent<KnaspackRoleEquip>();
        hpText = transform.Find("Info/Health/ValueBg/Value").GetComponent<Text>();
        damageText = transform.Find("Info/Damage/ValueBg/Value").GetComponent<Text>();
        maxExpText = transform.Find("Info/Exp/Tiao/Value/FullValue").GetComponent<Text>();
        nowExpText = transform.Find("Info/Exp/Tiao/Value/NowValue").GetComponent<Text>();
        expSlider = transform.Find("Info/Exp/Tiao").GetComponent<Slider>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }
    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.All || type == InfoType.HP || type == InfoType.Damage || type == InfoType.Exp||type==InfoType.Equip)
        {
            UpdateInfo();
        }
    }
    void UpdateInfo()
    {
        PlayerInfo info = PlayerInfo._instance;

        //HelmImage.SetID(info.HelmID);
        //ClothImage.SetID(info.ClothID);
        //WeaponImage.SetID(info.WeaponID);
        //ShoesImage.SetID(info.ShoesID);
        //NecklaceImage.SetID(info.NecklaceID);
        //BraceletImage.SetID(info.BraceletID);
        //RingImage.SetID(info.RingID);
        //WingsImage.SetID(info.WingsID);

        //改变装备栏中的装备
        HelmImage.SetInventoryItem(info.helmInvItem);
        ClothImage.SetInventoryItem(info.clothInvItem);
        WeaponImage.SetInventoryItem(info.weaponInvItem);
        ShoesImage.SetInventoryItem(info.shoesInvItem);
        NecklaceImage.SetInventoryItem(info.necklaceInvItem);
        BraceletImage.SetInventoryItem(info.braceleInvItem);
        RingImage.SetInventoryItem(info.ringInvItem);
        WingsImage.SetInventoryItem(info.wingsInvItem);

        hpText.text = info.HP.ToString();
        damageText.text = info.Damage.ToString();
        expSlider.value = (float)info.nowExp / GameController.GetRequireExpByLevel(info.Level + 1);
        maxExpText.text = GameController.GetRequireExpByLevel(info.Level + 1).ToString();
        nowExpText.text = info.nowExp.ToString();
    }
}
