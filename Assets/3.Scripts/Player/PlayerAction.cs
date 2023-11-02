using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Command[] playerCommandList;

    Command playerCommand;

    Vector3 playerStartPos;
    Vector3 playerStartRot;

    bool isReady = false;
    bool isWall = false;
    bool isJump = false;
    bool isJumpDown = false;

    bool isMove = false;
    int moveMainCnt;
    int moveProc1Cnt;
    int moveProc2Cnt;

    private void OnEnable()
    {
        playerCommand = playerCommandList[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isWall = true;
        }
        if (other.CompareTag("Jump"))
        {
            isWall = true;
            isJump = true;
        }
        if (other.CompareTag("JumpDown"))
        {
            isWall = true;
            isJumpDown = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isWall = false;
        }
        if (other.CompareTag("Jump"))
        {
            isWall = false;
            isJump = false;
        }
        if (other.CompareTag("JumpDown"))
        {
            isWall = false;
            isJumpDown = false;
        }
    }

    private void Update()
    {
        if (isMove)
        {
            if (isReady)
            {
                MainSimulation();
            }

            FinishCheck();

            isReady = playerCommand.isReady;
        }
    }

    private void MainSimulation()
    {
        if (moveMainCnt == GameManager.Instance.playerInput[0].Count)
        {
            playerCommand.playerAni.SetBool("GO", false);
            playerCommand.playerAni.SetBool("JUMP", false);

            isMove = false;

            return;
        }

        UIManager.Instance.ResultBoardBtnOn(moveMainCnt, 0);

        if (GameManager.Instance.playerInput[0][moveMainCnt] == 5)
        {
            if (Proc1Simulation()) moveMainCnt++;
        }
        else if(GameManager.Instance.playerInput[0][moveMainCnt] == 6)
        {
            if (Proc2Simulation()) moveMainCnt++;
        }
        else
        {
            Movement(GameManager.Instance.playerInput[0][moveMainCnt]);
            moveMainCnt++;
        }
    }

    private bool Proc1Simulation()
    {
        if (GameManager.Instance.playerInput[1].Count == moveProc1Cnt)
        {
            moveProc1Cnt = 0;
            UIManager.Instance.ResultBoardBtnOff(1);
            playerCommand.isReady = true;
            return true;
        }

        UIManager.Instance.ResultBoardBtnOn(moveProc1Cnt, 1);

        if (GameManager.Instance.playerInput[1][moveProc1Cnt] == 5)
        {
            UIManager.Instance.ResultBoardBtnOff(1);
            moveProc1Cnt = 0;
        }
        else if (GameManager.Instance.playerInput[1][moveProc1Cnt] == 6)
        {
            if(Proc2Simulation()) moveProc1Cnt++;
        }
        else
        {
            Movement(GameManager.Instance.playerInput[1][moveProc1Cnt]);

            moveProc1Cnt++;
        }

        return false;
    }

    private bool Proc2Simulation()
    {
        if (GameManager.Instance.playerInput[2].Count == moveProc2Cnt)
        {
            moveProc2Cnt = 0;
            UIManager.Instance.ResultBoardBtnOff(2);
            playerCommand.isReady = true;
            return true;
        }

        UIManager.Instance.ResultBoardBtnOn(moveProc2Cnt, 2);

        if (GameManager.Instance.playerInput[2][moveProc2Cnt] == 6)
        {
            UIManager.Instance.ResultBoardBtnOff(2);
            moveProc2Cnt = 0;
        }
        else if (GameManager.Instance.playerInput[2][moveProc2Cnt] == 5)
        {
            if (Proc1Simulation()) moveProc2Cnt++;
        }
        else
        {
            Movement(GameManager.Instance.playerInput[2][moveProc2Cnt]);

            moveProc2Cnt++;
        }

        return false;
    }

    private void Movement(int playerMove)
    {
        isReady = false;

        playerCommand = playerCommandList[playerMove];
        playerCommand.Action(isWall, isJump, isJumpDown);
    }

    public void StartMove()
    {
        moveMainCnt = 0;
        moveProc1Cnt = 0;
        moveProc2Cnt = 0;

        if (GameManager.Instance.playerInput[0].Count != 0)
        {
            isMove = true;
            isReady = true;
        }
    }

    public void StopMove()
    {
        playerCommand.playerAni.SetBool("GO", false);
        playerCommand.playerAni.SetBool("JUMP", false);
        playerCommand.StopAllCoroutines();

        if (GetComponent<Light_Command>().lightAniOn.Count != 0)
        {
            for (int i = 0; i < GetComponent<Light_Command>().lightAniOn.Count; i++)
            {
                GetComponent<Light_Command>().lightAniOn[i].SetBool("LIGHT", false);
            }
        }

        isMove = false;

        transform.position = playerStartPos;
        transform.eulerAngles = playerStartRot;

        GameManager.Instance.currentLight = 0;

        UIManager.Instance.ResultBoardBtnOff(0);
        UIManager.Instance.ResultBoardBtnOff(1);
        UIManager.Instance.ResultBoardBtnOff(2);
    }

    private void FinishCheck()
    {
        if (GameManager.Instance.currentLight == GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].lightCnt)
        {
            playerCommand.playerAni.SetBool("GO", false);
            playerCommand.playerAni.SetBool("JUMP", false);
            playerCommand.StopAllCoroutines();

            GameManager.Instance.isFinish = true;
        }
    }

    public void LoadPlayer()
    {
        transform.position = new Vector3(
            GameObject.Find("Base1").transform.position.x,
            GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].playerYPos,
            GameObject.Find("Base1").transform.position.z);
        transform.eulerAngles = new Vector3(0, GameObject.Find("Base1").transform.eulerAngles.y, 0);

        playerStartPos = transform.position;
        playerStartRot = transform.eulerAngles;

        StartCoroutine(LoadPlayer_co());

        GetComponent<Light_Command>().lightAni = null;
        isMove = false;
        isWall = false;
        isJump = false;
        isJumpDown = false;
    }

    IEnumerator LoadPlayer_co()
    {
        transform.position += Vector3.up;

        while (Vector3.Distance(transform.position, playerStartPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, playerStartPos, 0.005f * 2);

            yield return null;
        }

        transform.position = playerStartPos;

        UIManager.Instance.PlayerInputBtnOn();
    }

}
