using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class normaGameMnager : MonoBehaviour {

    static public normaGameMnager _instance;
    public GameObject player;
    public GameObject doge;
    public GameObject bone;
    public AudioSource bgm;
    public GameObject[] cubes;
    public GameObject bloodBG;
    public AudioSource playerDead;
    private bool isDead=false;
    public int point = 0;
    private float sceneChangeTime = 0;
    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<dogemove>().hp <= 0)
        {
            death();
            isDead = true;
        }
        if (isDead)
        {
            sceneChangeTime +=Time.deltaTime;
        }

    }
    void death()
    {
        playerDead.enabled = true;
        bloodBG.SetActive(true);
        bgm.enabled = false;
        doge.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Manager>().enabled = false;
        Destroy(bone);
        foreach (GameObject cube in cubes)
        {
            Destroy(cube);
        }
        if (sceneChangeTime>=5) {
            SceneManager.LoadScene(4);
        }        
    }
}

