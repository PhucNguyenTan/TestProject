using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollSnap : ScrollRect
{
    public UnityEvent OnEndDragging = new();
    public UnityEvent OnNext = new();
    public UnityEvent OnPrev = new();

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        OnEndDragging.Invoke();
    }

    public override void OnScroll(PointerEventData data)
    {
        var deltaY = data.scrollDelta.y * -1;
        if(deltaY == -1)
        {
            OnPrev.Invoke();
        }
        else if (deltaY == 1)
        {
            OnNext.Invoke();
        }
    }
}
