using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TranscriptDialog : MonoBehaviour {
    private Text desText;
    private Text energyTagText;
    private Text energyText;
    private Button enterButton;
    private Button closeButton;

    private void Start()
    {
        desText = transform.Find("DesBG/Des").GetComponent<Text>();
        energyTagText = transform.Find("DesBG/Energy").GetComponent<Text>();
        energyText = transform.Find("DesBG/Energy/Value").GetComponent<Text>();
        enterButton = transform.Find("enterButton").GetComponent<Button>();
        enterButton.onClick.AddListener(onEnter);
        closeButton = transform.Find("ButtonClose").GetComponent<Button>();
        closeButton.onClick.AddListener(onClose);
    }
    public void showWarn()
    {
        energyTagText.enabled = false;
        energyText.enabled = false;
        enterButton.interactable = false;
        desText.text = "当前等级无法进入该地下城";
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }
    public void showDes(btnTranscript transcriptBtn)
    {
        energyTagText.enabled = true;
        energyText.enabled = true;
        enterButton.interactable = true;

        desText.text = transcriptBtn.Des;
        energyText.text = "3";
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }
    void onEnter()
    {
        TranscriptMapUI._instance.onEnter();
    }
    void onClose()
    {
        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(1300, 0, 0);
    }
}
