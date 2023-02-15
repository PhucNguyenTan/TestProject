using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BtnData
{
    public Color Color;
    public string Name;
    public bool isSelected;
}

public class Data : MonoBehaviour, IDatasource
{
    [SerializeField] RecycleScroll _recycleScroll;

    [SerializeField] int _dataLength;

    List<BtnData> _listBtnData = new List<BtnData>();

    private void Awake()
    {
        InitData();
        _recycleScroll.Data = this;
    }

    void InitData()
    {
        if (_listBtnData != null) _listBtnData.Clear();

        for (var i = 0; i < _dataLength; i++)
        {
            BtnData btn = new BtnData();
            btn.Name = $"Cube_{i}";
            btn.Color = Random.ColorHSV();
            btn.isSelected = false;
            _listBtnData.Add(btn);
        }
    }

    public int GetItemCount()
    {
        return _listBtnData.Count;
    }

    public void SetCell(ICell cell, int index)
    {
        var item = cell as Cell; //???
        item.ConfigCell(_listBtnData[index], index);
    }
}
