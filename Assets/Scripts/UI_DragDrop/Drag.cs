using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI_DragDrop
{
    public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private RectTransform rectTransform;
        [SerializeField]
        private Image image;

        private bool isDragging = false;
        private int index;

        public RectTransform Rect { get { return rectTransform; } }
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("BeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Down");
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetIndex(int newIndex)
        {
            index = newIndex;
        }
    }
}