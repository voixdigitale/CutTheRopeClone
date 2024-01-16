using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct CandyHinges {
    public Rope rope;
    public HingeJoint2D hinge;
}

public class Candy : MonoBehaviour
{
    private List<CandyHinges> _hinges = new List<CandyHinges>();

    public void AttachRope(Rigidbody2D ropeBit) {
        HingeJoint2D hinge = transform.AddComponent<HingeJoint2D>();
        hinge.connectedBody = ropeBit;
        _hinges.Add(new CandyHinges { rope = ropeBit.GetComponentInParent<Rope>(), hinge = hinge });
    }

    public void DetachRope(Rope rope) {
        foreach(CandyHinges hinge in _hinges) {
            if (hinge.rope == rope) {
                Destroy(hinge.hinge);
                _hinges.Remove(hinge);
                return;
            }
        }
    }
}
