using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawBookController : MonoBehaviour {
    [SerializeField]
    private GameObject text;
    private GameObject insText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            insText = Instantiate(text, transform);
            var tMesh = insText.GetComponent<TextMesh>();
            tMesh.text = "'L' to read";
            tMesh.characterSize = 2;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(insText);
    }
}
