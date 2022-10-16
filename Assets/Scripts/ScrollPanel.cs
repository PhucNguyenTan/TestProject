using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollPanel : MonoBehaviour
{
    [SerializeField] GameObject _contentCube;
    [SerializeField] GameObject _contentSphere;
    [SerializeField] Button _cubePrefabBtn;
    [SerializeField] Button _spherePrefabBtn;
    ScrollRect _scrollRect;


    List<Color> _listCubeColor;
    List<Color> _listSphereColor;
    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _listCubeColor = CreateRandomColorList();
        _listSphereColor = CreateRandomColorList();
        CreateButtonForObj(_contentSphere, _spherePrefabBtn, _listSphereColor);
        CreateButtonForObj(_contentCube, _cubePrefabBtn, _listCubeColor);
        EnableCubeContent();
    }

    private void OnEnable()
    {
        UI.OnBtnCubePress += EnableCubeContent;
        UI.OnBtnSpherePress += EnableSphereContent;
    }

    private void OnDisable()
    {
        UI.OnBtnCubePress -= EnableCubeContent;
        UI.OnBtnSpherePress -= EnableSphereContent;
    }

    void EnableCubeContent()
    {
        _scrollRect.content = _contentCube.GetComponent<RectTransform>();
        _contentCube.SetActive(true);
        _contentSphere.SetActive(false);
    }

    void EnableSphereContent()
    {
        _scrollRect.content = _contentSphere.GetComponent<RectTransform>();
        _contentCube.SetActive(false);
        _contentSphere.SetActive(true);
    }

    List<Color> CreateRandomColorList()
    {
        List<Color> list = new List<Color>();
        for (var i = 0; i < 50; i++)
        {
            list.Add(Random.ColorHSV(0f, 1f, 1f, .5f, 1f, .1f));
        }
        return list;
    }

    void CreateButtonForObj(GameObject obj, Button btnPrefab, List<Color> listColor)
    {
        int i = 1;
        foreach (Color color in listColor)
        {
            Button btn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, obj.transform);
            TextMeshProUGUI textUI = btn.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
            textUI.color = color;
            textUI.text = $"{textUI.text}_{i}";
            i++;

        }
    }
}
