using r_core.utils.messages;

public class R_MessageCabezaButton : R_Message
{
    public GameEnums.TipoAmigo cabeza_amigo { get; private set; }
    public string name_sprite_head { get; private set; }

    public R_MessageCabezaButton()
    {
        IsLocal = true;
        SenderId = 0;
    }

    public R_MessageCabezaButton(uint _senderId)
    {
        IsLocal = true;
        SenderId = _senderId;
    }

    public R_MessageCabezaButton (uint _senderId, GameEnums.TipoAmigo cabezaAmigo): base(_senderId)
    {
        IsLocal = true;
        cabeza_amigo = cabezaAmigo;
    }

    public void SetData(uint _senderId, GameEnums.TipoAmigo cabezaAmigo, string name_sprite_head)
    {
        IsLocal = true;
        SenderId = _senderId;
        cabeza_amigo = cabezaAmigo;
        this.name_sprite_head = name_sprite_head;
    }

}
