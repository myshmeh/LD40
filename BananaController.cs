using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaController : MonoBehaviour {
    [SerializeField]
    private GameObject text;
    private GameObject insText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (collision.gameObject.GetComponent<PlayerController>().item != Items.Banana))
        {
            insText = Instantiate(text, transform);
            var tMesh = insText.GetComponent<TextMesh>();
            tMesh.text = "'L' to pick";
            tMesh.characterSize = 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(insText);
    }
}
