public class Slot
{
    public UISprite imagen { get; private set; }
    public bool on { get; private set; }

    public Slot(UISprite imagen, bool on)
    {
        this.imagen = imagen;
        this.on = on;
    }

    public void SetOn(string spriteName, bool modal)
    {
        imagen.spriteName = spriteName;
        on = modal;
    }
}