using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rope : MonoBehaviour
{
    [SerializeField] HingeJoint2D _hook;
    [SerializeField] int _ropeLength;
    [SerializeField] GameObject _linkPrefab;
    [SerializeField] Candy _candy;

    List<Transform> _ropePoints = new List<Transform>();
    LineRenderer _lineRenderer;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start() {
        GenerateRope();
    }
    
    void GenerateRope() {
        Rigidbody2D previousRB = _hook.GetComponent<Rigidbody2D>();
        _ropePoints.Add(_hook.transform);

        Vector2 distanceToWeight = _candy.transform.position - _hook.transform.position;
        Vector2 increment = distanceToWeight / _ropeLength;

        for (int i = 0; i < _ropeLength - 1; i++) {
            HingeJoint2D previousJoint = previousRB.GetComponent<HingeJoint2D>();
            Vector2 spawnPoint = (Vector2) previousRB.transform.position + increment;

            GameObject link = Instantiate(_linkPrefab, spawnPoint, Quaternion.identity, transform);
            _ropePoints.Add(link.transform);

            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            previousRB = link.GetComponent<Rigidbody2D>();
        }
        _candy.AttachRope(previousRB);
        _ropePoints.Add(_candy.transform);
    }

    private void Update() {
        _lineRenderer.positionCount = _ropePoints.Count;
        _lineRenderer.SetPositions(_ropePoints.Select(x => x.position).ToArray());
    }
}
