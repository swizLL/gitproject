using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcDialogUI : MonoBehaviour {
    public static NpcDialogUI _instance;
    private Text npcTalkText;
    private Button acceptButton;
    private Button closeButton;
    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start () {
        npcTalkText = transform.Find("Talk").GetComponent<Text>();
        acceptButton = transform.Find("acceptButton").GetComponent<Button>();
        acceptButton.onClick.AddListener(onAccept);
        closeButton = transform.Find("closeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(onClose);
    }
	public void onShow(string npcTalk)
    {
        npcTalkText.text = npcTalk;
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -280, 0);
    }
    public void onAccept()
    {
        //通知任务管理器已经接受了任务
        TaskManager._instance.onAcceptTask();
        onClose();
    }
    public void onClose()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(1200, -280, 0);
    }
}
