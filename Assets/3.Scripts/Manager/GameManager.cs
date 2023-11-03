using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //라운드 정보
    public RoundInfo[] roundInfo;   
    public int roundAll;
    public int roundCnt;
    public int currentLight;
    public bool isFinish = false;
    [SerializeField] private GameObject finishBoard;
    [SerializeField] private GameObject completeBoard;

    //플레이어 움직임 실행 할 int 값 저장
    public Dictionary<int, List<int>> playerInput = new Dictionary<int, List<int>>();
    public List<int> playerMainInput = new List<int>();
    public List<int> playerProc1Input = new List<int>();
    public List<int> playerProc2Input = new List<int>();

    private void OnEnable() //dictionary에 list 할당
    {
        playerInput.Add(0, playerMainInput);
        playerInput.Add(1, playerProc1Input);
        playerInput.Add(2, playerProc2Input);
    }

    private void Start() //전체 라운드 수 체크 및 라운드 초기화
    {
        for(int i = 0; i < roundInfo.Length; i++)
        {
            roundAll++;
        }

        NextStageBtn();
    }

    private void Update() //게임 종료 조건 체크
    {
        if (isFinish)
        {
            if (roundCnt == roundAll)
            {
                completeBoard.SetActive(true);
            }
            else
            {
                finishBoard.SetActive(true);
            }
        }
        else
        {
            finishBoard.SetActive(false);
        }
    }

    public void NextStageBtn() //라운드 초기화 버튼
    {
        isFinish = false;

        roundCnt++;

        RoundSet();
    }

    public void RoundSet() //라운드 세팅 
    {
        currentLight = 0;

        UIManager.Instance.UIReset();
        PlayerInput.Instance.InputReset();

        FindObjectOfType<MapLoader>().LoadMap(roundCnt);
        FindObjectOfType<PlayerAction>().LoadPlayer();
    }

    public void CompleteBtn()
    {
        completeBoard.transform.GetChild(0).gameObject.SetActive(false);
        completeBoard.transform.GetChild(1).gameObject.SetActive(true);
    }
}
