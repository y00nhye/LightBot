using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDrop : MonoBehaviour, IPointerEnterHandler //마우스 오버 상태에 따른 결과 보드 선택 구현 클래스
{
    public int blockNum;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ResultBoardClick(blockNum);
    }
}
