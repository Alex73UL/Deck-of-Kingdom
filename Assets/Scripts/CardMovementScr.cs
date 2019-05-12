using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// Главная камера
    /// </summary>
    Camera MainCamera;
    /// <summary>
    /// Отступ от центра карты
    /// </summary>
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public GameManagerScr GameManager;
    public bool IsDraggable;
    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
        GameManager = FindObjectOfType<GameManagerScr>();
    }
    //OnBeginDrag- код выполняется один раз, как только начинаем перетягивать объект
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);//отнимаем отступ от центра до указателя мыши, иначе при поднятии карты, указатель мыши перемещается к центру

        DefaultParent = DefaultTempCardParent = transform.parent;

        IsDraggable = (DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND || DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_FIELD) && GameManager.IsPlayerTurn;

        if (!IsDraggable)
            return;

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }
    //OnDrag- Выполняется каждый кадр, пока тянем объект
    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;
        
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position); // значение текущей координаты мыши
        transform.position = newPos + offset; // прибавляем обратно отступ

        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);

        CheckPosition();
    }
    //OnEndDrag - выполняется  раз, когда отпускаем объект
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());
        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(2340, 0);
    }
    void CheckPosition()
    {
        int newIndex = DefaultTempCardParent.childCount;

        for(int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if(transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            }
        }
        TempCardGO.transform.SetSiblingIndex(newIndex);
    }
}
