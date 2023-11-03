using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Result Board UI")]
    [SerializeField] private GameObject resultBoard;
    [SerializeField] private GameObject[] resultBlock;

    [Space(10f)]

    [SerializeField] private Sprite[] resultSprite;
    [SerializeField] private Sprite[] OnBtnSprite;
    [SerializeField] private Image[] boardImg;
    [SerializeField] private Color[] boardColor;

    [Space(10f)]

    [SerializeField] private GameObject[] mainBoardInput;
    [SerializeField] private GameObject[] proc1BoardInput;
    [SerializeField] private GameObject[] proc2BoardInput;
    [HideInInspector] public GameObject[] selectBoradInput;
    [HideInInspector] public int selectBoardNum;

    [Space(20f)]

    [Header("Start or Stop UI")]
    [SerializeField] private Image startOrStopBtn;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite stopSprite;

    [Space(20f)]

    [Header("Player Input UI")]
    [SerializeField] private GameObject[] inputBtn;
    [SerializeField] private GameObject inputBlock;

    private bool isBtnOn = false;

    public void UIReset() //모든 UI 초기화
    {
        isBtnOn = true;
        StartOrStopBtn();
        ResultBoardClick(0);

        resultBoard.SetActive(false);

        for (int i = 0; i < inputBtn.Length; i++)
        {
            inputBtn[i].SetActive(false);
        }
        for (int i = 0; i < mainBoardInput.Length; i++)
        {
            mainBoardInput[i].SetActive(false);
        }
        for(int i = 0; i< proc1BoardInput.Length; i++)
        {
            proc1BoardInput[i].SetActive(false);
        }
        for (int i = 0; i < proc2BoardInput.Length; i++)
        {
            proc2BoardInput[i].SetActive(false);
        }
    }

    public void PlayerInputBtnOn() //하단 플레이어 조작 버튼 세팅
    {
        boardImg[1].gameObject.SetActive(false);
        boardImg[2].gameObject.SetActive(false);

        resultBoard.SetActive(true);

        for (int i = 0; i < GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].inputBtnArr.Length; i++)
        {
            inputBtn[GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].inputBtnArr[i]].SetActive(true);

            if (GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].inputBtnArr[i] == 5)
            {
                boardImg[1].gameObject.SetActive(true);
            }
            if (GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].inputBtnArr[i] == 6)
            {
                boardImg[2].gameObject.SetActive(true);
            }
        }
    }

    public void ResultBoardSet(List<int> playerInput) //우측 결과 UI 업데이트
    {
        isBtnOn = true;
        StartOrStopBtn();

        for (int i = 0; i < selectBoradInput.Length; i++)
        {
            if (i < playerInput.Count)
            {
                selectBoradInput[i].SetActive(true);

                selectBoradInput[i].GetComponentsInChildren<Image>()[1].sprite = resultSprite[playerInput[i]];
            }
            else
            {
                if (selectBoradInput[i].activeSelf)
                {
                    selectBoradInput[i].SetActive(false);
                }
                else
                {
                    return;
                }
            }

        }
    }

    public void StartOrStopBtn() //시작, 멈춤 버튼 세팅
    {
        isBtnOn = !isBtnOn;

        if (isBtnOn)
        {
            FindObjectOfType<PlayerAction>().StartMove();
            startOrStopBtn.sprite = stopSprite;
            inputBlock.SetActive(true);
        }
        else
        {
            FindObjectOfType<PlayerAction>().StopMove();
            startOrStopBtn.sprite = startSprite;
            inputBlock.SetActive(false);
        }
    }

    public void ResultBoardBtnOn(int num, int boardNum) //우측 결과 보드 버튼 색 ON 함수
    {
        if (boardNum == 0)
        {
            mainBoardInput[num].GetComponent<Image>().sprite = OnBtnSprite[1];
        }
        else if (boardNum == 1)
        {
            proc1BoardInput[num].GetComponent<Image>().sprite = OnBtnSprite[1];
        }
        else if (boardNum == 2)
        {
            proc2BoardInput[num].GetComponent<Image>().sprite = OnBtnSprite[1];
        }
    }
    public void ResultBoardBtnOff(int boardNum) //우측 결과 보드 버튼 색 OFF 함수
    {
        if (boardNum == 0)
        {
            for (int i = 0; i < mainBoardInput.Length; i++)
            {
                mainBoardInput[i].GetComponent<Image>().sprite = OnBtnSprite[0];
            }
        }
        else if (boardNum == 1)
        {
            for (int i = 0; i < proc1BoardInput.Length; i++)
            {
                proc1BoardInput[i].GetComponent<Image>().sprite = OnBtnSprite[0];
            }
        }
        else if (boardNum == 2)
        {
            for (int i = 0; i < proc2BoardInput.Length; i++)
            {
                proc2BoardInput[i].GetComponent<Image>().sprite = OnBtnSprite[0];
            }
        }
    }

    public void ResultBoardClick(int num) //우측 결과 보드 선택 시스템 구현
    {
        for (int i = 0; i < boardImg.Length; i++)
        {
            if (i != num) boardImg[i].color = boardColor[1];
        }
        boardImg[num].color = boardColor[0];

        for (int i = 0; i < resultBlock.Length; i++)
        {
            resultBlock[i].SetActive(true);
        }
        resultBlock[num].SetActive(false);

        if (num == 0)
        {
            selectBoradInput = new GameObject[mainBoardInput.Length];
            selectBoradInput = mainBoardInput;
        }
        else if (num == 1)
        {
            selectBoradInput = new GameObject[proc1BoardInput.Length];
            selectBoradInput = proc1BoardInput;
        }
        else if (num == 2)
        {
            selectBoradInput = new GameObject[proc2BoardInput.Length];
            selectBoradInput = proc2BoardInput;
        }

        selectBoardNum = num;
    }
}
