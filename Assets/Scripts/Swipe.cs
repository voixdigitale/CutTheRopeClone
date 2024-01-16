using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool _cutting;
    private Vector2 framePos;
    private Vector2 lastFramePos;

    private void Update() {
        lastFramePos = framePos;
        framePos = transform.position;

        if (_cutting) {
            CutByRaycast();
        }
    }

    private void CutByRaycast() {
        RaycastHit2D[] hits = Physics2D.LinecastAll(lastFramePos, framePos);
        foreach (RaycastHit2D hit in hits) {
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Rope")) {
                    hit.collider.GetComponentInParent<Rope>().SplitRope(hit.transform);
                }
            }
        }

        //Draw a line to debug the Linecast
        Debug.DrawLine(lastFramePos, framePos, Color.green, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Rope")) {
            collision.GetComponentInParent<Rope>().SplitRope(collision.transform);
        }
    }

    public void ActivateCutting(bool active) {
        _cutting = active;
    }
}
