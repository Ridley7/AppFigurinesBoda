using r_core.core;
using r_core.utils.messages;

public class PoseButton : ButtonApp
{
    private R_MessagePoseButton message_pose = new R_MessagePoseButton((uint)GameEnums.Senders.PoseButton);

    public GameEnums.TipoPose tipo_pose { get; private set; }

    public void SetValues(string sprite, string label, GameEnums.TipoPose tipo_pose)
    {
        this.sprite.spriteName = sprite;
        this.label.text = label;
        this.tipo_pose = tipo_pose;
    }

    public override void OnClickButton()
    {
        //Reproducimos sonido
        R_Core.GetInstance().PlaySound("tap_button", 1.0f);

        message_pose.SetData((uint)GameEnums.Senders.PoseButton, tipo_pose);
        R_MessagesController<R_MessagePoseButton>.Post((int)GameEnums.MessagesTypes.PressPoseButton, message_pose);
    }
}
