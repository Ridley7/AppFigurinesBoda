using r_core.core;
using r_core.utils.messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaButton : ButtonApp
{
    private R_MessageCabezaButton message_cabeza = new R_MessageCabezaButton((uint)GameEnums.Senders.CabezaButton);

    private GameEnums.TipoAmigo tipo_amigo;

    public void SetValues(string sprite, string label, GameEnums.TipoAmigo tipo_amigo)
    {
        this.sprite.spriteName = sprite;
        this.label.text = label;
        this.tipo_amigo = tipo_amigo;
    }

    public override void OnClickButton()
    {
        //Reproducimos sonido
        R_Core.GetInstance().PlaySound("tap_button", 1.0f);

        message_cabeza.SetData((uint)GameEnums.Senders.CabezaButton, tipo_amigo, sprite.spriteName);
        R_MessagesController<R_MessageCabezaButton>.Post((int)GameEnums.MessagesTypes.PressHeadButton, message_cabeza);
    }
}
