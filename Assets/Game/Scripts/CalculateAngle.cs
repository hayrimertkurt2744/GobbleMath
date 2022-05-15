using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateAngle : MonoBehaviour
{
    [SerializeField]
    private Transform movingTarget;
    //[SerializeField]
    //private Transform targetZero;
    [SerializeField]
    private Transform targetOne;
    [SerializeField]
    private Transform targetTwo;
    [SerializeField]
    private Transform targetThree;
    [SerializeField]
    private Transform targetFour;
    [SerializeField]
    private Transform targetFive;
    [SerializeField]
    private Vector2 endPoint;
    [SerializeField]
    private Vector2 startPoint;
    
   
    //private TextAlignment angleText;
    [SerializeField] private float angle = 0;
    [SerializeField] private float angleOne = 0;
    [SerializeField] private float angleTwo = 0;
    [SerializeField] private float angleThree = 0;
    [SerializeField] private float angleFour = 0;
    [SerializeField] private float angleFive = 0;
    

    private Vector2 directionMoving;
    //private Vector2 directionZero;
    private Vector2 directionOne;
    private Vector2 directionTwo;
    private Vector2 directionThree;
    private Vector2 directionFour;
    private Vector2 directionFive;


    private void Start()
    {
        directionOne = targetOne.position - transform.position;
        directionTwo = targetTwo.position - transform.position;
        directionThree = targetThree.position - transform.position;
        directionFour = targetFour.position - transform.position;
        directionFive = targetFive.position - transform.position;

        angleOne = Vector2.Angle(endPoint, directionOne);
        angleTwo = Vector2.Angle(endPoint, directionTwo);
        angleThree = Vector2.Angle(endPoint, directionThree);
        angleFour = Vector2.Angle(endPoint, directionFour);
        angleFive = Vector2.Angle(endPoint, directionFive);

    }

    void Update()
    {
        directionMoving = movingTarget.position - transform.position;
        //directionZero =targetZero.position - transform.position;
       

        angle = Vector2.Angle(endPoint, directionMoving);

        //angleZero= angleOne = Vector2.Angle(endPoint, directionOne);
        
        if (GameManager.Instance.globalSpeedModifier==0f)
        {
            if (0<= angle && angle<= angleFive)
            {
                print("x2");
            }
            else if (angleFive < angle && angle <= angleFour)
            {
                print("x3");
            }
            else if (angleFour < angle && angle <= angleThree)
            {
                print("x5");
            }
            else if (angleThree < angle && angle <= angleTwo)
            {
                print("x3");
            }
            else if (angleTwo < angle && angle <= angleOne)
            {
                print("x2");
            }
            else if (angleOne<angle)
            {
                print("x2");
            }
        }
       
        
    }
}
