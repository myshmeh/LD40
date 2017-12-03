using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class EnemyController : Character {
    private bool collided;
    [SerializeField]
    private GameObject text;
    private Subject subject;
    public int id;
    private GameObject oops;
    
    protected override void Init()
    {
        speed = -75f;
        collided = false;
        subject = new Subject();
        subject.AddObserver(GameObject.FindWithTag("GameController").GetComponent<IObserver>());
        Hp = 2;
    }

    protected override void ProcessStateIdle()
    {
        velocity.x += speed;
        collided = false;
    }

    protected override void ProcessStateDamaged()
    {
        var ins = Instantiate(text, transform);
        ins.GetComponent<TextMesh>().text = "Oops";
    }

    protected override void ProcessStateDead()
    {
        print("dead...");
        var ins = Instantiate(text, transform);
        ins.gameObject.GetComponent<TextMesh>().text = "Dead...";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision") && !collided)
        {
            collided = true;
            speed *= -1;
        }
        //collided with attack layer
        if (collision.gameObject.name.Contains("Needle") && (StateAccess != State.Dead))
        {
            Hp = 0;
            List<object> data = new List<object>();
            data.Add(id);
            StateAccess = State.Dead;
            subject.Notify(gameObject, EventMessage.ENEMY_NEEDLE, data);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Banana") && (StateAccess != State.Damaged) && (StateAccess != State.Dead))
        {
            Hp--;
            StateAccess = State.Damaged;
            List<object> data = new List<object>();
            data.Add(id);
            subject.Notify(gameObject, EventMessage.ENEMY_BANANAED, data);
            var ins = Instantiate(text, transform);
            ins.GetComponent<TextMesh>().text = "Oops";
            Destroy(collision.gameObject);
        }

        //collided with attack layer
        if (collision.gameObject.name.Contains("AttackLayer") && (StateAccess != State.Dead))
        {
            Hp--;
            List<object> data = new List<object>();
            data.Add(id);
            if (Hp > 0) { StateAccess = State.Damaged; subject.Notify(gameObject, EventMessage.ENEMY_ATTACKED, data); }
            else if (Hp <= 0) { StateAccess = State.Dead; subject.Notify(gameObject, EventMessage.ENEMY_KILLED, data); }
        }

       
    }
}
