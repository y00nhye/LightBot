using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Command : MonoBehaviour
{
    public float movespeed = 0.005f;
    public Animator playerAni;
    
    public bool canGo;
    public bool canJump;
    public Vector3 tar;

    public bool isReady;

    public abstract void Action(bool isWall, bool isJump, bool isJumpDown);

    public abstract IEnumerator Action_co();
}
