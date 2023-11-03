using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 움직일 때 사용하는 공통 요소를 묶은 부모 클래스
public abstract class Command : MonoBehaviour
{
    public float movespeed = 0.005f; //움직임 속도
    public Animator playerAni;

    //움직일 때 바뀌는 값
    public bool canGo;
    public bool canJump;
    public Vector3 tar;
    public bool isReady;

    public abstract void Action(bool isWall, bool isJump, bool isJumpDown); //움직임 실행 함수

    public abstract IEnumerator Action_co(); //움직임 코루틴
}
