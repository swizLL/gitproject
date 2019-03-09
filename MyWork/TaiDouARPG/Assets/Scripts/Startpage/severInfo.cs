using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class severInfo : MonoBehaviour {

    private OnButtonPress obp;
    public string IP = "192.168.1.23";
    private string _name;
   
    public string name
    {
        set
        {
            transform.Find("Text").GetComponent<Text>().text = value;
            _name = value;
        }
        get
        {
            return _name;
        }
    }
    public int userNum;
    public void OnPress()
    {
        obp = UIController._instance.hasSelecSever.GetComponent<OnButtonPress>();
        obp.severSelect(this.gameObject);
        obp.isSeverPress = true;
    }
}