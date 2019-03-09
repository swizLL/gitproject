using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knaspack : MonoBehaviour
{
    public static Knaspack _intance;
    private EquipPopup equipPopup;
    private InventoryPopup inventoryPopup;
    private InventoryItemUI itUI;

    private Button sellBtn;
    private Button closeBtn;
    private Text sellPriceTxt;

    private void Awake()
    {
        _intance = this;
        equipPopup = transform.Find("EquipPopUP").GetComponent<EquipPopup>();
        inventoryPopup = transform.Find("InventoryPopup").GetComponent<InventoryPopup>();
        
        sellBtn = transform.Find("Inventory/Sell").GetComponent<Button>();
        closeBtn = transform.Find("ButtonClose").GetComponent<Button>();
        sellPriceTxt = transform.Find("Inventory/Price/Value").GetComponent<Text>();
        sellBtn.onClick.AddListener(onSell);
        closeBtn.onClick.AddListener(onClose);

        disableSellBtn();
    }
    public void onInventoryClick(object[] objArray)
    {
        InventoryItem it = objArray[0] as InventoryItem;
        bool isLeft = (bool)objArray[1];
        if (it.Inventory.InventoryType == InventoryType.Equip)
        {
            inventoryPopup.onClose();
            if (isLeft == false)
            {
                itUI = objArray[2] as InventoryItemUI;
                enableSellBtn();
                sellPriceTxt.text = (itUI.it.Inventory.Price * itUI.it.Count).ToString();
            }           
            equipPopup.Show(it, itUI, isLeft); 
        }
        else
        {
            itUI = objArray[2] as InventoryItemUI;
            equipPopup.onClose();
            inventoryPopup.Show(it, itUI);
            enableSellBtn();
            sellPriceTxt.text = (itUI.it.Inventory.Price * itUI.it.Count).ToString();
        }
    }
    public void disableSellBtn()
    {
        sellBtn.interactable = false;
        sellPriceTxt.text = "";
    }
    public void enableSellBtn()
    {
        sellBtn.interactable = true;
    }
    public void onSell()
    {
        int price = int.Parse(sellPriceTxt.text);
        PlayerInfo._instance.AddCoin(price);
        InventoryManager._instance.RemoveInventoryItem(itUI.it);
        itUI.Clear();
        equipPopup.onClose();
        inventoryPopup.onClose();
    }
    public void onClose()
    {
        this.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000, 0);
    }
    public void onOpen()
    {
        this.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 0);
    }
}
