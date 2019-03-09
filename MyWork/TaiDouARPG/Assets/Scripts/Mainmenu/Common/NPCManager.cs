using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
    public static NPCManager _instance; 
    public GameObject[] npcArray;
    private Dictionary<int, GameObject> npcDict = new Dictionary<int, GameObject>();
    public GameObject transcriptGo;//副本的位置
    private void Awake()
    {
        _instance = this;
        Init();
    }
    public void Init()
    {
        foreach(GameObject go in npcArray)
        {
            int id = int.Parse(go.name.Substring(0, 4));
            npcDict.Add(id, go);
        }
    }
    public GameObject getNpcById(int id)
    {
        GameObject obj = null;
        npcDict.TryGetValue(id, out obj);
        return obj;
    }
}
