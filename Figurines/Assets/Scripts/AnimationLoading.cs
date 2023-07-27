using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using r_core.core;
using r_core.utils.messages;
using DG.Tweening;

public class AnimationLoading : MonoBehaviour
{
    public UISprite[] slotsSprites = new UISprite[5];
    private Slot[] slots = new Slot[5];
        
    public UISprite backgroundLoading;
    public UISprite logoElenaSoriano;
    [SerializeField] private UISprite logoTipoElenaSoriano;

    private R_MessageUI message_ui = new R_MessageUI((uint)GameEnums.Senders.AnimationLoading);

    private void Start()
    {
        
        if (PlayerPrefs.GetInt(FigurinesConstants.ANIMATION_INITIAL) == (int)GameEnums.AnimacionInicial.PLAYED)
        {
            R_MessagesController<R_MessageUI>.Post((int)GameEnums.MessagesTypes.EndProcessLoading, message_ui);
        }
        else
        {
            int length = slotsSprites.Length;

            for (int i = 0; i < length; i++)
            {
                slots[i] = new Slot(slotsSprites[i], false);
            }

            R_Core.GetInstance().StartTimer(0.8f, this, cacheAction =>
            {
                (cacheAction.Context as AnimationLoading).StartLoading();
            });
        }
    }

    private void StartLoading()
    {
        int lastIndex = FillSlot();

        if(lastIndex == -1 )
        {
            //Si obtenemos -1 continuamos con la aplicacion           
            
            TweenAlpha.Begin(backgroundLoading.transform.gameObject, 0.5f, 0.0f);
            TweenAlpha.Begin(logoElenaSoriano.transform.gameObject, 1.0f, 1.0f);

            R_Core.GetInstance().StartTimer(1.1f, this, cacheAction =>
            {
                (cacheAction.Context as AnimationLoading).AnimateLogoElenaSoriano();
            });

        }
        else
        {
            R_Core.GetInstance().StartTimer(1.0f, this, cacheAction =>
            {
                (cacheAction.Context as AnimationLoading).StartLoading();
            });
        }
    }

    private void AnimateLogoElenaSoriano()
    {
        logoElenaSoriano.transform.DOLocalMoveY(400f, 1.5f);
        TweenAlpha.Begin(logoTipoElenaSoriano.transform.gameObject, 1.3f, 1.0f);

        R_Core.GetInstance().StartTimer(1.6f, this, cacheAction =>
        {
            (cacheAction.Context as AnimationLoading).LoadNextScene();
        });
    }


    private void LoadNextScene()
    {
        //Guardamos PlayerPrefs
        PlayerPrefs.SetInt(FigurinesConstants.ANIMATION_INITIAL, (int)GameEnums.AnimacionInicial.PLAYED);

        R_MessagesController<R_MessageUI>.Post((int)GameEnums.MessagesTypes.EndProcessLoading, message_ui);
    }

    private int FillSlot()
    {
        int length = slots.Length;

        for(int i = 0; i < length; i++)
        {
            if (!slots[i].on)
            {
                slots[i].SetOn("squareWhite", true);

                return i;
            }            
        }

        return -1;
    }

}

