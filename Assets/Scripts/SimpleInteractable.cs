using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color baseColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    public void Highlight(bool on)
    {
        rend.material.color = on ? highlightColor : baseColor;
    }
}
