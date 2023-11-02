using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Command : Command
{
    Vector3 jumpEndPos = Vector3.zero;

    public override void Action(bool isWall, bool isJump, bool isJumpDown)
    {
        isReady = false;
        if (isJump)
        {
            canJump = true;

            tar = transform.position + (transform.forward * 0.5f) + (Vector3.up * 1.1f);
            jumpEndPos = transform.position + transform.forward + Vector3.up;

        }
        else if (isJumpDown)
        {
            canJump = true;

            tar = transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f);
            jumpEndPos = transform.position + transform.forward - Vector3.up;
        }

        playerAni.SetBool("GO", false);
        playerAni.SetBool("JUMP", true);

        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        if (canJump)
        {
            while (Vector3.Distance(transform.position, tar) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tar, movespeed);

                yield return null;
            }
            transform.position = tar;

            while (Vector3.Distance(transform.position, jumpEndPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, jumpEndPos, movespeed);

                yield return null;
            }
            transform.position = jumpEndPos;
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        canJump = false;
        playerAni.SetBool("JUMP", false);
        isReady = true;
    }
}
