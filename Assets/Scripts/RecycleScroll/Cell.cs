using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Cell : MonoBehaviour, ICell
{

    [SerializeField] TextMeshProUGUI _name;
    Button _btn;
    BtnData _data;
    int _cellIndex;

    public static UnityAction<Color> OnClickColor;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ButtonListener);
    }


    private void OnDisable()
    {
        _btn.onClick.RemoveListener(ButtonListener);

    }

    public void ConfigCell(BtnData data, int index) {

        _cellIndex = index;
        _data = data;

        _name.text = _data.Name;
        _name.color = _data.Color;
    }

    public void ButtonListener()
    {
        _btn.image.color = Color.yellow;
        OnClickColor?.Invoke(_data.Color);
    }

}
