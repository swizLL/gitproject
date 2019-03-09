using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfoType
{
    Name,
    HeadPortrait,
    Level,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Toughen,
    HP,
    Damage,
    Equip,
    All
}
public enum PlayerType
{
    //战士
    Warrior,
    //女刺客
    FemaleAssassin 
}
public class PlayerInfo : MonoBehaviour
{
    //姓名,头像,等级,战斗力 ,经验值 ,钻石数,金币数,体力值,历练值
    public static PlayerInfo _instance;

    public float energyTimer = 0;
    public float toughenTimer = 0;

    public delegate void OnPlayerInfoChangedEvent(InfoType type);
    public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;
    #region property
    private string _name;
    private string _headPortrait;
    private int _level = 1;
    private int _power = 1;
    private int _nowexp = 0;
    private int _maxexp = 100;
    private int _diamond;
    private int _coin;
    private int _jingshi;
    private int _nowenergy;
    private int _maxenergy;
    private int _nowtoughen;
    private int _maxtoughen;

    private int _hp;
    private int _damage;
    private PlayerType _playerType;
    //private int _helmid=0;
    //private int _clothid=0;
    //private int _weaponid=0;
    //private int _shoesid=0;
    //private int _necklaceid=0;
    //private int _braceletid=0;
    //private int _ringid=0;
    //private int _wingsid=0;

    public InventoryItem helmInvItem;
    public InventoryItem clothInvItem;
    public InventoryItem weaponInvItem;
    public InventoryItem shoesInvItem;
    public InventoryItem necklaceInvItem;
    public InventoryItem braceleInvItem;
    public InventoryItem ringInvItem;
    public InventoryItem wingsInvItem;
    #endregion
    #region get set method
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public string HeadPortrait
    {
        get
        {
            return _headPortrait;
        }
        set
        {
            _headPortrait = value;
        }
    }
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
    public int Power
    {
        get
        {
            return _power;
        }
        set
        {
            _power = value;
        }
    }
    public int nowExp
    {
        get
        {
            return _nowexp;
        }
        set
        {
            _nowexp = value;
        }
    }
    public int maxExp
    {
        get
        {
            return _maxexp;
        }
        set
        {
            _maxexp = value;
        }
    }
    public int Diamond
    {
        get
        {
            return _diamond;
        }
        set
        {
            _diamond = value;
        }
    }
    public int Coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
        }
    }
    public int Jingshi
    {
        get
        {
            return _jingshi;
        }
        set
        {
            _jingshi = value;
        }
    }
    public int nowEnergy
    {
        get
        {
            return _nowenergy;
        }
        set
        {
            _nowenergy = value;
        }
    }
    public int maxEnergy
    {
        get
        {
            return _maxenergy;
        }
        set
        {
            _maxenergy = value;
        }
    }
    public int nowToughen
    {
        get
        {
            return _nowtoughen;
        }
        set
        {
            _nowtoughen = value;
        }
    }
    public int maxToughen
    {
        get
        {
            return _maxtoughen;
        }
        set
        {
            _maxtoughen = value;
        }
    }
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    public PlayerType PlayerType
    {
        get
        {
            return _playerType;
        }

        set
        {
            _playerType = value;
        }
    }

    //public int HelmID
    //{
    //    get
    //    {
    //        return _helmid;
    //    }
    //    set
    //    {
    //        _helmid = value;
    //    }
    //}
    //public int ClothID
    //{
    //    get
    //    {
    //        return _clothid;
    //    }
    //    set
    //    {
    //        _clothid = value;
    //    }
    //}
    //public int WeaponID
    //{
    //    get
    //    {
    //        return _weaponid;
    //    }
    //    set
    //    {
    //        _weaponid = value;
    //    }
    //}
    //public int ShoesID
    //{
    //    get
    //    {
    //        return _shoesid;
    //    }
    //    set
    //    {
    //        _shoesid = value;
    //    }
    //}
    //public int NecklaceID
    //{
    //    get
    //    {
    //        return _necklaceid;
    //    }
    //    set
    //    {
    //        _necklaceid = value;
    //    }
    //}
    //public int BraceletID
    //{
    //    get
    //    {
    //        return _braceletid;
    //    }
    //    set
    //    {
    //        _braceletid = value;
    //    }
    //}
    //public int RingID
    //{
    //    get
    //    {
    //        return _ringid;
    //    }
    //    set
    //    {
    //        _ringid = value;
    //    }
    //}
    //public int WingsID
    //{
    //    get
    //    {
    //        return _wingsid;
    //    }
    //    set
    //    {
    //        _wingsid = value;
    //    }
    //}
    //private int _hp;
    //private int _damage;
    //private int _helmid;
    //private int _clothid;
    //private int _weaponid;
    //private int _shoesid;
    //private int _necklaceid;
    //private int _braceletid;
    //private int _ringid;
    //private int _wingsid;
    #endregion
    #region UnityBehaviour
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        //体力和历练的自动增长
        if (this.nowEnergy < maxEnergy)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer > 60)
            {
                nowEnergy += 1;
                energyTimer -= 60;
                OnPlayerInfoChanged(InfoType.Energy);
            }
        }
        else
        {
            energyTimer = 0;
        }
        if (this.nowToughen < maxToughen)
        {
            toughenTimer += Time.deltaTime;
            if (toughenTimer > 60)
            {
                nowToughen += 1;
                toughenTimer -= 60;
                OnPlayerInfoChanged(InfoType.Toughen);
            }
        }
        else
        {
            toughenTimer = 0;
        }
    }
    #endregion

    //初始化
    void Init()
    {
        this.Coin = 70000;
        this.Diamond = 7000;
        this.Jingshi = 666;
        this.nowEnergy = 0;
        this.Name = "风行云";
        this.HeadPortrait = "头像底板男性";
        this.nowEnergy = 70;
        this.maxEnergy = 100;
        this.nowToughen = 7;
        this.maxToughen = 50;
        this.Level = 20;

        //this.BraceletID = 1001;
        //this.WingsID = 1002;
        //this.RingID = 1003;
        //this.ClothID = 1004;
        //this.HelmID = 1005;
        //this.WeaponID = 1006;
        //this.NecklaceID = 1007;
        //this.ShoesID = 1008;

        InitHpDamagePower();
        OnPlayerInfoChanged(InfoType.All);
    }
    //使用物品
    public void inventoryUse(InventoryItem it,int count)
    {
        //使用效果
        //TODO
        //处理物体使用后是否还存在
        it.Count -= count;
        if (it.Count <= 0)
        {
            InventoryManager._instance.RemoveInventoryItem(it);
            InventoryUI._instance.updateValuetxt();
        }
    }
    //取得所需的金币个数
    public bool GetCoin(int count)
    {
        if (Coin >= count)
        {
            Coin -= count;
            OnPlayerInfoChanged(InfoType.Coin);
            return true;
        }
        else
            return false;
    }
    //增加金钱的方法
    public void AddCoin(int count)
    {
        Coin += count;
        OnPlayerInfoChanged(InfoType.Coin);
    }
    //得到综合战斗力
    public int GetOverAllPower()
    {
        float power = this.Power;
        if (helmInvItem != null)
        {
            power += helmInvItem.Inventory.Power * (1 + (helmInvItem.Level-1)/10f);
        }
        if (clothInvItem!= null)
        {
            power += clothInvItem.Inventory.Power * (1 + (clothInvItem.Level - 1) / 10f);
        }
        if (weaponInvItem != null)
        {
            power += weaponInvItem.Inventory.Power * (1 + (weaponInvItem.Level - 1) / 10f);
        }
        if (shoesInvItem != null)
        {
            power += shoesInvItem.Inventory.Power * (1 + (shoesInvItem.Level - 1) / 10f);
        }
        if (necklaceInvItem != null)
        {
            power += necklaceInvItem.Inventory.Power * (1 + (necklaceInvItem.Level - 1) / 10f);
        }
        if (braceleInvItem != null)
        {
            power += braceleInvItem.Inventory.Power * (1 + (braceleInvItem.Level - 1) / 10f);
        }
        if (ringInvItem != null)
        {
            power += ringInvItem.Inventory.Power * (1 + (ringInvItem.Level - 1) / 10f);
        }
        if (wingsInvItem!= null)
        {
            power += wingsInvItem.Inventory.Power * (1 + (wingsInvItem.Level - 1) / 10f);
        }
        return (int)power;
    }
    //名字改变
    public void ChangeName(string newName)
    {
        this.Name = newName;
        OnPlayerInfoChanged(InfoType.Name);
    }
    //初始化生命值等信息
    public void InitHpDamagePower()
    {
        this.HP = this.Level * 100;
        this.Damage = this.Level * 50;
        this.Power = this.HP + this.Damage;
    }
    //背包里的穿上事件
    public void DressEquip(InventoryItem it)
    {
        it.IsDressed = true;
        //首先检测有没有穿上相同类型的装备
        bool isDressed = false;
        InventoryItem inventoryItemDressed = null;
        switch (it.Inventory.EquipType)
        {
            case EquipType.Bracelet:
                if (braceleInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = braceleInvItem;
                }
                braceleInvItem = it;
                break;
            case EquipType.Cloth:
                if (clothInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = clothInvItem;                    
                }
                clothInvItem = it;
                break;
            case EquipType.Helm:
                if (helmInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = helmInvItem;                   
                }
                helmInvItem = it;
                break;
            case EquipType.Necklace:
                if (necklaceInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = necklaceInvItem;                   
                }
                necklaceInvItem = it;
                break;
            case EquipType.Ring:
                if (ringInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = ringInvItem;                   
                }
                ringInvItem = it;
                break;
            case EquipType.Shoes:
                if (shoesInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = shoesInvItem;                   
                }
                shoesInvItem = it;
                break;
            case EquipType.Weapon:
                if (weaponInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = weaponInvItem;                  
                }
                weaponInvItem = it;
                break;
            case EquipType.Wings:
                if (wingsInvItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = wingsInvItem;                   
                }
                wingsInvItem = it;
                break;
        }
        //有
        //把已经穿上的装备脱掉，放入背包
        if(isDressed)
        {
            inventoryItemDressed.IsDressed = false;
            InventoryUI._instance.AddInventoryItem(inventoryItemDressed);
        }
        OnPlayerInfoChanged(InfoType.Equip);
        //没有就直接穿上

    }
    //卸下装备
    public void DressOff(InventoryItem it)
    {
        switch (it.Inventory.EquipType)
        {
            case EquipType.Bracelet:
                if (braceleInvItem != null)
                {
                    braceleInvItem = null;
                }
                break;
            case EquipType.Cloth:
                if (clothInvItem != null)
                {
                    clothInvItem = null;
                }
                break;
            case EquipType.Helm:
                if (helmInvItem != null)
                {
                    helmInvItem = null;
                }
                break;
            case EquipType.Necklace:
                if (necklaceInvItem != null)
                {
                    necklaceInvItem = null;
                }
                break;
            case EquipType.Ring:
                if (ringInvItem != null)
                {
                    ringInvItem = null;
                }
                break;
            case EquipType.Shoes:
                if (shoesInvItem != null)
                {
                    shoesInvItem = null;
                }
                break;
            case EquipType.Weapon:
                if (weaponInvItem != null)
                {
                    weaponInvItem = null;
                }
                break;
            case EquipType.Wings:
                if (wingsInvItem != null)
                {
                    wingsInvItem = null;
                }
                break;
        }
        it.IsDressed = false;
        InventoryUI._instance.AddInventoryItem(it);
        OnPlayerInfoChanged(InfoType.Equip);
    }
    //根据id穿上这装备
    public void PutonEquip(int id)
    {
        if (id == 0) return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.HP += inventory.HP;
        this.Damage += inventory.Damage;
        this.Power += inventory.Power;
    }
    //卸下装备
    public void PutoffEquiq(int id)
    {
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.HP -= inventory.HP;
        this.Damage -= inventory.Damage;
        this.Power -= inventory.Power;
    }

}
