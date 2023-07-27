using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonApp : MonoBehaviour
{
    public UISprite sprite;
    public UILabel label;

    public abstract void OnClickButton();
}
