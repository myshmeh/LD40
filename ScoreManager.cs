using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Text resulttxt;
    public Text crimetxt;
    public Text scoretxt;
    private bool clear;
    private const int bonus = 200;
	// Use this for initialization
	void Start () {
        //exception
        if (SaveController.instance == null) return;

        int murder = SaveController.instance.murder;
        int aMurder = SaveController.instance.accidentalMurder;
        int bHarm = SaveController.instance.bodilyHarm;
        int aBHarm = SaveController.instance.accidentalBodilyHarm;

        int scoreAMurder = aMurder * -100;
        int scoreBHarm = bHarm * -50;
        int scoreABHarm = aBHarm * -5;

        int rewardMurder = 0;
        if ((murder > 0) || (aMurder > 0)) rewardMurder = 200;//(murder+aMurder) * 200 - scoreAMurder;
        int rewardHarm = 0;
        if (bHarm + aBHarm >= 3) rewardHarm = 30;//(bHarm+aBHarm) * 30 - scoreBHarm - scoreABHarm;

        print("reward murder " + rewardMurder);
        print("reward harm " + rewardHarm);

        clear = false;
        //result text
        if((aMurder == 1) && (bHarm+aBHarm == 3) && (murder == 0))
        {
            resulttxt.text = "Mission Complete!";
            clear = true;
        }
        else
        {
            resulttxt.text = "Mission Failed...";
            clear = false;
        }

        //crime text
        string prisontext = "";
        if (murder > 0) prisontext = (murder * 10) + " year imprisonment";
        else prisontext = "no imprisonment";
        crimetxt.text = "Art.1 Murder: 10 year imprisonment X " + murder + " = " + prisontext + "\n" +
                        "Art.2 Manslaughter: 100G fine X " + aMurder + " = " + scoreAMurder + "G\n" +
                        "Art.3 Bodily Harm: 50G fine X " + bHarm + " = " + scoreBHarm + "G\n" +
                        "Art.4 Accidental Bodily Harm: 10G fine X" + aBHarm + " = " + scoreABHarm + "G\n" +
                        "Reward for 1 Murder: "+rewardMurder+"G\n"+
                        "Reward for 3 Injury: "+rewardHarm+"G";
        if(clear) crimetxt.text += "\nBonus from Boss: "+bonus+"G";
        if (clear) scoretxt.text = "Score: " + (rewardMurder + rewardHarm + scoreAMurder + scoreBHarm + scoreABHarm + bonus);
        else scoretxt.text = "Score: " + (rewardMurder + rewardHarm + scoreAMurder + scoreBHarm + scoreABHarm);
    }
	
	// Update is called once per frame
	void Update () {
        resulttxt.color = new Color(resulttxt.color.r, resulttxt.color.g, resulttxt.color.b, Mathf.PingPong(Time.time*0.5f, 0.5f)+0.5f);
	}
}
