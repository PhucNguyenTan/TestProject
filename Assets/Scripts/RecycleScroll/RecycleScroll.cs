using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecycleScroll : ScrollRect
{
    public RectTransform PrototypeCell;
    public RectTransform ViewPort;
    public RectTransform Content;
    public IDatasource Data;

    ScrollRect _scrollRect;

    VerticalScrollSystem _scrollSystem;
    Vector2 _prevAnchoredPos;

    

    private void Start()
    {
        
        Initialize();
    }

    
    void Initialize()
    {
        _scrollSystem = new VerticalScrollSystem(ViewPort, Content, PrototypeCell, Data);
        _prevAnchoredPos = Content.anchoredPosition;
        onValueChanged.RemoveListener(onValueChangedListener);
        StartCoroutine(_scrollSystem.InitCoroutine(() => onValueChanged.AddListener(onValueChangedListener))); //???
    }

    public void onValueChangedListener(Vector2 pos)
    {
        Vector2 direction = Content.anchoredPosition - _prevAnchoredPos;
        m_ContentStartPosition = _scrollSystem.OnValueChangeListener(direction);
        _prevAnchoredPos = Content.anchoredPosition;
    }

    void ReloadData()
    {
        ReloadData(Data);
    }

    void ReloadData(IDatasource data)
    {
        if(_scrollSystem != null)
        {
            StopMovement();
            onValueChanged.RemoveListener(onValueChangedListener);
            _scrollSystem.Data = Data;
            StartCoroutine(_scrollSystem.InitCoroutine(() => onValueChanged.AddListener(onValueChangedListener))); //???

            _prevAnchoredPos = Content.anchoredPosition;

        }
    }
}
