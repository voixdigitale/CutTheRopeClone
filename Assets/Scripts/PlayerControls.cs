using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] Transform _swipeTransform;
    [SerializeField] ParticleSystem _touchParticles;
    [SerializeField] float _swipeActivationTime = .5f;

    bool _isTouching = false;
    Swipe _swipeScript;
    float _swipeStartTime = 0f;

    private void Awake() {
        _touchParticles.Stop();
        _swipeScript = _swipeTransform.GetComponent<Swipe>();
    }

    // Update is called once per frame
    void Update()
    {
        _swipeScript.ActivateCutting(false);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            _isTouching = true;
            _swipeStartTime = Time.time;
        }

        if (_isTouching) {
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPosition.z = 0;
            _swipeTransform.position = touchPosition;
            if (Time.time - _swipeStartTime > _swipeActivationTime)
                _swipeScript.ActivateCutting(true);
            if (!_touchParticles.isPlaying) _touchParticles.Play();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            _isTouching = false;
            _touchParticles.Stop();
        }
    }
}
