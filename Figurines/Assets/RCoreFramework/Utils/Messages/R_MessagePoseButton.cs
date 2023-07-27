using r_core.utils.messages;

public class R_MessagePoseButton : R_Message
{
    public GameEnums.TipoPose tipo_pose { get; private set; }

    public R_MessagePoseButton(uint senderId)
    {
        IsLocal = true;
        SenderId = senderId;
    }

    public void SetData(uint senderId, GameEnums.TipoPose tipo_pose)
    {
        IsLocal = true;
        SenderId = senderId;
        this.tipo_pose = tipo_pose;
    }


}
