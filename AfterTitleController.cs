using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterTitleController : MonoBehaviour {
    private const int waitTime = 180;
    private int timer;
    private bool ready;
    public GameObject canvas;
    public Text startText;
	// Use this for initialization
	void Start () {
        timer = 0;
        ready = false;
        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        timer++;
        if(timer > waitTime)
        {
            ready = true;
            canvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ready)
        {
            SceneManager.LoadScene(2);
        }

        startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, Mathf.PingPong(Time.time, 1));
    }
}
