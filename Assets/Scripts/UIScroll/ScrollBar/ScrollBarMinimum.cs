using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIScroll.Scroll_bar
{
    public class ScrollBarMinimum : MonoBehaviour
    {
        [SerializeField]
        private RectTransform handleCustom;
        [SerializeField]
        private RectTransform handleOriginal;
        [SerializeField]
        private Scrollbar scrollbar;
        [SerializeField, Range(0, 1)]
        private float sizeMin;

        private const float sizeMax = 1f;
        private void OnEnable()
        {
            SetCustomScrollbar(scrollbar.value);
            scrollbar.onValueChanged.AddListener(SetCustomScrollbar);
        }

        private void SetCustomScrollbar(float scrollValue)
        {
            var minY = Mathf.Min(sizeMax, scrollValue * (sizeMax - sizeMin));
            //var maxY = handleCustom.anchorMin.y + sizeMin;
            //var maxY = Mathf.Min(sizeMax, minY + Mathf.Max(0f, sizeMin - Mathf.Abs(scrollValue - 0.5f) * 2f));
            var maxY = Mathf.Max(0f, minY + Mathf.Max(0f, sizeMin - Mathf.Abs(scrollValue - 0.5f) * 2f));
            handleCustom.anchorMin = new Vector2(handleCustom.anchorMin.x, minY);
            handleCustom.anchorMax = new Vector2(handleCustom.anchorMax.x, maxY);
        }
    }

}
