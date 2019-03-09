using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour {
    private Text nameText;
    private Image inventoryImage;
    private Text desText;
    private Text lotbtnText;
    private InventoryItem it;
    private InventoryItemUI itUI;

    private Button closebtn;
    private Button usebtn;
    private Button uselotbtn;

    private void Awake()
    {
        nameText = transform.Find("NameText").GetComponent<Text>();
        inventoryImage = transform.Find("ptBg/picture").GetComponent<Image>();
        desText = transform.Find("itdBg/Text").GetComponent<Text>();
        lotbtnText = transform.Find("Usealot/Text").GetComponent<Text>();
        closebtn = transform.Find("ButtonClose").GetComponent<Button>();
        usebtn = transform.Find("Use").GetComponent<Button>();
        uselotbtn = transform.Find("Usealot").GetComponent<Button>();

        closebtn.onClick.AddListener(onClose);
        usebtn.onClick.AddListener(onUse);
        uselotbtn.onClick.AddListener(onUselot);
    }

    public void Show(InventoryItem it,InventoryItemUI itUI)
    {
        gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        nameText.text = it.Inventory.Name;
        inventoryImage.sprite= Resources.Load("Equip/" + it.Inventory.ICON, typeof(Sprite)) as Sprite;
        desText.text = it.Inventory.DES;
        lotbtnText.text="批量使用("+it.Count+")";
    }
    public void onClose()
    {
        it = null;
        itUI = null;
        this.gameObject.SetActive(false);
        Knaspack._intance.disableSellBtn();
    }
    public void onUse()
    {
        itUI.changeCount(1);
        PlayerInfo._instance.inventoryUse(it, 1);
        onClose();
    }
    public void onUselot()
    {
        itUI.changeCount(it.Count);
        PlayerInfo._instance.inventoryUse(it, it.Count);
        onClose();
    }

}
