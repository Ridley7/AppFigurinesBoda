using r_core.utils.messages;
public class R_MessageFigurinButton : R_Message
{
    public GameEnums.TipoFigurin tipo_figurin { get; private set; }
    public string name_sprite_figurin { get; private set; }

    public R_MessageFigurinButton()
    {
        IsLocal = true;
        SenderId = 0;
    }

    public R_MessageFigurinButton(uint _senderId)
    {
        IsLocal = true;
        SenderId = _senderId;
    }

    public R_MessageFigurinButton(uint _senderId, GameEnums.TipoFigurin tipoFigurin) : base(_senderId)
    {
        IsLocal = true;
        this.tipo_figurin = tipoFigurin;
    }

    public void SetData(uint _senderId, GameEnums.TipoFigurin tipoFigurin, string name_sprite_figurin)
    {
        IsLocal = true;
        SenderId = _senderId;
        tipo_figurin = tipoFigurin;
        this.name_sprite_figurin = name_sprite_figurin;
    }
}
