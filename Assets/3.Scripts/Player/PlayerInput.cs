using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput> //플레이어 입력 command 저장 함수 구현 클래스
{
    public void InputAdd(int input) //추가
    {
        if (UIManager.Instance.selectBoradInput.Length > GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count)
        {
            GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Add(input);
            UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
        }

    }

    public void InputInsert(int index, int input) //삽입
    {
        if(UIManager.Instance.selectBoradInput.Length <= GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count)
        {
            GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].RemoveAt(UIManager.Instance.selectBoradInput.Length - 1);
        }
        
        GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Insert(index, input);

        UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
    }

    public void InputDelete(int index) //삭제
    {
        GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].RemoveAt(index);

        UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
    }

    public void InputReset() //리셋
    {
        GameManager.Instance.playerInput[0] = new List<int>();
        GameManager.Instance.playerInput[1] = new List<int>();
    }
}
