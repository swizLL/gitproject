using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class btnTranscript : MonoBehaviour {
    public int id;
    public int needLevel;
    public string sceneName;
    public string Des = "这里是一个无主之地，你愿意来称霸这个地方吗？";

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void onClick()
    {
        //TODO
        TranscriptMapUI._instance.OnTrstBtnClick(this);
    }

}
