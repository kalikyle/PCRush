using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public Renderer background;
    public float speed;

    private void Update()
    {
        background.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
    
}
