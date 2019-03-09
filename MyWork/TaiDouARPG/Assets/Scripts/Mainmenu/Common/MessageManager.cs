using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageManager : MonoBehaviour {

    //使用协程来实现文本出现一定时间的效果
    // Use this for initialization
    public static MessageManager _instance;
    private Text messageText;
    private void Awake()
    {
        _instance = this;
        messageText = transform.Find("Text").GetComponent<Text>();
        gameObject.SetActive(false);
        messageText.gameObject.SetActive(true);
    }
    public void startCoroutine(string message,float time=2)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(ShowMessge(message, time));
    }
    public IEnumerator ShowMessge(string message,float time)
    {
        messageText.text = message;
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
