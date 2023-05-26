using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI_DragDrop.Views
{
    public class NodeView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Image imageObj;

        [SerializeField]
        private DataObjectScriopt data;

        [SerializeField]
        private Drag drag;

        [SerializeField]
        private int index;
        private Sprite sprite;

        [SerializeField]
        private Canvas canvas;

        private bool isDragging = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            drag.gameObject.SetActive(true);
            drag.SetImage(sprite);
            drag.SetIndex(index);
            ExecuteEvents.Execute(drag.gameObject, eventData, ExecuteEvents.beginDragHandler);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }

        private void Awake()
        {
            sprite = Sprite.Create(
                data.Textures[index], 
                new Rect(0.0f, 0.0f, data.Textures[index].width, data.Textures[index].height), 
                new Vector2(0.5f, 0.5f), 100.0f);
            imageObj.sprite = sprite;

        }
    }
}