using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EyeBlinker : MonoBehaviour
{
    [SerializeField] private float blinkIntervalMin;
    [SerializeField] private float blinkIntervalMax;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink() {
        yield return new WaitForSeconds(Random.Range(blinkIntervalMin, blinkIntervalMax));
        GetComponent<Animator>().SetTrigger("Blink");
        StartCoroutine(Blink());
    }
}
