using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrollSystem
{
    Vector2 cachedZeroV2 = Vector2.zero;

    RectTransform _viewPort;
    RectTransform _content;
    RectTransform _prototypeCell;
    public IDatasource Data;

    float _minPoolCoverage = 1.5f;
    int _minPoolSize = 8;
    float _recyclingThreshold = .2f;

    List<RectTransform> _cellPool;
    List<ICell> _cachedCells;
    Bounds _recycleViewBounds;

    float _cellWidth, _cellHeight;

    readonly Vector3[] _viewPortCorners = new Vector3[4];
    bool _isRecycling;

    int _currentItemCount;
    int _topMostCellIndex;
    int _bottomMostCellIndex;

    int _cachedDataItemCount;

    public VerticalScrollSystem(RectTransform viewPort, RectTransform content, RectTransform prototypeCell, IDatasource data)
    {
        _viewPort = viewPort;
        _content = content;
        _prototypeCell = prototypeCell;
        Data = data;
        _cachedDataItemCount = Data.GetItemCount();
        _recycleViewBounds = new Bounds();

        _cellHeight = prototypeCell.sizeDelta.y;
        _cellWidth = prototypeCell.sizeDelta.x;
    }

    public IEnumerator InitCoroutine(System.Action onInitialize /*???*/) {
        Utilities.SetTopAnchor(_content);
        _content.anchoredPosition = Vector3.zero;
        yield return null; //???
        SetRecyclingBounds();

        CreateCellPool();
        _currentItemCount = _cellPool.Count;
        _topMostCellIndex = 0;
        _bottomMostCellIndex = _cellPool.Count - 1; //??? as it's set in CreateCellPool()

        float contentYsize = _cellPool.Count * _cellHeight;
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, contentYsize);
        Utilities.SetTopAnchor(_content);

        if (onInitialize != null) onInitialize();
    }

    void SetRecyclingBounds()
    {
        _viewPort.GetWorldCorners(_viewPortCorners);
        float threshold = _recyclingThreshold * (_viewPortCorners[2].y - _viewPortCorners[0].y);
        _recycleViewBounds.min = new Vector3(_viewPortCorners[0].x, _viewPortCorners[0].y - threshold);
        _recycleViewBounds.max = new Vector3(_viewPortCorners[2].x, _viewPortCorners[2].y + threshold);
    }

    void CreateCellPool()
    {
        ResetCellPool();
        _prototypeCell.gameObject.SetActive(true);
        Utilities.SetTopAnchor(_prototypeCell);
        _topMostCellIndex = _bottomMostCellIndex = 0;

        float currentPoolCoverage = 0;
        int poolSize = 0;
        float posX = 0f;
        float posY = 0f;

        _cellHeight = _content.rect.width;
        _cellHeight = _prototypeCell.sizeDelta.y / _prototypeCell.sizeDelta.x * _cellWidth; //???

        float requiredCoverage = _minPoolCoverage * _viewPort.rect.height;
        int minPoolSize = Mathf.Min(_minPoolSize, _cachedDataItemCount);

        int numOfCell = 1;
        while((poolSize < minPoolSize || currentPoolCoverage < requiredCoverage)
            && poolSize < _cachedDataItemCount)
        {
            RectTransform item = Object.Instantiate(_prototypeCell);
            item.name = $"Cell_{numOfCell}";
            item.sizeDelta = new Vector2(_cellWidth, _cellHeight);
            _cellPool.Add(item);
            item.SetParent(_content, false);
            item.anchoredPosition = new Vector2(0, posY);

            numOfCell++;
            float nextItemPosY = item.anchoredPosition.y - item.rect.height;
            posY = nextItemPosY;
            currentPoolCoverage += item.rect.height;

            _cachedCells.Add(item.GetComponent<ICell>());
            Data.SetCell(_cachedCells[_cachedCells.Count - 1], poolSize); //???
            poolSize++;
        }
    }

    void ResetCellPool()
    {
        if (_cellPool != null)
        {
            _cellPool.ForEach(rect => Object.Destroy(rect.gameObject));
            _cellPool.Clear();
            _cachedCells.Clear();
        }
        else
        {
            _cellPool = new List<RectTransform>();
            _cachedCells = new List<ICell>();
        }
    }

    public Vector2 OnValueChangeListener(Vector2 direction)
    {
        if (_isRecycling || _cellPool == null || _cellPool.Count == 0) return cachedZeroV2;
        SetRecyclingBounds();

        if(direction.y > 0 && _cellPool[_bottomMostCellIndex].MaxY() > _recycleViewBounds.min.y)
        {
            return MoveToptoBottom();
        }
        else if(direction.y < 0 && _cellPool[_topMostCellIndex].MinY() < _recycleViewBounds.max.y)
        {
            return MoveBottomtoTop();
        }

        return cachedZeroV2;
    }

    Vector2 MoveToptoBottom()
    {
        _isRecycling = true;
        int n = 0;
        float posY = 0;
        float posX = 0;
        while(_cellPool[_topMostCellIndex].MinY() > _recycleViewBounds.max.y && _currentItemCount < _cachedDataItemCount)
        {
            posY = _cellPool[_bottomMostCellIndex].anchoredPosition.y - _cellPool[_bottomMostCellIndex].sizeDelta.y;
            _cellPool[_topMostCellIndex].anchoredPosition = new Vector2(_cellPool[_topMostCellIndex].anchoredPosition.x, posY);
            

            Data.SetCell(_cachedCells[_topMostCellIndex], _currentItemCount);

            _bottomMostCellIndex = _topMostCellIndex;
            _topMostCellIndex = (_topMostCellIndex + 1) % _cellPool.Count;

            _currentItemCount++;

        }

        //???
        _cellPool.ForEach((RectTransform cell) => cell.anchoredPosition += n * Vector2.up * _cellPool[_topMostCellIndex].sizeDelta.y);
        _content.anchoredPosition -= n * Vector2.up * _cellPool[_topMostCellIndex].sizeDelta.y;
        _isRecycling = false;
        return -new Vector2(0, n * _cellPool[_topMostCellIndex].sizeDelta.y);
    }

    Vector2 MoveBottomtoTop()
    {
        _isRecycling = true;
        int n = 0;
        float posY = 0;
        float posX = 0;
        while (_cellPool[_bottomMostCellIndex].MaxY() < _recycleViewBounds.min.y && _currentItemCount > _cellPool.Count)
        {
            posY = _cellPool[_topMostCellIndex].anchoredPosition.y + _cellPool[_topMostCellIndex].sizeDelta.y;
            _cellPool[_bottomMostCellIndex].anchoredPosition = new Vector2(_cellPool[_bottomMostCellIndex].anchoredPosition.x, posY);
            n++;

            _currentItemCount--;
            Data.SetCell(_cachedCells[_bottomMostCellIndex], _currentItemCount - _cellPool.Count);

            _topMostCellIndex = _bottomMostCellIndex;
            _bottomMostCellIndex = (_bottomMostCellIndex - 1 + _cellPool.Count) % _cellPool.Count; //???

        }

        //???
        _cellPool.ForEach((RectTransform cell) => cell.anchoredPosition -= n * Vector2.up * _cellPool[_topMostCellIndex].sizeDelta.y);
        _content.anchoredPosition += n * Vector2.up * _cellPool[_topMostCellIndex].sizeDelta.y;
        _isRecycling = false;
        return new Vector2(0, n * _cellPool[_topMostCellIndex].sizeDelta.y);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_recycleViewBounds.min - new Vector3(2000, 0), _recycleViewBounds.min + new Vector3(2000, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_recycleViewBounds.max - new Vector3(2000, 0), _recycleViewBounds.max + new Vector3(2000, 0));
    }
}
