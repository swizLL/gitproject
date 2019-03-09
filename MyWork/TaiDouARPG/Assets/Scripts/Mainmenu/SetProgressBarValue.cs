using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetProgressBarValue : MonoBehaviour
{
    public Slider slider;
    private void Update()
    {
        gameObject.GetComponent<Text>().text = ((slider.value)*100).ToString()+"%";
    }
}
