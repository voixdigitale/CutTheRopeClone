using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatCandy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Candy")) {
            GetComponentInParent<Animator>().SetTrigger("EatCandy");
            collision.gameObject.SetActive(false);
        }
    }
}
