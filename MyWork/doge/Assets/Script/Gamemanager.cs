using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour {
    static public Gamemanager _instance;
    public GameObject player;
    public GameObject blood;
    public GameObject doge;
    public GameObject bone;
    public GameObject[] cubes;
    public AudioSource bgm;
    public AudioSource playerdead;
    public GameObject bloodBG;
    private bool isDead = false;
    private float sceneChangeTime = 0;
	void Awake () {
        _instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<dogemove>().hp <= 0)
        {
            death();
            isDead = true;
        }
        if (isDead)
        {
            sceneChangeTime += Time.deltaTime;
        }

    }
    void death()
    {
        playerdead.enabled = true;
        bloodBG.SetActive(true);
        bgm.enabled = false;
        doge.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Manager>().enabled = false;
        Destroy(bone);
        foreach(GameObject cube in cubes)
        {
            Destroy(cube);
        }
        if (sceneChangeTime>=5)
        {
            SceneManager.LoadScene(2);
        }
        
    }
}
