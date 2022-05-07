using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t= 0; t <= 1; t+=0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 1) * controlPoints[0].position + t * controlPoints[1].position;
            Gizmos.DrawSphere(gizmosPosition, 1f);
        }
        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
    }

}
