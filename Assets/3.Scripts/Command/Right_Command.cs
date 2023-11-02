using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Command : Command
{
    public override void Action(bool isWall, bool isJump, bool isJumpDown)
    {
        isReady = false;
        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        transform.eulerAngles += new Vector3(0, 90, 0);
        yield return new WaitForSeconds(0.2f);

        isReady = true;
    }
}
