using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        var offset = 10f;
        //var x = (player.transform.position.x - gameObject.transform.position.x) * offset;
        //var y = (player.transform.position.y - gameObject.transform.position.y) * offset;
        //gameObject.transform.position += new Vector3(x, y);
        Vector2 smoothedPosition = Vector2.Lerp(gameObject.transform.position, player.transform.position, offset*Time.deltaTime);
        gameObject.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
	}
}
