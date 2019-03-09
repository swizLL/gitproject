using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadSceneProgressBar : MonoBehaviour {
    public static LoadSceneProgressBar _instance;
    private GameObject bg;
    private Slider progressBar;
    private bool isAsyn = false;
    private AsyncOperation Ao=null;
    private void Awake()
    {
        _instance = this;
        bg = transform.Find("BG").gameObject;
        progressBar = transform.Find("BG/ProgressBar").GetComponent<Slider>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(isAsyn)
        {
            progressBar.value = Ao.progress;
        }
    }
    public void Show(AsyncOperation ao)//异步加载场景的进度
    {
        gameObject.SetActive(true);
        bg.SetActive(true);
        isAsyn = true;
        this.Ao = ao;
    }
}
