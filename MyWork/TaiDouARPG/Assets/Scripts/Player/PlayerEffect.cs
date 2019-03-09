using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    private Renderer[] rendererArray;
    private NcCurveAnimation[] curveAnimaArray;
	// Use this for initialization
	void Start () {
        rendererArray = this.GetComponentsInChildren<Renderer>();
        curveAnimaArray = this.GetComponentsInChildren<NcCurveAnimation>();
	}

    public void Show()
    {
        foreach(Renderer renderer in rendererArray)
        {
            renderer.enabled = true;
        }
        foreach(NcCurveAnimation anim in curveAnimaArray)
        {
            anim.ResetAnimation();
        }
    }
}
