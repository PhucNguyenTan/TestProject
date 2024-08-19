using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScrollRectEvent : MonoBehaviour, IEndDragHandler
{
    public UnityEvent OnEndDrag = new();

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        OnEndDrag.Invoke();
    }
}
