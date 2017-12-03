using UnityEngine;
using System;

public class Character : MonoBehaviour {
    /////////////////////////////////////////////////
    //Var Declaration
    /////////////////////////////////////////////////
    private int state; //controls player state (refer MyGameLib.cs)
    private int[] timer; //timer for each state (refer MyGameLib.cs for its array amount), 0: stopped, -1:ready, i>0: running
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected float speed;
    protected Vector2 vecSpeed;
    protected Direction dir;
    private int hp;
    //public vars
    public bool availability;

    /////////////////////////////////////////////////
    //Main
    /////////////////////////////////////////////////
    public virtual void Start()
    {
        state = State.Idle;
        timer = new int[State.Length*2]; //should define length of timer array in more solid way
        velocity = new Vector2();
        rb2d = GetComponent<Rigidbody2D>();
        speed = 70f;
        dir = Direction.Right;
        hp = 1;
        availability = true;

        Init();
        for(int i=0; i<timer.Length; i++)
        {
            InitTimer(i);
        }
    }

    protected virtual void Update()
    {
        //exception handling
        if (!availability) return;

        BeforeAnyUpdates();

        //update timer
        UpdateTimer();

        //update key input
        UpdateKeyInput();

        //state control
        switch (state)
        {
            case State.Dead:
                ProcessStateDead();
                break;
            case State.Idle:
                ProcessStateIdle();
                break;
            case State.Damaged:
                ProcessStateDamaged();
                break;
            case State.Attack:
                ProcessStateAttack();
                break;
            case State.Attack2:
                ProcessStateAttack2();
                break;
            case State.Guard:
                ProcessStateGuard();
                break;
            case State.Rest:
                ProcessStateRest();
                break;
            default:
                Debug.Log("Character::Update() >> invalid state("+state+") detected \nconsider State for state value");
                break;
        }

        //apply velocity to buit-in velocity on player
        Move(velocity);

        //initialize velocity to stop player constantly
        InitVelocity();

        AfterAllUpdates();
    }

    /////////////////////////////////////////////////
    //Functions Inside Start() and Update()
    /////////////////////////////////////////////////
    //*-- inside start() --*//
    protected virtual void Init() { }

    //*-- inside update() --*//
    protected virtual void BeforeAnyUpdates() { }

    protected virtual void UpdateKeyInput() { }
    protected virtual void ProcessStateDead() { }
    protected virtual void ProcessStateIdle() { }
    protected virtual void ProcessStateDamaged() { }
    protected virtual void ProcessStateAttack() { }
    protected virtual void ProcessStateAttack2() { }
    protected virtual void ProcessStateGuard() { }
    protected virtual void ProcessStateRest() { }

    protected virtual void AfterAllUpdates() { }

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

    protected virtual void Move(Vector2 velocity)
    {
        //take in account of angled move
        bool condition = (Math.Abs(velocity.x) > 0) && (Math.Abs(velocity.y) > 0);
        if (condition) velocity /= (float)Math.Sqrt(2);

        //parse pseudo-velocity to real one
        rb2d.velocity = velocity;
        //rb2d.AddForce(velocity);
    }

    protected void InitVelocity()
    {
        velocity = new Vector2();
    }

    protected bool IsDead()
    {
        if (hp == 0)
            return true;
        return false;
    }

    /////////////////////////////////////////////////
    //Propertties
    /////////////////////////////////////////////////
    public float X
    {
        get { return gameObject.transform.position.x; }
    }
    public float Y
    {
        get { return gameObject.transform.position.y; }
    }
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp < 0) hp = 0;
        }
    }
    public int StateAccess
    {
        get { return state; }
        set
        {
            if (value > State.Length || value < State.Dead)
                return;
            state = value;
        }
    }
    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public Vector2 VecSpeed
    {
        get { return vecSpeed; }
        set { vecSpeed = value; }
    }
}
