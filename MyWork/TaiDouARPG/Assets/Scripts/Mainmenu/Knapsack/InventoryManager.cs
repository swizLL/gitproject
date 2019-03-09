using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;
    public TextAsset inventoryInfo;
    public Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();
    //public Dictionary<int, InventoryItem> inventoryItemDicet = new Dictionary<int, InventoryItem>();
    //自己持有的物品清单
    public List<InventoryItem> inventoryItemList = new List<InventoryItem>();

    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChange;
    private void Awake()
    {
        _instance = this;
         ReadInventoryInfo();
    }
    private void Start()
    {
        InitInventoryItem();    
    }
    void ReadInventoryInfo()
    {
        string infoTxt = inventoryInfo.ToString();
        string[] strArray = infoTxt.Split('\n');///每一行数据的数组
        foreach (string str in strArray)
        {
            string[] infoArray = str.Split('|');
            //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm, Cloth, Weapon, Shoes, Necklace, Bracelet, Ring, Wing) 售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            Inventory inventory = new Inventory();
            inventory.ID = int.Parse(infoArray[0]);
            inventory.Name = infoArray[1];
            inventory.ICON = infoArray[2];
            switch (infoArray[3])
            {
                case "Equip":
                    inventory.InventoryType = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.InventoryType = InventoryType.Drug;
                    break;
                case "Box":
                    inventory.InventoryType = InventoryType.Box;
                    break;
            }
            inventory.Price = int.Parse(infoArray[5]);
            if (inventory.InventoryType == InventoryType.Equip)
            {
                switch (infoArray[4])
                {
                    case "Helm":
                        inventory.EquipType = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.EquipType = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.EquipType = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.EquipType = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.EquipType = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.EquipType = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.EquipType = EquipType.Ring;
                        break;
                    case "Wing":
                        inventory.EquipType = EquipType.Wings;
                        break;
                }
                inventory.StarLevel = int.Parse(infoArray[6]);
                inventory.Quality = int.Parse(infoArray[7]);
                inventory.Damage = int.Parse(infoArray[8]);
                inventory.HP = int.Parse(infoArray[9]);
                inventory.Power = int.Parse(infoArray[10]);
            }
            if (inventory.InventoryType == InventoryType.Drug)
            {
                switch (infoArray[11])
                {
                    case "Energy":
                        inventory.InfoType = InfoType.Energy;
                        break;
                    case "Toughen":
                        inventory.InfoType = InfoType.Toughen;
                        break;
                }
                inventory.ApplyValue = int.Parse(infoArray[12]);
            }
            inventory.DES = infoArray[13];
            inventoryDict.Add(inventory.ID, inventory);
        }
    }
    //初始化背包物品
    void InitInventoryItem()
    {
        //TODO连接服务器获取玩家背包里的物品
        //随机生成背包物品
        for (int i = 0; i < 10; i++)
        {
            int id = Random.Range(1001, 1019);
            //Debug.Log(id);
            Inventory inv = null;
            inventoryDict.TryGetValue(id, out inv);
            //判断生成的物品是否为武器类型，如果是武器，则直接加入物品清单即可
            if (inv.InventoryType == InventoryType.Equip)
            {
                InventoryItem it = new InventoryItem();
                it.Inventory = inv;
                it.Level = Random.Range(1, 10);
                it.Count = 1;
                inventoryItemList.Add(it);
            }
            //若不是武器，则判断物品是否在清单里已经存在，如果存在，则只需要将此物品的数量加一即可，如果不存在，则将此物品加入清单
            else
            {
                InventoryItem itm = null;
                bool itemExit = false;
                foreach (InventoryItem temp in inventoryItemList)
                {
                    if (temp.Inventory.ID == id)
                    {
                        itemExit = true;
                        itm = temp;
                        break;
                    }
                }
                if (itemExit)
                {
                    itm.Count++;
                }
                else
                {
                    itm = new InventoryItem();
                    itm.Inventory = inv;
                    itm.Count = 1;
                    inventoryItemList.Add(itm);
                }
            }
        }
        OnInventoryChange();
    }
    //移除一个背包中的物体
    public void RemoveInventoryItem(InventoryItem it)
    {
        this.inventoryItemList.Remove(it);
    }
}
