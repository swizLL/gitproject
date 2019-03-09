using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMove : MonoBehaviour {

    public RectTransform targetPos;
    public int moveSpeed=1;
    // Update is called once per frame
    void Update () {
        move();
    }
    private void move()
    {
        //不能用“==”判断，应为是每一帧加上速度，所以不一定能正好等于800；
        if (Mathf.Abs(targetPos.anchoredPosition.x - this.GetComponent<RectTransform>().anchoredPosition.x) < 10)
        {
            //将云朵旋转一下，沿y轴旋转180度；
            this.GetComponent<RectTransform>().Rotate(0, 180, 0);
            //使云朵反向运动
            moveSpeed = -moveSpeed;
            targetPos.anchoredPosition = new Vector2(-targetPos.anchoredPosition.x, targetPos.anchoredPosition.y);
        }
        this.GetComponent<RectTransform>().anchoredPosition += new Vector2(moveSpeed * Time.deltaTime*10, 0);   
    }
}
