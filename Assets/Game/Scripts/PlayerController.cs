using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public GameObject stackSpoon;
    public GameObject spoon;
    public GameObject[] stackList;
    public ParticleSystem[] particleList;
    private int collectParticleIndex=0;
    private int obstacleParticleIndex = 1;
    private int loseParticleIndex=1;
    private int currentStackListNumber=0;
    private int gatePositiveParticleIndex = 2;
    private int gateNegativeParticleIndex = 3;
    private bool isPlayerPushed=false;
    private UIManager uiManager;
    [HideInInspector] public Transform playerTransform;

    

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
    private void FixedUpdate()
    {
        if (isPlayerPushed==true)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -5), ForceMode.Impulse);
            isPlayerPushed = false;
        }
    }

    private void ProcessPlayerMovement()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal && GameManager.Instance.currentState != GameManager.GameState.Victory)
        {
            GetComponent<Mover>().MoveTo(new Vector3(
             0f, 0f, GameManager.Instance.forwardSpeed));
        }
    }

    private void ProcessPlayerSwerve()
    {
        //clamp makes move to method to restrict player in a range.
        if (GameManager.Instance.currentState==GameManager.GameState.Normal && GameManager.Instance.currentState!=GameManager.GameState.Victory)
        {//the all movement happen here.
           GetComponent<Mover>().MoveTo(new 
               Vector3(-InputManager.Instance.GetDirection().x * GameManager.Instance.horizontalSpeed,0f,0f));
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {   //Collect
        //print(other.GetComponent<MeshRenderer>().material.name);
        if (GameManager.Instance.currentState == GameManager.GameState.Normal )
        {
            OnCollisionWithCollectable(other);
            OnCollisionWithObstacle(other);
            OnCollisionWithGate(other);
            
        }
    }
    private void OnCollisionWithCollectable(Collider other)
    {
        if ( other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID==Character.CharacterID.Stack)
        {
            //uiManager.ShowPraiseText(uiManager.GetRandomWord());
            if (other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Stack && GameManager.Instance.globalCollectedStack == 0)
            {   //toplama animasyonu/sesi ekle
                stackList[currentStackListNumber].SetActive(true);
                Destroy(other.gameObject);
                OnStackTakeAnimation();
                OnParticlePlay(collectParticleIndex);

                GameManager.Instance.globalCollectedStack += 1;
                print(GameManager.Instance.globalCollectedStack);
            }
            else if(other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Stack && currentStackListNumber<4)
            {   //artýþa animasyonu/sesi ekle
                Destroy(other.gameObject);

                stackList[currentStackListNumber+1].SetActive(true);
                stackList[currentStackListNumber].SetActive(false);
                currentStackListNumber++;
                OnStackTakeAnimation();
                OnParticlePlay(collectParticleIndex);
                //stackSpoon.transform.localScale.

                GameManager.Instance.globalCollectedStack += 1;
                print(GameManager.Instance.globalCollectedStack);
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
                
            }
            else if (other.GetComponent<Collectable>() != null && other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Stack && currentStackListNumber==4)
            {
                Destroy(other.gameObject);
                GameManager.Instance.globalCollectedStack += 1;
                OnStackTakeAnimation();
                OnParticlePlay(collectParticleIndex);

                print(GameManager.Instance.globalCollectedStack);
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
                //max durumda stack ekleyince açýlacak animasyonu ekle
            }
        }
    }
    private void OnCollisionWithObstacle(Collider other)
    {   //engele çarpýp saçýlma ses/animasyonu ekle
        //onCollisionWithObstacle();

        if (other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle && currentStackListNumber==0 && GameManager.Instance.globalCollectedStack == 1)
        {
            //gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);

            stackList[currentStackListNumber].SetActive(false); 

            GameManager.Instance.globalCollectedStack--;
            GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
            isPlayerPushed = true;

            OnParticlePlay(obstacleParticleIndex);
            print(GameManager.Instance.globalCollectedStack);
            onCollisionWithObstacle();
            Destroy(other.gameObject);

        }
        else if(other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle && currentStackListNumber != 0 && GameManager.Instance.globalCollectedStack>1)
        {
            
            gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);

            stackList[currentStackListNumber - 1].SetActive(true);
            stackList[currentStackListNumber].SetActive(false);
            currentStackListNumber--;

            GameManager.Instance.globalCollectedStack--;
            GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
            isPlayerPushed = true;
            OnParticlePlay(obstacleParticleIndex);
            print(GameManager.Instance.globalCollectedStack);
            onCollisionWithObstacle();
            Destroy(other.gameObject);
        }
        else if(other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle && currentStackListNumber == 0 && GameManager.Instance.globalCollectedStack>1)
        {
            //gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);
            GameManager.Instance.globalCollectedStack--;
            GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
            isPlayerPushed = true;
            OnParticlePlay(obstacleParticleIndex);
            print(GameManager.Instance.globalCollectedStack);
            onCollisionWithObstacle();
            Destroy(other.gameObject);

        }
        else if (other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Obstacle &&  GameManager.Instance.globalCollectedStack <= 0)
        {
            //gameObject.GetComponent<Rigidbody>().AddForce(-10, -10, -10, ForceMode.Impulse);
            print("you failed");
            GameManager.Instance.currentState = GameManager.GameState.Failed;
            LoseTheGame();
            LevelManager.Instance.NextLevel();
            print(GameManager.Instance.currentState);
            isPlayerPushed = true;
            OnParticlePlay(obstacleParticleIndex);
            onCollisionWithObstacle();
            Destroy(other.gameObject);

        }
        


    }
    private void OnCollisionWithGate(Collider other)
    {
        if (other.GetComponent<Character>().currentCharacterID==Character.CharacterID.Gate)
        {
          
            if (currentStackListNumber == 0 && other.gameObject.GetComponent<GateMechanicsEditor>().isNegative == true && GameManager.Instance.globalCollectedStack > other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber)
            {
                OnParticlePlay(gateNegativeParticleIndex);
                stackList[currentStackListNumber].SetActive(false);
                currentStackListNumber--;
                GameManager.Instance.globalCollectedStack -= other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber;

                print("works");
            }
            else if (currentStackListNumber >= 0 && other.gameObject.GetComponent<GateMechanicsEditor>().isNegative == true && GameManager.Instance.globalCollectedStack < other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber)
            {
                OnParticlePlay(gateNegativeParticleIndex);
                stackList[currentStackListNumber].SetActive(false);
                currentStackListNumber--;
                GameManager.Instance.globalCollectedStack -= other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber;
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
                GameManager.Instance.currentState = GameManager.GameState.Failed;

                print("works");

                LoseTheGame();
                LevelManager.Instance.RestartLevel();
                //LevelManager.Instance.RestartLevel();

                //GameManager.Instance.ShowMenuOnNewSceneLoaded = true;

            }
            else if (currentStackListNumber > 0 && other.gameObject.GetComponent<GateMechanicsEditor>().isNegative == true && GameManager.Instance.globalCollectedStack > other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber)
            {
                OnParticlePlay(gateNegativeParticleIndex);
                stackList[currentStackListNumber].SetActive(false);
                stackList[currentStackListNumber-1].SetActive(true);
                currentStackListNumber--;
                GameManager.Instance.globalCollectedStack -= other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber;
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
          
            }

            if (other.gameObject.GetComponent<GateMechanicsEditor>().isNegative == false && GameManager.Instance.globalCollectedStack==0)
            {
                OnParticlePlay(gatePositiveParticleIndex);
                stackList[currentStackListNumber].SetActive(true);
                GameManager.Instance.globalCollectedStack += other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber;
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
                
            }
            else if (other.gameObject.GetComponent<GateMechanicsEditor>().isNegative == false && GameManager.Instance.globalCollectedStack > 0 && currentStackListNumber< stackList.Length - 1)
            {
                OnParticlePlay(gatePositiveParticleIndex);
                stackList[currentStackListNumber ].SetActive(false);
                stackList[currentStackListNumber+1].SetActive(true);

                currentStackListNumber++;
                GameManager.Instance.globalCollectedStack += other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber;
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
            }
            else if (other.gameObject.GetComponent<GateMechanicsEditor>().isNegative == false  && currentStackListNumber==stackList.Length-1)
            {
                OnParticlePlay(gatePositiveParticleIndex);
                GameManager.Instance.globalCollectedStack += other.gameObject.GetComponent<GateMechanicsEditor>().gateNumber;
                GameManager.Instance.onStackTake(GameManager.Instance.globalCollectedStack);
            }

            Destroy(other.gameObject);
        }
    }
    public void LoseTheGame()
    {
        
        GameManager.onLoseEvent?.Invoke();

        
    }
    private void OnParticlePlay(int particleIndex)
    {
        particleList[particleIndex].Play();
    }
    private void OnParticlePlayGate(int particleIndex,Collider other)
    {//its better to have particles play at the exact point with the gates
        other.GetComponent<GateMechanicsEditor>().particleSystems[particleIndex].Play();
    }

    private void OnStackTakeAnimation()
    {
      
    }
    private void onCollisionWithObstacle()
    {
        spoon.transform.DOMove(spoon.transform.position + new Vector3(0, 0, -1), 0.5f, false);
    }


   
  
}
