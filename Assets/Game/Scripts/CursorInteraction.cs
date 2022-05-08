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
        if (GameManager.Instance.currentState == GameManager.GameState.TapTiming)
        {
            


            if (Input.GetKeyDown(KeyCode.Space) )
            {

                bezierFollow.speedModifier = 0;
            }

            tapTimingStopped = true;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (tapTimingStopped==true)
        {
            if (other.gameObject.tag == "Two")
            {
                multiplier = 2;
            }
            else if (other.gameObject.tag == "Three")
            {
                multiplier = 3;
            }
            else if (other.gameObject.tag == "Five")
            {
                multiplier = 5;
            }
            else if (other.gameObject == null)
            {
                multiplier = 2;

            }

            GameManager.Instance.globalCollectedStack *= multiplier;
            GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
        }
        
        //tapTimingActive = false;
    }
}
