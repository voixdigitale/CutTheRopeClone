using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Rope : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer1;
    [SerializeField] LineRenderer _lineRenderer2;
    [SerializeField] HingeJoint2D _hook;
    [SerializeField] int _ropeLength;
    [SerializeField] GameObject _linkPrefab;
    [SerializeField] Candy _candy;

    List<Transform> _rope1Points = new List<Transform>();
    List<Transform> _rope2Points = new List<Transform>();
    bool _ropeIsCut = false;

    private void Start() {
        GenerateRope();
    }
    
    void GenerateRope() {
        Rigidbody2D previousRB = _hook.GetComponent<Rigidbody2D>();
        _rope1Points.Add(_hook.transform);

        Vector2 distanceToWeight = _candy.transform.position - _hook.transform.position;
        Vector2 increment = distanceToWeight / _ropeLength;

        for (int i = 0; i < _ropeLength - 1; i++) {
            HingeJoint2D previousJoint = previousRB.GetComponent<HingeJoint2D>();
            Vector2 spawnPoint = (Vector2) previousRB.transform.position + increment;

            GameObject link = Instantiate(_linkPrefab, spawnPoint, Quaternion.identity, transform);
            _rope1Points.Add(link.transform);

            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            previousRB = link.GetComponent<Rigidbody2D>();
        }
        _candy.AttachRope(previousRB);
        _rope1Points.Add(_candy.transform);
    }

    private void Update() {
        _lineRenderer1.positionCount = _rope1Points.Count;
        _lineRenderer1.SetPositions(_rope1Points.Select(x => x.position).ToArray());

        _lineRenderer2.positionCount = _rope2Points.Count;
        _lineRenderer2.SetPositions(_rope2Points.Select(x => x.position).ToArray());
    }

    public void SplitRope(Transform transform) {
        if (_ropeIsCut)
            return;

        _ropeIsCut = true;

        int index = _rope1Points.IndexOf(transform);
        if (index != -1) {
            _rope2Points = _rope1Points.GetRange(index, _rope1Points.Count - index);
            _rope1Points.RemoveRange(index, _rope1Points.Count - index);
        }
        transform.GetComponent<HingeJoint2D>().enabled = false;
    }
}
