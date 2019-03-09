using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class EquipPopup : MonoBehaviour
{
    private InventoryItem it;
    private InventoryItemUI itUI;
    public PowerShow powerShow;

    private Image equipImage;
    private Text nameText;
    private Text qualityText;
    private Text damageText;
    private Text hpText;
    private Text levelText;
    private Text powerText;
    private Text desText;
    private Text btnText;

    private Button closeButton;
    private Button equipButton;
    private Button levelUpButton;
    private bool isLeft = true;

    private void Awake()
    {
        equipImage = transform.Find("EquipBg/Picture").GetComponent<Image>();
        nameText = transform.Find("Name").GetComponent<Text>();
        qualityText = transform.Find("Quality/Value").GetComponent<Text>();
        damageText = transform.Find("Damage/Value").GetComponent<Text>();
        hpText = transform.Find("Health/Value").GetComponent<Text>();
        levelText = transform.Find("Level/Value").GetComponent<Text>();
        powerText = transform.Find("Power/Value").GetComponent<Text>();
        desText = transform.Find("Introduce").GetComponent<Text>();
        btnText = transform.Find("Equip/Text").GetComponent<Text>();
        closeButton = transform.Find("ButtonClose").GetComponent<Button>();
        equipButton = transform.Find("Equip").GetComponent<Button>();
        levelUpButton = transform.Find("LevelUp").GetComponent<Button>();

        closeButton.onClick.AddListener(onClose);
        equipButton.onClick.AddListener(onEquip);
        levelUpButton.onClick.AddListener(onLevelUp);
    }

    public void Show(InventoryItem it, InventoryItemUI itUI, bool isLeft = true)
    {
        gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        this.isLeft = isLeft;
        if (isLeft)
        {
            btnText.text = "卸下";
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 0);
        }
        else
        {
            btnText.text = "装备";
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        equipImage.sprite = Resources.Load("Equip/" + it.Inventory.ICON, typeof(Sprite)) as Sprite;
        nameText.text = it.Inventory.Name;
        qualityText.text = it.Inventory.Quality.ToString();
        damageText.text = it.Inventory.Damage.ToString();
        hpText.text = it.Inventory.HP.ToString();
        powerText.text = it.Inventory.Power.ToString();
        levelText.text = it.Inventory.Level.ToString();
        desText.text = it.Inventory.DES;
    }
    public void onClose()
    {
        clearObject();
        this.gameObject.SetActive(false);
        Knaspack._intance.disableSellBtn();
    }
    //穿上或者卸下装备
    public void onEquip()
    {
        int startValue = PlayerInfo._instance.GetOverAllPower();
        if (!isLeft)
        {
            itUI.Clear();
            PlayerInfo._instance.DressEquip(it);
            
        }
        else
        {
            PlayerInfo._instance.DressOff(it);
        }
        onClose();
        int endValue = PlayerInfo._instance.GetOverAllPower();
        powerShow.showPowerChange(startValue, endValue);
        InventoryUI._instance.updateValuetxt();       
    }
    public void onLevelUp()
    {
        int coinNeed = (it.Level + 1) * (it.Inventory.Price);
        bool isSuccess = PlayerInfo._instance.GetCoin(coinNeed);
        //成功
        if(isSuccess)
        {
            it.Level += 1;
            levelText.text = it.Level.ToString();
        }
        else
        {
            //给出提示信息
            MessageManager._instance.startCoroutine("金币不足");
        }
    }
    public void clearObject()
    {
        it = null;
        itUI = null;
    }
}
