using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameObject spoon;
    public GameObject[] objectToThrow;
    public Button nextLevelButton;
    private PlayerController playerController;
    public GameObject tapTimingBar;
    
    

    public Vector3[] endPoint;
    public Vector3 afterFinishTransfom;
    public bool followingCam=true;
    [Header("Jump Parameters")]
    public float jumpDuration;
    public float jumpPower;
    private bool snapping = false;
    private bool onTapTimingFinished = false;
    private UIManager uiManager;
    public int numOfJumps;
    private int count = 63;
    private int clickCount = 0;
   
    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineVirtualCamera vcam2; //last sequence cam
    //[HideInInspector] public bool isLastSequenceStarted = false;
    public ParticleSystem[] finishParticleList;
    private void OnEnable()
    {
        InputManager.Instance.onTouchStart += OnLevelEndingSequence;
        InputManager.Instance.onTouchMove += OnLevelEndingSequence;
    }
    private void OnDisable()
    {
        InputManager.Instance.onTouchStart -= OnLevelEndingSequence;
        InputManager.Instance.onTouchMove -= OnLevelEndingSequence;
    }
    private void OnTriggerExit(Collider other)
    {
        finishConfetti();
        SwitchCamPriority();
        if (other.GetComponent<Character>().currentCharacterID==Character.CharacterID.Player)
        {
            GameManager.Instance.currentState = GameManager.GameState.LastSequence;
            
            spoon.transform.DOMove(afterFinishTransfom, 2f, false).OnComplete(() => {
                
                spoon.transform.DORotate(new Vector3(0, 180, 0), 1f, RotateMode.FastBeyond360).OnComplete(() =>
                {

                    spoon.transform.DORotate(new Vector3(-75, 180, 0), 0.3f, RotateMode.FastBeyond360).OnComplete(() =>
                    {
                        foreach (GameObject gameObject in objectToThrow)
                        {
                            gameObject.SetActive(true);
                        }

                        objectToThrow[0].gameObject.transform.DOJump(endPoint[0], jumpPower, numOfJumps, jumpDuration, snapping);
                        objectToThrow[1].gameObject.transform.DOJump(endPoint[1], jumpPower, numOfJumps, jumpDuration, snapping);
                        objectToThrow[2].gameObject.transform.DOJump(endPoint[2], jumpPower, numOfJumps, jumpDuration, snapping);
                        objectToThrow[3].gameObject.transform.DOJump(endPoint[3], jumpPower, numOfJumps, jumpDuration, snapping);
                        objectToThrow[4].gameObject.transform.DOJump(endPoint[4], jumpPower, numOfJumps, jumpDuration, snapping);


                        objectToThrow[5].gameObject.transform.DOJump(endPoint[5], jumpPower, numOfJumps, jumpDuration, snapping).OnComplete(() =>
                        {
                        foreach (GameObject gameObject in objectToThrow)
                        {
                            gameObject.transform.parent = null;
                        }

                        spoon.transform.DORotate(new Vector3(75, 0, 0), 1f, RotateMode.LocalAxisAdd);

                        clickCount++;
                            //GameManager.Instance.currentState = GameManager.GameState.TapTiming;


                            GameManager.onTapTimingEvent?.Invoke();


                            //OnLevelEndingSequence();

                            if (onTapTimingFinished == true)
                            {
                                GameManager.Instance.currentState = GameManager.GameState.Victory;
                                GameManager.onWinEvent?.Invoke();
                                nextLevelButton.gameObject.SetActive(true);
                                nextLevelButton.onClick.AddListener(GetNextLevel);
                                onTapTimingFinished = false;
                                GameManager.Instance.globalSpeedModifier = 1.1f;
                            }
                                


                           
                        });
                    });
                });
            });
            
            
        }
        
  
    }
    private void SwitchCamPriority()
    {
        if (followingCam)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
            followingCam = !followingCam;
        }
    }

    private void finishConfetti()
    {
        finishParticleList[0].Play();
        finishParticleList[1].Play();

    }
    public void GetNextLevel()
    {
        foreach (GameObject gameObject in objectToThrow)
        {
            gameObject.SetActive(false);
        }
        LevelManager.Instance.NextLevel();

    }
    private void OnLevelEndingSequence()
    {
       
       
            GameManager.Instance.globalSpeedModifier = 0;

            ExecuteAfterTime(2);

        onTapTimingFinished = true;

    }
    IEnumerator ExecuteAfterTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        
        
    }
}
