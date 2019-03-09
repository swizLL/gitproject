using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class changescene : MonoBehaviour {

    public GameObject chooseObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void changeToChoose()
    {
        chooseObj.SetActive(true);
    }
    public void Sceneschange(){
		SceneManager.LoadScene (1);
        Time.timeScale = 1;
     }
    public void normalScencechange()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void changeToAREnd()
    {
        SceneManager.LoadScene(2);
    }
    public void changeToNomalEnd()
    {
        SceneManager.LoadScene(4);
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
