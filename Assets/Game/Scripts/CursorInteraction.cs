using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorInteraction : MonoBehaviour
{
    private int multiplier;
    private BezierFollow bezierFollow;
    public bool tapTimingStopped = false;
    public GameObject tapTimingBar;
   
    private void Update()
    {
        /*if (GameManager.Instance.currentState == GameManager.GameState.TapTiming)
        {
            


           

                //bezierFollow.speedModifier = 0;
           

            tapTimingStopped = true;
        }*/
       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (tapTimingStopped==true)
        {
            
        }*/
        print("collided");
        if (other.gameObject.tag == "Two")
        {
            multiplier = 2;
            print(multiplier);
            print("collided");
        }
        else if (other.gameObject.tag == "Three")
        {
            multiplier = 3;
            print(multiplier);
            print("collided");
        }
        else if (other.gameObject.tag == "Five")
        {
            multiplier = 5;
            print(multiplier);
            print("collided");
        }
        else if (other.gameObject == null)
        {
            multiplier = 2;
            print(multiplier);
            print("collided");

        }

        GameManager.Instance.globalCollectedStack *= multiplier;
        GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
        GameManager.Instance.currentState = GameManager.GameState.Victory;

        //tapTimingActive = false;
    }
}
