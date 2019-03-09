using UnityEngine;
using System.Collections;
using GDGeek;

public class Play : MonoBehaviour {
    public Square[] squareList=null;
    public Square photoType;
	// Use this for initialization
	void Awake () {
        foreach(Square square in squareList)
        {
            square.hide();
        }
	}
   public Task moveTask(int number,Vector2 beginPos,Vector2 endPos)
   {
        Square s = (Square)GameObject.Instantiate(photoType);
        Square b = this.getSquare((int)(beginPos.x), (int)(beginPos.y));
        Square e = this.getSquare((int)(endPos.x), (int)(endPos.y));

        s.transform.parent = b.transform.parent;
        s.transform.localScale = b.transform.localScale;
        s.transform.localPosition = b.transform.localPosition;
        s.show();
        s.number = number;
        b.hide();
        TweenTask tt = new TweenTask(delegate () {
            return TweenLocalPosition.Begin(s.gameObject, 0.3f, e.transform.localPosition);
        });

        TaskManager.PushBack(tt, delegate {
            GameObject.DestroyObject(s.gameObject);
        });
        return tt;
    }
    public Square getSquare(int x,int y)
    {
        int n = x + y * 4;
        return squareList[n] ;
    }	
}
