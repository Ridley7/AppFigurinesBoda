using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using r_core.core;

public class SettingsMenu : MonoBehaviour
{
    [Header("Espacio entre los items del menu")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Rotacion del menu principal")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;

    UIButton mainButton;
    SettingsMenuItem[] menuItems;
    bool isExpanded = false;
    [SerializeField] private UIButton closeButton;
    [SerializeField] private UIButton configButton;

    [Space]
    [Header("Animacion")]
    [SerializeField] float expandDuration;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;

    Vector2 mainButtonPosition;
    int itemsCount;

    // Start is called before the first frame update
    void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];

        //Obtenemos todos los botones hijos
        for(var i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        //Obtenemos el boton principal
        mainButton = transform.GetChild(0).GetComponent<UIButton>();
        EventDelegate.Add(mainButton.onClick, ToggleMenu);

        mainButtonPosition = mainButton.transform.position;

        

        ResetPositions();

    }

    private void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].transform.position = mainButtonPosition;
        }
    }

    private void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            //Reproducimos sonido de open menu
            R_Core.GetInstance().PlaySound("open_menu", 1.0f);

            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].transform.position = mainButtonPosition + spacing * (i + 1);
                //Desplazamos con DOTween
                menuItems[i].transform.DOMove(mainButtonPosition + spacing * (i + 1), expandDuration).SetEase(expandEase);
                //Aplicamos Fade con NGUI
                TweenAlpha.Begin(menuItems[i].transform.gameObject, expandFadeDuration, 1.0f);
                
            }

            //Rotamos el menu principal
            mainButton.transform
                .DORotate(Vector3.forward * 180.0f, rotationDuration)
                .From(Vector3.zero)
                .SetEase(rotationEase);

            //Hacemos que aparezca el boton cerrar
            closeButton.gameObject.SetActive(true);

            //Desplazamos con DOTween
            configButton.transform.DOLocalMove(new Vector2(464.1f, 1094.5f), expandDuration).SetEase(expandEase);
            closeButton.transform.DOLocalMove(new Vector2(464.1f, 942.2f), expandDuration).SetEase(expandEase);

        }
        else
        {
            //Reproducimos sonido de close menu
            R_Core.GetInstance().PlaySound("close_menu", 1.0f);

            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].transform.position = mainButtonPosition;
                menuItems[i].transform.DOMove(mainButtonPosition, collapseDuration).SetEase(collapseEase);
                //Aplicamos Fade con NGUI
                TweenAlpha.Begin(menuItems[i].transform.gameObject, collapseFadeDuration, 0.0f);
            }

            //Rotamos el menu principal
        mainButton.transform
            .DORotate(Vector3.zero, rotationDuration)
            .From(Vector3.forward * 180.0f)
            .SetEase(rotationEase);

            //Hacemos desaparecer el boton cerrar
            configButton.transform.DOLocalMove(new Vector2(615.5f, 1094.5f), expandDuration).SetEase(expandEase);
            closeButton.transform.DOLocalMove(new Vector2(615.5f, 942.2f), expandDuration).SetEase(expandEase);
        }

        
    }

    private void OnDestroy()
    {
        EventDelegate.Remove(mainButton.onClick, ToggleMenu);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
