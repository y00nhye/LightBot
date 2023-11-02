using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public RoundInfo[] roundInfo;
    
    public int roundAll;
    public int roundCnt;
    public int currentLight;

    public Dictionary<int, List<int>> playerInput = new Dictionary<int, List<int>>();
    public List<int> playerMainInput = new List<int>();
    public List<int> playerProc1Input = new List<int>();
    public List<int> playerProc2Input = new List<int>();

    public bool isFinish = false;

    [SerializeField] private GameObject finishBoard;
    [SerializeField] private GameObject completeBoard;

    private void OnEnable()
    {
        playerInput.Add(0, playerMainInput);
        playerInput.Add(1, playerProc1Input);
        playerInput.Add(2, playerProc2Input);
    }

    private void Start()
    {
        for(int i = 0; i < roundInfo.Length; i++)
        {
            roundAll++;
        }

        NextStageBtn();
    }

    private void Update()
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

    public void NextStageBtn()
    {
        isFinish = false;

        roundCnt++;

        RoundSet();
    }

    public void RoundSet()
    {
        currentLight = 0;

        UIManager.Instance.UIReset();
        PlayerInput.Instance.InputReset();

        FindObjectOfType<MapLoader>().LoadMap();
        FindObjectOfType<PlayerAction>().LoadPlayer();
    }

    public void CompleteBtn()
    {
        completeBoard.transform.GetChild(0).gameObject.SetActive(false);
        completeBoard.transform.GetChild(1).gameObject.SetActive(true);
    }
}
