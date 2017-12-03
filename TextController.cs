using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour {
    Renderer text;
    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Renderer>();
        text.sortingLayerName = "Message";
    }
}
