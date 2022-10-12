using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    [SerializeField] Button _btnCube;
    [SerializeField] Button _btnSphere;
    [SerializeField] GameObject _cube;
    [SerializeField] GameObject _sphere;

    public static UnityAction OnBtnCubePress;
    public static UnityAction OnBtnSpherePress;

    private void Start()
    {
        PressOnCubeBtn();
    }

    public void PressOnCubeBtn()
    {
        _btnCube.interactable = false;
        _btnSphere.interactable = true;
        OnBtnCubePress?.Invoke();
        _cube.SetActive(true);
        _sphere.SetActive(false);


    }

    public void PressOnSphereBtn()
    {
        _btnCube.interactable = true;
        _btnSphere.interactable = false;
        OnBtnSpherePress?.Invoke();
        _cube.SetActive(false);
        _sphere.SetActive(true);
    }

    
}
