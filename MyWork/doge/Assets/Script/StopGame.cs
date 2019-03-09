using UnityEngine;
using System.Collections;

public class StopGame : MonoBehaviour {

    public GameObject stopUI;
    public  void stop()
    {
        Time.timeScale = 0;
        stopUI.SetActive(true);
    }
    public void goAhead()
    {
        Time.timeScale = 1;
        stopUI.SetActive(false);
    }
}
