using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    private Renderer[] rendererArray;
    private NcCurveAnimation[] curveAnimaArray;
    private GameObject effectOffset;
    // Use this for initialization
    void Start()
    {
        rendererArray = this.GetComponentsInChildren<Renderer>();
        curveAnimaArray = this.GetComponentsInChildren<NcCurveAnimation>();
        if (transform.Find("EffectOffset")!= null)
        {
            effectOffset = transform.Find("EffectOffset").gameObject;
        }
        
    }

    public void Show()
    {
        if (effectOffset != null)
        {
            //先隐藏后显示，使粒子系统进行播放
            effectOffset.SetActive(false);
            effectOffset.SetActive(true);
        }
        else
        {
            foreach (Renderer renderer in rendererArray)
            {
                renderer.enabled = true;
            }
            foreach (NcCurveAnimation anim in curveAnimaArray)
            {
                anim.ResetAnimation();
            }
        }
    }
}
