using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnapManager : MonoBehaviour
{
    private static List<string> strings = new List<string>
    {
        "Item1",
        "Item2",
        "Item3",
        "Item4"
    };

    public Button       btnL;
    public Button       btnR;
    public TMP_Text     textPrefab;
    public Transform    content;
    public ScrollRect   scrollRect;
    public float        scrollSpeed = 1f;

    private Scrollbar   scrollbar;

    private int         currentSelect = -1;
    private List<float> scrollPositions = new();

    private ScrollRectEvent scrollRectEvent;

    private float scrollGoal    = 0f;
    private float scrollStart   = 0f;
    private float scrollTime    = 1f;

    private void Start()
    {
        scrollbar       = scrollRect.horizontalScrollbar;
        scrollRectEvent = scrollRect.gameObject.GetComponent<ScrollRectEvent>();
        RegisterActions();
        Setup();
    }

    private void Update()
    {
        if(scrollTime < 1f)
        {
            scrollTime = Mathf.Min(1f, scrollTime + Time.deltaTime * scrollSpeed);
            var scrollValue = Mathf.Lerp(scrollStart, scrollGoal, scrollTime);
            scrollbar.value = scrollValue;
        }
    }

    private void SetLerpScroll(int position)
    {
        var goal = scrollPositions[position];
        currentSelect = position;
        scrollStart = scrollbar.value;
        scrollGoal  = goal;
        scrollTime  = 0;
    }

    private void Setup()
    {
        float step = 1f / (strings.Count - 1);
        for (var i = 0; i < strings.Count; i++)
        {
            var str  = strings[i];
            var tmp  = Instantiate(textPrefab, content);
            tmp.text = str;
            scrollPositions.Add(i * step);
        }
    }

    private void RegisterActions()
    {
        btnL.onClick.AddListener(GoLeft);
        btnR.onClick.AddListener(GoRight);
        scrollRectEvent.OnEndDrag.AddListener(Snap);
    }

    private void Snap()
    {
        var current = scrollbar.value;
        var list = new List<(float, int)>();
        for(var i = 0; i < scrollPositions.Count; i++)
        {
            list.Add((Mathf.Abs(current - scrollPositions[i]), i));
        }
        var test = list.OrderBy(x => x.Item1).First();
        SetLerpScroll(test.Item2);
    }

    private void GoLeft()
    {
        if (currentSelect == 0)
            return;
        var nextTo = currentSelect - 1;
        SetLerpScroll(nextTo);

    }

    private void GoRight()
    {
        if (currentSelect == strings.Count - 1)
            return;
        var nextTo = currentSelect + 1;
        SetLerpScroll(nextTo);
    }
}