using UnityEngine;
using System.Collections;
public enum TouchDir
{
    Right,
    Left,
    Up,
    Down,
    None
}
public class playerMove : MonoBehaviour
{
    public float speed;
    public float moveHorizontalSpeed = 6;
    public int nowLineIndex = 1;
    public int targetLineIndex = 1;
    public float slideTime = 0.7f;
    public float jumpHeight = 25;
    public float jumpSpeed = 60;
    public bool isSliding = false;
    public bool isJumping = false;

    private float haveJumpHeight;
    private bool isUp = true;
    private EvnGenerator evnGenerator;
    private float slideTimer = 0;
    private Vector3 MouseDownPos = Vector3.zero;
    private Vector3 MouseUpPos;
    private float moveHorizontal = 0;
    private int[] xOffset = new int[] { -14, 0, 14 };
    private Transform prisoner;
    // Use this for initialization
    void Awake()
    {
        evnGenerator = Camera.main.GetComponent<EvnGenerator>();
        prisoner = this.transform.Find("Prisoner").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.gamestate == GameController.Gamestate.Playing)
        {
            Vector3 targetPos = evnGenerator.forest1.getNextTargetPoint();
            targetPos = new Vector3(targetPos.x + xOffset[targetLineIndex], targetPos.y, targetPos.z);
            Vector3 moveDir = targetPos - transform.position;
            transform.position += moveDir.normalized * speed * Time.deltaTime;
        }
        MoveControl();
    }
    private void MoveControl()
    {
        TouchDir dir = GetTouchDir();
        if (nowLineIndex != targetLineIndex)
        {
            float moveLengh = Mathf.Lerp(0, moveHorizontal, moveHorizontalSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + moveLengh, transform.position.y, transform.position.z);
            moveHorizontal -= moveLengh;
            if (Mathf.Abs(moveHorizontal) < 0.5f)
            {
                transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
                moveHorizontal = 0;
                nowLineIndex = targetLineIndex;
            }
        }
        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer >= slideTime)
            {
                slideTimer = 0;
                isSliding = false;
            }
        }
        if (isJumping)
        {
            float yMove = jumpSpeed * Time.deltaTime;
            if (isUp)
            {
                prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y + yMove, prisoner.position.z);
                haveJumpHeight += yMove;
                if (Mathf.Abs(jumpHeight - haveJumpHeight) < 0.5f)
                {
                    prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y + jumpHeight - haveJumpHeight, prisoner.position.z);
                    isUp = false;
                    haveJumpHeight = jumpHeight;
                }
            }
            else if(isUp==false)
            {
                prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y - yMove, prisoner.position.z);
                haveJumpHeight -= yMove;
                if (Mathf.Abs(haveJumpHeight-0) < 0.5f)
                {
                    prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y - haveJumpHeight-0, prisoner.position.z);
                    isJumping = false;
                    haveJumpHeight =0;
                }
            }
        }
    }
    TouchDir GetTouchDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUpPos = Input.mousePosition;
            Vector3 touchOffset = MouseUpPos - MouseDownPos;
            if (Mathf.Abs(touchOffset.x) > 50 || Mathf.Abs(touchOffset.y) > 50)
            {
                if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x > 0)
                {
                    if (targetLineIndex < 2)
                    {
                        targetLineIndex++;
                        moveHorizontal = 14;
                    }
                    return TouchDir.Right;
                }
                else if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x < 0)
                {
                    if (targetLineIndex > 0)
                    {
                        targetLineIndex--;
                        moveHorizontal = -14;
                    }
                    return TouchDir.Left;
                }
                else if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y > 0)

                {
                    if (isJumping == false)
                    {
                        isJumping = true;
                        isUp = true;
                        haveJumpHeight = 0;
                    }
                    return TouchDir.Up;
                }
                else if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y < 0)
                {
                    isSliding = true;
                    slideTimer = 0;
                    return TouchDir.Down;
                }
            }
        }


        return TouchDir.None;
    }
}
