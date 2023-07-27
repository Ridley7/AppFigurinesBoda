

namespace r_core.utils.messages
{

    public abstract class R_Message
    {

        //Variable que nos indica si hablamos de un aviso local
        //o para partidas multijugador
        public bool IsLocal { get; protected set; }

        //Identificador de quien envia el mensaje
        public uint SenderId { get; protected set; }

        //Constructores
        public R_Message()
        {
            IsLocal = true;
            SenderId = 0;
        }

        public R_Message(uint _senderId)
        {
            IsLocal = true;
            SenderId = _senderId;
        }
    }
}