using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonColor : MonoBehaviour
{
    //[SerializeField] GameObject _obj;
    Button _button;
    Color _color;
    Image _image;

    public static UnityAction<Color> OnClickColor;
    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _color = _button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color;
        _button.onClick.AddListener(SendColorData);
    }

    void SendColorData()
    {
        OnClickColor?.Invoke(_color);
    }
}
