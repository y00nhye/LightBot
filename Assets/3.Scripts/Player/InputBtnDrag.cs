using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputBtnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //버튼 드래그 시 
{
    [SerializeField] Image copyObject;
    RectTransform rect;
    CanvasGroup canvasGroup;
    Transform canvas;

    public int btnNum;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;

        rect = copyObject.GetComponent<RectTransform>();
        canvasGroup = copyObject.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Button>().interactable = false;

        copyObject.gameObject.SetActive(true);
        copyObject.GetComponentsInChildren<Image>()[1].sprite = eventData.pointerDrag.GetComponentsInChildren<Image>()[1].sprite;

        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;

        if (transform.parent.name == "Result")
        {
            btnNum = GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum][int.Parse(gameObject.name)];
            FindObjectOfType<PlayerInput>().InputDelete(int.Parse(gameObject.name));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Button>().interactable = true;

        copyObject.gameObject.SetActive(false);
    }
}
