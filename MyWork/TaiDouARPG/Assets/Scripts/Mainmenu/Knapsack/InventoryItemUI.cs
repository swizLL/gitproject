using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItemUI : MonoBehaviour
{
    private Image picture;
    private Text text;
    public InventoryItem it;
    private void Awake()
    {
        Button btn = (Button)this.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    private Image Picture
    {
        get
        {
            if (picture == null)
            {
                picture = transform.Find("picture").GetComponent<Image>();
            }
            return picture;
        }
    }
    private Text Text
    {
        get
        {
            if (text == null)
            {
                text = transform.Find("Text").GetComponent<Text>();
            }
            return text;
        }
    }
    public void setInventoryItem(InventoryItem it)
    {
        this.it = it;
        Picture.sprite = Resources.Load("Equip/" + it.Inventory.ICON, typeof(Sprite)) as Sprite;
        if (it.Count <= 1)
        {
            Text.text = "";
        }
        else
        {
            Text.text = it.Count.ToString();
        }

    }
    public void Clear()
    {
        it = null;
        Text.text = "";
        Picture.sprite = Resources.Load("UI/bg_道具", typeof(Sprite)) as Sprite;
    }
    public void onClick()
    {
        if (it != null)
        {
            object[] objArray = new object[3];
            objArray[0] = it;
            objArray[1] = false;
            objArray[2] = this;
            Knaspack._intance.onInventoryClick(objArray);
        }
    }
    public void changeCount(int count)
    {
        if (it.Count - count <= 0)
        {
            Clear();
        }
        else if (it.Count - count == 1)
        {
            Text.text = "";
        }
        else
        {
            Text.text = (it.Count - count).ToString();
        }
    }
}
