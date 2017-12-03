using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {
    public static SaveController instance;
    public int murder;
    public int accidentalMurder;
    public int bodilyHarm;
    public int accidentalBodilyHarm;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
