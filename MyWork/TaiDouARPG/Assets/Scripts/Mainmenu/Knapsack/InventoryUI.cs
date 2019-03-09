using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour {
    public static InventoryUI _instance;
    public List<InventoryItemUI> itemUIList = new List<InventoryItemUI>();

    private Text nowValuetxt;
    private Text maxValuetxt;
    private Button tidyButton;

    private int nowItCount=0;
    private void Awake()
    {
        _instance = this;
        InventoryManager._instance.OnInventoryChange += this.OnInventoryChange;
        tidyButton = transform.Find("Tidy").GetComponent<Button>();

        nowValuetxt = transform.Find("itemValue/NowValue").GetComponent<Text>();
        maxValuetxt = transform.Find("itemValue/MaxValue").GetComponent<Text>();

        tidyButton.onClick.AddListener(tidyKnapsack);
    }
    private void OnDestroy()
    {
        InventoryManager._instance.OnInventoryChange -= this.OnInventoryChange;
    }
    private void OnInventoryChange()
    {
        UpdateShow();
    }
    private void UpdateShow()
    {
        int temp = 0;
        for(int i=0;i<InventoryManager._instance.inventoryItemList.Count;++i)
        {
            InventoryItem it = InventoryManager._instance.inventoryItemList[i];
            if(it.IsDressed==false)
            {
                itemUIList[temp].setInventoryItem(it);
                temp++;
            }
        }        
        for(int i=temp;i<itemUIList.Count;++i)
        {
            itemUIList[i].Clear();
        }
        updateValuetxt();
    }
    public void AddInventoryItem(InventoryItem it)
    {
        //遍历所有的背包，找到空的背包
        foreach(InventoryItemUI itUI in itemUIList)
        {
            if (itUI.it == null)
            {
                itUI.setInventoryItem(it);
                break;
            }
        }
        updateValuetxt();
    }
    public void tidyKnapsack()
    {
        UpdateShow();
    }
    //改变背包里面物品数量的文本
    public void updateValuetxt()
    {
        nowItCount = 0;
        foreach (InventoryItemUI itUI in itemUIList)
        {
            if (itUI.it != null)
            {
                nowItCount++;
            }
        }
        nowValuetxt.text = nowItCount.ToString();
        maxValuetxt.text = itemUIList.Count.ToString();
    }
}
