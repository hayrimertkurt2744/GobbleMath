using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject spoon;
    public GameObject objectToThrow;
    public Vector3 endPoint;
    [Header("Jump Parameters")]
    public float jumpDuration;
    public float jumpPower;
    public bool snapping = false;
    public int numOfJumps;
    private int count = 63;
    private int clickCount = 0;
    //[HideInInspector]public bool


}
