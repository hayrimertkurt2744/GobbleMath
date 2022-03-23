using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject stackSpoon;
    public GameObject[] stackList;
    private int currentStackListNumber=0;
    private bool isPlayerHasLastChance=false;
    

    // Start is called before the first frame update
    void Start()
    {   //when touch the finger or move the finger on the screen,these are not the same action.
        InputManager.Instance.onTouchStart += ProcessPlayerSwerve;
        InputManager.Instance.onTouchMove += ProcessPlayerSwerve;
        stackSpoon = GameManager.Instance.stackSpoon;

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
    private void OnTriggerEnter(Collider other)
    {   //Collect
        //print(other.GetComponent<MeshRenderer>().material.name);
        if (GameManager.Instance.currentState == GameManager.GameState.Normal )
        {
            OnCollisionWithCollectable(other);
            OnCollisionWithObstacle(other);
            
        }
    }
    private void OnCollisionWithCollectable(Collider other)
    {
        if ( other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID==Character.CharacterID.Stack)
        {
            if (other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Stack && GameManager.Instance.globalCollectedStack == 0)
            {   //toplama animasyonu/sesi ekle
                stackList[currentStackListNumber].SetActive(true);
                Destroy(other.gameObject);

                GameManager.Instance.globalCollectedStack += 1;
                print(GameManager.Instance.globalCollectedStack);
            }
            else if(other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Stack && currentStackListNumber<4)
            {   //artýþa animasyonu/sesi ekle
                Destroy(other.gameObject);

                stackList[currentStackListNumber+1].SetActive(true);
                stackList[currentStackListNumber].SetActive(false);
                currentStackListNumber++;

                //stackSpoon.transform.localScale.

                GameManager.Instance.globalCollectedStack += 1;
                print(GameManager.Instance.globalCollectedStack);
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
                
            }
            else if (other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Stack && currentStackListNumber==4)
            {
                Destroy(other.gameObject);
                GameManager.Instance.globalCollectedStack += 1;

                print(GameManager.Instance.globalCollectedStack);
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
                //max durumda stack ekleyince açýlacak animasyonu ekle
            }
        }
    }
    private void OnCollisionWithObstacle(Collider other)
    {   //engele çarpýp saçýlma ses/animasyonu ekle
       
        if (other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle&& currentStackListNumber==0)
        {
            //gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);
            stackList[currentStackListNumber].SetActive(false); 

            GameManager.Instance.globalCollectedStack--;
            GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);

            isPlayerHasLastChance = true;
            print(GameManager.Instance.globalCollectedStack);

        }
        else if (other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle && isPlayerHasLastChance == true)
        {//gameover,fail animation
            gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);
            GameManager.Instance.currentState = GameManager.GameState.Failed;
            print("you failed");
            print(GameManager.Instance.globalCollectedStack);

        }
        else if(other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle && currentStackListNumber>0)
        {
            isPlayerHasLastChance = false;
            gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);

            stackList[currentStackListNumber - 1].SetActive(true);
            stackList[currentStackListNumber].SetActive(false);
            currentStackListNumber--;

            GameManager.Instance.globalCollectedStack -= 1;
            GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);

            print(GameManager.Instance.globalCollectedStack);
        }  

    }
    private void OnCollisionWithGate(Collider other)
    {

    }
  
}
