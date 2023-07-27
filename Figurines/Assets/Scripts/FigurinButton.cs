
using r_core.core;
using r_core.utils.messages;

public class FigurinButton : ButtonApp
{
    private R_MessageFigurinButton message_figurin = new R_MessageFigurinButton((uint)GameEnums.Senders.FigurinButton);

    private GameEnums.TipoFigurin tipo_figurin;
    private GameEnums.TipoPose tipo_pose;

    public void SetValues(string sprite, string label, GameEnums.TipoFigurin tipo_figurin, GameEnums.TipoPose tipo_pose)
    {
        this.sprite.spriteName = sprite;
        this.label.text = label;
        this.tipo_figurin = tipo_figurin;
        this.tipo_pose = tipo_pose;
    }

    public GameEnums.TipoFigurin GetTipoFigurin()
    {
        return tipo_figurin;
    }

    public GameEnums.TipoPose GetTipoPose()
    {
        return tipo_pose;
    }

    public override void OnClickButton()
    {
        //Reproducimos sonido
        R_Core.GetInstance().PlaySound("tap_button", 1.0f);

        message_figurin.SetData((uint)GameEnums.Senders.FigurinButton, tipo_figurin, sprite.spriteName);
        R_MessagesController<R_MessageFigurinButton>.Post((int)GameEnums.MessagesTypes.PressFigurinButton, message_figurin);
    }
}
