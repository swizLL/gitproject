using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Endpoint : MonoBehaviour {

    private void Awake()
    {
        this.GetComponent<Text>().text = Manager.m.point.ToString();
    }
}
