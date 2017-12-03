using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class RoomManagerStage1 : RoomManager {
    private Subject subject;
    [SerializeField]
    private GameObject canvas;

    private int accidentalBodilyHarm;
    private List<int> abhIds;
    private int bodilyHarm;
    private List<int> bhIds;
    private int murder;
    private List<int> mIds;
    private int accidentalMurder;

    [SerializeField]
    private Text scoretxt; 

    private void Start()
    {
        subject = new Subject();
        subject.AddObserver(canvas.GetComponent<IObserver>());
        subject.AddObserver(GameObject.FindWithTag("Player").GetComponent<IObserver>());

        accidentalBodilyHarm = 0;
        bodilyHarm = 0;
        murder = 0;
        accidentalMurder = 0;
        abhIds = new List<int>();
        bhIds = new List<int>();
        mIds = new List<int>();
    }

    private void Update()
    {
        scoretxt.text = "injury: " + (accidentalBodilyHarm + bodilyHarm) + ", murder: " + (murder + accidentalMurder);

        if(accidentalBodilyHarm+bodilyHarm+murder+accidentalMurder == 4)
        {
            print("finish!!");
            SaveController.instance.murder = murder;
            SaveController.instance.accidentalMurder = accidentalMurder;
            SaveController.instance.bodilyHarm = bodilyHarm;
            SaveController.instance.accidentalBodilyHarm = accidentalBodilyHarm;
            SceneManager.LoadScene(3);
        }
    }

    protected override void ProcessOnNotify(GameObject entity, EventMessage eventMsg, List<object> data)
    {
        if(eventMsg == EventMessage.PLAYER_OPENLAWBOOK)
        {
            subject.Notify(gameObject, EventMessage.ROOMMANAGER_WAKECANVAS);
        }

        if(eventMsg == EventMessage.CANVAS_DEACTIVATED)
        {
            subject.Notify(gameObject, EventMessage.ROOMMANAGER_WAKEUPPLAYER);
        }

        if(eventMsg == EventMessage.ENEMY_BANANAED)
        {
            abhIds.Add((int)data[0]);
            accidentalBodilyHarm++;
        }

        if(eventMsg == EventMessage.ENEMY_ATTACKED)
        {
            bhIds.Add((int)data[0]);
            bodilyHarm++;
        }

        if(eventMsg == EventMessage.ENEMY_KILLED)
        {
            bool alreadythere = false;
            foreach(int val in abhIds)
            {
                if (val == (int)data[0])
                {
                    alreadythere = true;
                    accidentalBodilyHarm--;
                }
            }
            foreach(int val in bhIds)
            {
                if (val == (int)data[0])
                {
                    alreadythere = true;
                    bodilyHarm--;
                }
            }

            mIds.Add((int)data[0]);
            murder++;
        }
        else if (eventMsg == EventMessage.ENEMY_NEEDLE)
        {
            bool alreadythere = false;
            foreach (int val in abhIds)
            {
                if (val == (int)data[0])
                {
                    alreadythere = true;
                    accidentalBodilyHarm--;
                }
            }
            foreach (int val in bhIds)
            {
                if (val == (int)data[0])
                {
                    alreadythere = true;
                    bodilyHarm--;
                }
            }

            mIds.Add((int)data[0]);
            accidentalMurder++;
        }
    }
}
