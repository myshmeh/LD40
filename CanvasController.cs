using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class CanvasController : MonoBehaviour, IObserver {
    private Subject subject;
    private bool added;
    private bool inLawbook;

	// Use this for initialization
	void Awake () {
        gameObject.SetActive(false);
        subject = new Subject();
        added = false;
        inLawbook = false;
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.L) && inLawbook)
        {
            print("f");
            subject.Notify(gameObject, EventMessage.CANVAS_DEACTIVATED);
            //DeactivateLawBook();
            gameObject.SetActive(false);
            inLawbook = false;
        }
	}

    void IObserver.OnNotify(GameObject entity, EventMessage eventMsg, List<object> data)
    {
        if(eventMsg == EventMessage.ROOMMANAGER_WAKECANVAS)
        {
            //ActivateLawBook();
            gameObject.SetActive(true);
            if (!added) subject.AddObserver(entity.GetComponent<IObserver>());
            inLawbook = true;
        }
    }
}
