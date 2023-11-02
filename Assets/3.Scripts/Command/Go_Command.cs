using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_Command : Command
{
    public override void Action(bool isWall, bool isJump, bool isJumpDown)
    {
        isReady = false;
        canGo = !isWall;
        tar = transform.position + transform.forward;

        playerAni.SetBool("GO", true);
        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        if (canGo)
        {
            while (Vector3.Distance(transform.position, tar) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tar, movespeed);

                yield return null;
            }
            transform.position = tar;
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        isReady = true;
    }
}
