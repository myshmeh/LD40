using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class RoomManagerMenu : RoomManager {
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject email;
    [SerializeField]
    private GameObject bankaccount;
    [SerializeField]
    private GameObject close;
    [SerializeField]
    private GameObject arrow;

    //timer
    private int[] timer;

    //IO
    private KeyManager kMgr;

    //PC related
    private bool pcRunning;
    private int index;
    private const int indexMax = 2;
    private const int menuSpace = 50;
    private const int menuYOffset = -25;

    private void Start()
    {
        DeactivatePc();
        //IO
        kMgr = new KeyManager();
        //pc related
        pcRunning = false;
        index = 0;
        //timer
        timer = new int[State.Length];
    }

    private void Update()
    {
        kMgr.UpdateKey();
        UpdateTimer();
        if (pcRunning)
        {
            ProcessPcRunning();
        }
    }

    void AddIndex()
    {
        if((index+1) <= indexMax)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

    void DecIndex()
    {
        if((index-1) >= 0)
        {
            index--;
        }
        else
        {
            index = indexMax;
        }
    }

    void ProcessPcRunning()
    {
        //key update
        if (kMgr.IsPressed(KeyBit.down) && (GetTimerState(0) <= 0))
        {
            SetTimer(0, 20);
            AddIndex();
            print(index);
        }
        if (kMgr.IsPressed(KeyBit.up) && (GetTimerState(1) <= 0))
        {
            SetTimer(1, 20);
            DecIndex();
            print(index);
        }

        //update arrow position
        var rTransform = arrow.GetComponent<RectTransform>();
        rTransform.localPosition = new Vector3(0, -menuSpace*index);
    }

    private void DeactivatePc()
    {
        panel.SetActive(false);
        email.SetActive(false);
        bankaccount.SetActive(false);
        close.SetActive(false);
        arrow.SetActive(false);
        pcRunning = false;
    }

    private void ActivatePc()
    {
        panel.SetActive(true);
        email.SetActive(true);
        bankaccount.SetActive(true);
        close.SetActive(true);
        arrow.SetActive(true);
        pcRunning = true;
    }

    protected override void ProcessOnNotify(GameObject entity, EventMessage eventMsg, List<object> data)
    {
        if(eventMsg == EventMessage.PLAYER_CALLPC)
        {
            ActivatePc();
        }
    }

    /////////////////////////////////////////////////
    //Sub Functions
    /////////////////////////////////////////////////
    /// <summary>
    /// Sets given state index ready(-1)
    /// Use State stataic class for the parameter
    /// </summary>
    /// <param name="timerIndex">indec of state to initialize</param>
    protected void InitTimer(int timerIndex)
    {
        if (timerIndex > State.Length) return; //exception handling
        timer[timerIndex] = -1;
    }
    /// <summary>
    /// Sets timer in given index
    /// </summary>
    /// <param name="timerIndex">state index to set timer</param>
    /// <param name="time">timer duration</param>
    protected void SetTimer(int timerIndex, int time)
    {
        if ((time < 0) || (timerIndex > State.Length)) return; //exception handling
        timer[timerIndex] = time;
    }
    /// <summary>
    /// Decreases timer for each state by 1
    /// </summary>
    protected void UpdateTimer()
    {
        for (int i = 0; i < timer.Length; i++)
        {
            if (timer[i] > 0)
                timer[i]--;
        }
    }
    /// <summary>
    /// Determines whether or not given timer is stopped(0)
    /// </summary>
    /// <param name="timerIndex"></param>
    /// <returns>returns true if stopped</returns>
    protected bool IsTimerStopped(int timerIndex)
    {
        if (timerIndex > State.Length) return false; //exception handling

        if (timer[timerIndex] == 0)
            return true;

        return false;
    }

    /// <summary>
    /// Determines whether or not given timer is ready(-1)
    /// </summary>
    /// <param name="timerIndex"></param>
    /// <returns>returns true if stopped</returns>
    protected bool IsTimerReady(int timerIndex)
    {
        if (timerIndex > State.Length) return false; //exception handling

        if (timer[timerIndex] == -1)
            return true;

        return false;
    }

    protected int GetTimerState(int timerIndex)
    {
        if (timerIndex > State.Length) return -2; //exception handling

        return timer[timerIndex];
    }
}
