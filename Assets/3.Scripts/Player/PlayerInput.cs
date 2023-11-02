using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
    /*    
    <플레이어 입력 스크립트>

    - 플레이어의 버튼 입력에 따른 결과 저장 > 추가, 삭제 모두 구현하기 위해 list로 작성
    - 추가, 삭제만 판단하여 결과 변수에 저장
    - 플레이어의 입력 결과를 움직임, UI 에서 사용하기 위해 singleton 패턴 사용
    - 버튼 종류는 숫자로 대입해 판단 > 0.전진 1.빛 2.회전(오) 3.회전(왼) 4.점프 5.1P 6.2P  (7가지)
     */

    public void InputAdd(int input)
    {
        if (UIManager.Instance.selectBoradInput.Length > GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count)
        {
            GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Add(input);
            UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
        }

    }

    public void InputInsert(int index, int input)
    {
        if(UIManager.Instance.selectBoradInput.Length <= GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count)
        {
            GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].RemoveAt(UIManager.Instance.selectBoradInput.Length - 1);
        }
        
        GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Insert(index, input);

        UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
    }

    public void InputDelete(int index)
    {
        GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].RemoveAt(index);

        UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
    }

    public void InputReset()
    {
        GameManager.Instance.playerInput[0] = new List<int>();
        GameManager.Instance.playerInput[1] = new List<int>();
    }
}
