using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputBtnDrop : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler //버튼 드랍 시 상호작용 구현 클래스
{
    Image img;
    Color color;
    PlayerInput playerInput;

    [SerializeField] int index;
    [SerializeField] bool defaultChecker;

    Vector2 defaultPos;

    private void Awake()
    {
        img = GetComponentsInChildren<Image>()[1];
        playerInput = FindObjectOfType<PlayerInput>();
        color = img.color;
    }

    private void Start()
    {
        defaultPos = new Vector2(img.rectTransform.anchoredPosition.x, img.rectTransform.anchoredPosition.y);
    }
    private void Update()
    {
        if (defaultChecker)
        {
            index = GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count;
            img.rectTransform.anchoredPosition = new Vector2(defaultPos.x + (100 * (index % 4) + 20), defaultPos.y - (100 * (index / 4)));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            color.a = 100;
            img.color = color;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<InputBtnDrag>()) playerInput.InputInsert(index, eventData.pointerDrag.GetComponent<InputBtnDrag>().btnNum);

            color.a = 0;
            img.color = color;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            color.a = 0;
            img.color = color;
        }
    }
}
