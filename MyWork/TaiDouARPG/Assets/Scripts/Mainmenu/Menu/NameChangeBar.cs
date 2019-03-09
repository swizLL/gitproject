using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameChangeBar : MonoBehaviour {
    private InputField nameInput;
    private void Update()
    {
        nameInput = transform.Find("NameInput").GetComponent<InputField>();
    }
    public void Cancel()
    {
        this.gameObject.SetActive(false);
        nameInput.text = "";
    }
    public void Ensure()
    {
        //校验名字是否合法
        //TODO
        if (nameInput.text != "")
        {
            PlayerInfo._instance.ChangeName(nameInput.text);
            this.gameObject.SetActive(false);
            nameInput.text = "";
        }
        else
        {
            Debug.Log("输入名字不能为空！");
        }
    }
}
