using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatMap : MonoBehaviour {
    public GameObject[] level1Map;
    public GameObject[] level2Map;
    public GameObject[] level3Map;
    public GameObject[] level4Map;
    public GameObject[] level5Map;
    public GameObject[] level6Map;
	// Update is called once per frame
	void Update () {
		
	}
    private void creatMap(int level)
    {
        GameObject[] map;
        int num = 0;
        switch (level) {
            case 1:
                map = level1Map;
                num = Random.Range(0, map.Length);
                Instantiate(map[num]);
                break;
            case 2:
                map = level2Map;
                num = Random.Range(0, map.Length);
                Instantiate(map[num]);
                break;
            case 3:
                map = level3Map;
                num = Random.Range(0, map.Length);
                Instantiate(map[num]);
                break;
            case 4:
                map = level4Map;
                num = Random.Range(0, map.Length);
                Instantiate(map[num]);
                break;
            case 5:
                map = level5Map;
                num = Random.Range(0, map.Length);
                Instantiate(map[num]);
                break;
        }
    }
}
