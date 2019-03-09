using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hpslider : MonoBehaviour {
    private Slider uislider;
    public dogemove doge2;
    private void Awake()
    {
        uislider = this.GetComponent<Slider>();
    }
    void Update () {
        uislider.value = doge2.hp/100;
	}
}
