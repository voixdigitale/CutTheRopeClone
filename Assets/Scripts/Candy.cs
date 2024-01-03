using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public void AttachRope(Rigidbody2D ropeBit) {
        transform.AddComponent<HingeJoint2D>().connectedBody = ropeBit;
    }
}
