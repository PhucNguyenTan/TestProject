using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniRx
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] float _rotateSpeed = 1f;

        Renderer _render;
        bool isBig;
        Vector3 _normalSize;
        Vector3 _bigSize;

        float lastClick = 0;
        float waitTime = 0.1f;
        float downTime;
        bool isHandled = false;

        private void Awake()
        {
            _render = GetComponent<Renderer>();
            _normalSize = transform.localScale;
            _bigSize = _normalSize * 2;
            
        }

        private void Start()
        {
            Vector3 cam = Camera.main.transform.position;
            transform.position = new Vector3(cam.x, cam.y, cam.z + 5f);
        }

        private void OnEnable()
        {
            ButtonColor.OnClickColor += ChangeColor;
            InputHandler.OnMouseDoubleClick += ChangeSize;
        }

        private void OnDisable()
        {
            ButtonColor.OnClickColor -= ChangeColor;
            InputHandler.OnMouseDoubleClick -= ChangeSize;
        }

        void ChangeColor(Color color)
        {
            _render.material.color = color;
        }

        void ChangeSize()
        {
            if (isBig)
            {
                transform.localScale = _normalSize;
                isBig = false;
            }
            else
            {
                transform.localScale = _bigSize;
                isBig = true;
            }
        }

        void RotateY(float rotate)
        {
            transform.Rotate(Vector3.up, -rotate);
        }

        void OnMouseDown()
        {
            downTime = Time.time;

            if (Time.time - lastClick < 0.3f)
            {
                ChangeSize();
            }
            lastClick = Time.time;
        }
        void OnMouseDrag()
        {
            if ((Time.time > downTime + waitTime))
            {
                RotateY(Input.GetAxis("Mouse X") * _rotateSpeed);
            }
        }
    }
}

