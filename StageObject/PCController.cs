using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour {
    [SerializeField]
    private GameObject text;
    private GameObject insText;
        
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        insText = Instantiate(text, transform.position, Quaternion.identity);
        var tMesh = insText.gameObject.GetComponent<TextMesh>();
        tMesh.text = "L";
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(insText);
    }
}
