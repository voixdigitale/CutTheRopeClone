using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMouth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Candy")) {
            GetComponentInParent<Animator>().SetTrigger("OpenMouth");
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Candy")) {
            GetComponentInParent<Animator>().SetTrigger("CloseMouth");
        }
    }
}
