using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class View : MonoBehaviour {

    public GameObject begin = null;
    public Play play = null;
    public GameObject end = null;
    public Text score;
    public Text endScore;
    public int _score
    {
        set
        {
            score.text = value.ToString();
        }
    }
    public int _endScore
    {
        set
        {
            endScore.text = value.ToString();
        }
    }
}
