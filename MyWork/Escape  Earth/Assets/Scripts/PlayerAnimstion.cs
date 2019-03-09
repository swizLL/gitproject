using UnityEngine;
using System.Collections;
public enum AnimationState
{
    glide,
    run,
    turnright,
    turnleft,
    slide,
    jump,
    death
}
public class PlayerAnimstion : MonoBehaviour
{

    public AnimationState animationState = AnimationState.glide;

    private Animation animation;
    private playerMove playermove;
    private bool havePlayDeath;
    void Awake()
    {
        animation = transform.Find("Prisoner").GetComponent<Animation>();
        playermove = this.GetComponent<playerMove>();
    }


    // Update is called once per frame
    void Update()
    {
        if (GameController.gamestate == GameController.Gamestate.Menu)
        {
            animationState = AnimationState.glide;
        }
        else if (GameController.gamestate == GameController.Gamestate.Playing)
        {
            animationState = AnimationState.run;
            //if (playermove.nowLineIndex > playermove.targetLineIndex)
            //{
            //    animationState = AnimationState.turnleft;
            //}else if (playermove.nowLineIndex < playermove.targetLineIndex)
            //{
            //    animationState = AnimationState.turnright;
            //}
            if (playermove.isSliding == true)
            {
                animationState = AnimationState.slide;
            }
            if (playermove.isJumping)
            {
                animationState = AnimationState.jump;
            }
        }
        else if (GameController.gamestate == GameController.Gamestate.End)
        {
            animationState = AnimationState.death;
        }
    }
    void LateUpdate()
    {
        switch (animationState)
        {
            case AnimationState.glide:
                PlayAnimation("glide");
                break;
            case AnimationState.run:
                PlayAnimation("run");
                break;
            case AnimationState.slide:
                animation["slide"].speed = 4;
                PlayAnimation("slide");
                break;
            case AnimationState.jump:
                PlayAnimation("jump");
                break;
            case AnimationState.death:
                PlayDeath();
                break;
                //case AnimationState.turnright:
                //    animation["right"].speed = 2.3f;
                //    PlayAnimation("right");
                //    break;
                //case AnimationState.turnleft:
                //    animation["left"].speed = 2.3f;
                //    PlayAnimation("left");
                //    break;
        }

    }
    private void PlayAnimation(string animationName)
    {
        if (animation.IsPlaying(animationName) == false)
        {
            animation.Play(animationName);
        }
    }
    private void PlayDeath()
    {
        if (animation.IsPlaying("death")==false && havePlayDeath == false)
        {
            animation.Play("death");
            havePlayDeath = true;
        }
    }
    //void PlayIdle()
    //{
    //    if (animation.IsPlaying("glide") == false)
    //    {
    //        animation.Play("glide");
    //    }
    //}
    //void PlayRun()
    //{
    //    if (animation.IsPlaying("run") == false)
    //    {
    //        animation.Play("run");
    //    }
    //}
}
