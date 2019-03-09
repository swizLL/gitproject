using UnityEngine;
using System.Collections;

public class PlayerSmallColider : MonoBehaviour {

    private PlayerAnimstion playerAnimation;
    void Awake()
    {
        playerAnimation = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAnimstion>();
    }
    void OnTriggerEnter(Collider obst)
    {
        if (obst.tag == Tags.obstacles && GameController.gamestate == GameController.Gamestate.Playing && playerAnimation.animationState == AnimationState.slide)
        {
            GameController.gamestate = GameController.Gamestate.End;
        }
    }
}
