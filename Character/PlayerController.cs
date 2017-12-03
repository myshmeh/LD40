using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public enum Items
{
    None,
    Banana
}

public class PlayerController : Character, IObserver {
    private KeyManager kMgr;
    private bool pcCollided;
    public Items item;
    private int attackTime;
    private Subject subject;

    [SerializeField]
    private GameObject banana;
    private bool inBanana;
    private GameObject colBanana;
    [SerializeField]
    private GameObject atkLayer;
    private GameObject insAtkLayer;

    private bool lawBookCollided;

    protected override void Init()
    {
        kMgr = new KeyManager();
        pcCollided = false;
        item = Items.None;
        attackTime = 20;
        inBanana = false;
        subject = new Subject();
        subject.AddObserver(GameObject.Find("RoomManager").GetComponent<IObserver>());
    }

    protected override void UpdateKeyInput()
    {
        kMgr.UpdateKey();
    }

    protected override void ProcessStateIdle()
    {
        if (kMgr.IsPressed(KeyBit.up))
        {
            velocity.y += speed;
            dir = Direction.Up;
        }
        if (kMgr.IsPressed(KeyBit.right))
        {
            velocity.x += speed;
            dir = Direction.Right;
        }
        if (kMgr.IsPressed(KeyBit.down))
        {
            velocity.y -= speed;
            dir = Direction.Down;
        }
        if (kMgr.IsPressed(KeyBit.left))
        {
            velocity.x -= speed;
            dir = Direction.Left;
        }

        if (kMgr.IsPressed(KeyBit.enter))
        {
            if (pcCollided)
            {
                print("open window");
                availability = false;
                subject.Notify(gameObject, EventMessage.PLAYER_CALLPC);
                //instantiate pc screen
            }
            else if (lawBookCollided)
            {
                subject.Notify(gameObject, EventMessage.PLAYER_OPENLAWBOOK);
                availability = false;
            }
            else if (item == Items.Banana)
            {
                print("banana placed");
                Instantiate(banana, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                item = Items.None;
            }
            else if (inBanana && (item != Items.Banana))
            {
                item = Items.Banana;
                print(item + " obtained");
                Destroy(colBanana);
                inBanana = false;
            }
            else //attacj otherwise
            {
                Vector2 pos = new Vector2(transform.position.x+16, transform.position.y-16);
                //get right rotaiton
                Quaternion rotation = Quaternion.identity;
                if(dir == Direction.Right) rotation = Quaternion.Euler(0, 0, 0);
                else if(dir == Direction.Left) rotation = Quaternion.Euler(0, 0, 180);
                else if (dir == Direction.Up) rotation = Quaternion.Euler(0, 0, 90);
                else if (dir == Direction.Down) rotation = Quaternion.Euler(0, 0, -90);
                //instantiate
                insAtkLayer = Instantiate(atkLayer, pos, rotation);
                StateAccess = State.Attack;
                SetTimer(0, attackTime);
            }
        }
    }

    protected override void ProcessStateAttack()
    {
        if (IsTimerStopped(0))
        {
            StateAccess = State.Idle;
            Destroy(insAtkLayer);
            InitTimer(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("PC"))
        {
            pcCollided = true;
        }
        if(collision.gameObject.name.Contains("Law"))
        {
            lawBookCollided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("PC"))
        {
            pcCollided = false;
        }
        if(collision.gameObject.name.Contains("Law"))
        {
            lawBookCollided = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Banana"))
        {
            inBanana = true;
            colBanana = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Banana"))
        {
            inBanana = false;
        }
    }

    void IObserver.OnNotify(GameObject entity, EventMessage eventMsg, List<object> data)
    {
        if(eventMsg == EventMessage.ROOMMANAGER_WAKEUPPLAYER)
        {
            availability = true;
        }
    }
}
