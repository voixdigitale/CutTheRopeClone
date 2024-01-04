using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] Transform _swipe;
    [SerializeField] ParticleSystem _touchParticles;

    bool _isTouching = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            _isTouching = true;
        }

        if (_isTouching) {
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPosition.z = 0;
            _swipe.position = touchPosition;    
            if (!_touchParticles.isPlaying) _touchParticles.Play();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            _isTouching = false;
            _touchParticles.Stop();
        }
    }
}
