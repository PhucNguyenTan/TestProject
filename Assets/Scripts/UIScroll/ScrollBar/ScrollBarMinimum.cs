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
            var minY = scrollValue * (sizeMax - sizeMin);
            var maxY = handleCustom.anchorMin.y + sizeMin;
            handleCustom.anchorMin = new Vector2(handleCustom.anchorMin.x, minY);
            handleCustom.anchorMax = new Vector2(handleCustom.anchorMin.x, maxY);
        }
    }

}
