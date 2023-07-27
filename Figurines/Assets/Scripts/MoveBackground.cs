using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private UITexture textura;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        textura = GetComponent<UITexture>();
    }

    private void Update()
    {
        offset += 0.0001f;
        Rect rect = new Rect(offset, 0.0f, textura.uvRect.width, textura.uvRect.height);
        textura.uvRect = rect;
    }

}
