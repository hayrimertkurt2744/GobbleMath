using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   //when touch the finger or move the finger on the screen,these are not the same action.
        InputManager.Instance.onTouchStart += ProcessPlayerSwerve;
        InputManager.Instance.onTouchMove += ProcessPlayerSwerve;

    }
    private void OnDisable()
    {
        //when the player not on the scene ,the input detection must stop.
        InputManager.Instance.onTouchStart -= ProcessPlayerSwerve;
        InputManager.Instance.onTouchMove -= ProcessPlayerSwerve;
    }

 
    void Update()
    {
        ProcessPlayerMovement();
    }

    private void ProcessPlayerMovement()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
            GetComponent<Mover>().MoveTo(new Vector3(
             0f, 0f, GameManager.Instance.forwardSpeed));
        }
    }

    private void ProcessPlayerSwerve()
    {
        //clamp makes move to method to restrict player in a range.
        if (GameManager.Instance.currentState==GameManager.GameState.Normal)
        {//the all movement happen here.
           GetComponent<Mover>().MoveTo(new Vector3(
             InputManager.Instance.GetDirection().x * GameManager.Instance.horizontalSpeed,0f,0f));
        }
       
    }
    
}
