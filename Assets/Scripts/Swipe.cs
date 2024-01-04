using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Rope")) {
            collision.GetComponentInParent<Rope>().SplitRope(collision.transform);
        }
    }
}
