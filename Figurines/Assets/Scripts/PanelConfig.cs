using r_core.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelConfig : MonoBehaviour
{
    public UIToggle toogleSound;
    public Transform cabezaFigurin;
    
    public void OnRestoreValues()
    {
        //Restauramos valores:
        //Restauramos player prefs
        PlayerPrefs.SetInt(FigurinesConstants.ANIMATION_INITIAL, (int)GameEnums.AnimacionInicial.NO_PLAYED);

        //Restauramos los sonidos
        R_Core.GetInstance().EnableSounds();
        toogleSound.value = true;

        //Ponemos la cabeza en un punto de 'origen'
        cabezaFigurin.localPosition = new Vector3(0f, 800f, 0f);

        //Desactivamos ventana
        transform.gameObject.SetActive(false);
    }

    public void OnClickBack()
    {

        //Comprobamos el estado del toogleSound
        if (toogleSound.value)
        {
            R_Core.GetInstance().EnableSounds();
        }
        else
        {
            R_Core.GetInstance().DisableSounds();
        }

        //Desactivamos ventana
        transform.gameObject.SetActive(false);
    }
}
