using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour {
    private Text coinNumText;
    private Text diamonNumText;
    private Button addCoinBtn;
    private Button addDiamonBtn;

    private void Awake()
    {
        coinNumText = transform.Find("CoinBar/Value").GetComponent<Text>();
        diamonNumText = transform.Find("DiamonBar/Value").GetComponent<Text>();
        addCoinBtn = transform.Find("AddCoin").GetComponent<Button>();
        addDiamonBtn = transform.Find("AddDiamon").GetComponent<Button>();
        //将事件注册上去
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }
    private void OnDestroy()
    {
        //当物体被销毁时，事件取消注册
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.All || type == InfoType.Coin || type == InfoType.Diamond)
        {
            UpdateBar();
        }
    }
    void UpdateBar()
    {
        PlayerInfo info = PlayerInfo._instance;
        coinNumText.text = info.Coin.ToString();
        diamonNumText.text = info.Diamond.ToString();
    }
}
