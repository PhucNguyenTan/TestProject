using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace UniRx
{
    public class InputHandler : MonoBehaviour
    {
        public static UnityAction<float> OnMouseDragEvent;
        public static UnityAction OnMouseDoubleClick;

        private float lastClick = 0;
        private float waitTime = 1.0f;
        private float downTime; 
        private bool isHandled = false;

        private void Start()
        {
            //var clickmouseDown = Observable.EveryUpdate().
            //    Where(_ => Input.GetMouseButtonDown(0)).
            //    Where(_ => CheckIfClickOnObj(Input.mousePosition));

            //var clickMouseUp = Observable.EveryUpdate()
            //    .Where(_ => Input.GetMouseButtonUp(0));

            
        }

        private void Update()
        {
            
        }

        bool CheckIfClickOnObj()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray);
        }

        void OnMouseDown()
        {
            downTime = Time.time;

            if (Time.time - lastClick < 0.3f)
            {
                OnMouseDoubleClick?.Invoke();
            }
            lastClick = Time.time;
        }
        void OnMouseDrag()
        {
            if ((Time.time > downTime + waitTime))
            {
                OnMouseDragEvent?.Invoke(Input.mousePosition.y);
            }
        }
    }
}

