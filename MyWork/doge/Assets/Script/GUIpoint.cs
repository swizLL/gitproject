using UnityEngine;
using System.Collections;

public class GUIpoint : MonoBehaviour {

	void OnGUI()
    {
        GUIStyle pointStyle = new GUIStyle();
        pointStyle.fontSize =70;
        pointStyle.normal.textColor = new Color(250,255,0,255);
       GUILayout.Box("Points:" + Manager.m.point,pointStyle);
    }
}
