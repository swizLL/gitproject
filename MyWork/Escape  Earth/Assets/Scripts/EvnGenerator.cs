using UnityEngine;
using System.Collections;
using System.Text;

public class EvnGenerator : MonoBehaviour {

    public Forest forest1;
    public Forest forest2;
    public GameObject[] forests;

    public void Generate()
    {
        int i = Random.Range(0, 3);
        forest1 = forest2;
        GameObject newforest = GameObject.Instantiate(forests[i], new Vector3(0, 0, forest1.transform.position.z +3000), Quaternion.identity)as GameObject;
        forest2 = newforest.GetComponent<Forest>();
    }
}
