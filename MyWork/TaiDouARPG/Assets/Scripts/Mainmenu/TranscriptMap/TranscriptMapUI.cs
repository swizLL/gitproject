using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptMapUI : MonoBehaviour {
    public static TranscriptMapUI _instance;

    private TranscriptDialog dialog;
    private void Awake()
    {
        _instance = this;
        dialog = transform.Find("transcripDialog").GetComponent<TranscriptDialog>();
    }
    public void Show()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }
    public void Close()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1200, 0);
    }
    public void OnTrstBtnClick(btnTranscript transcript)
    {
        if (PlayerInfo._instance.Level >= transcript.needLevel)
        {
            dialog.showDes(transcript);
        }
        else
        {
            dialog.showWarn();
        }
    }
    public void onEnter()
    {
        //TODO
        //进入场景
    }
}
