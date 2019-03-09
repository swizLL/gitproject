using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class KnaspackRoleEquip : MonoBehaviour
{
    public Image _image;
    private InventoryItem it;
    private InventoryItemUI itUI;
    private void Awake()
    {
        Button btn = (Button)this.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    private Image Image
    {
        get
        {
            if (_image == null)
            {
                _image = this.GetComponent<Image>();
            }
            return _image;
        }
    }
    public void SetID(int id)
    {
        Inventory inventory = new Inventory();
        bool isExit = InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        if (isExit)
        {
            Image.sprite = Resources.Load("Equip/" + inventory.ICON, typeof(Sprite)) as Sprite;
        }
    }

    public void SetInventoryItem(InventoryItem it)
    {
        this.it = it;
        if (it != null)
        {
            Image.sprite = Resources.Load("Equip/" + it.Inventory.ICON, typeof(Sprite)) as Sprite;
        }
        else
        {
            Image.sprite = Resources.Load("UI/bg_道具", typeof(Sprite)) as Sprite;
        }
        
    }

    public void onClick()
    {
        if (it != null)
        {
            object[] objArray = new object[2];
            objArray[0] = it;
            objArray[1] = true;
            Knaspack._intance.onInventoryClick(objArray);
        }
    }
}
