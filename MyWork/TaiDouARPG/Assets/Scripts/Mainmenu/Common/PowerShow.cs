using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerShow : MonoBehaviour
{
    private Text powerText;
    private float startValue = 0;
    private int endValue = 1000;
    private int speed = 1000;
    private bool isStart = false;
    private bool isUp = true;
    // Update is called once per frame
    private void Awake()
    {
        powerText = transform.Find("Text").GetComponent<Text>();
        powerText.text = "战斗力：";
        this.gameObject.SetActive(false);
        powerText.gameObject.SetActive(true);
    }
    void Update()
    {
        if (isStart)
        {
            if (isUp)
            {
                startValue += Time.deltaTime * speed;
                if (startValue > endValue)
                {
                    isStart = false;
                    startValue = endValue;
                    StartCoroutine(Wait(1));

                }
            }
            else
            {
                startValue -= Time.deltaTime * speed;
                if (startValue < endValue)
                {
                    isStart = false;
                    startValue = endValue;
                    StartCoroutine(Wait(1));
                }

            }
        }
        powerText.text = "战斗力：" + (int)startValue;
    }
    public void showPowerChange(int startValue, int endValue)
    {
        gameObject.SetActive(true);
        this.startValue = startValue;
        this.endValue = endValue;
        if (startValue < endValue)
            isUp = true;
        else
            isUp = false;
        isStart = true;
    }
    IEnumerator Wait(int time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
