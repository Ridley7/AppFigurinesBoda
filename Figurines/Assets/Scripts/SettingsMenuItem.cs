using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{
    [HideInInspector] public UISprite sprite;
    [HideInInspector] public Transform trans;

    private void Awake()
    {
        sprite = GetComponent<UISprite>();
        trans = transform;

    }

}
