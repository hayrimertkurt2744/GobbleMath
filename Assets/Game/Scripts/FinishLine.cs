using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameObject spoon;
    public GameObject objectToThrow;
    public Button nextLevelButton;

    public Vector3 endPoint;
    public Vector3 afterFinishTransfom;
    public bool followingCam=true;
    [Header("Jump Parameters")]
    public float jumpDuration;
    public float jumpPower;
    private bool snapping = false;
    public int numOfJumps;
    private int count = 63;
    private int clickCount = 0;
   
    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineVirtualCamera vcam2; //last sequence cam
    //[HideInInspector] public bool isLastSequenceStarted = false;
    public ParticleSystem[] finishParticleList;
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

                    spoon.transform.DORotate(new Vector3(-75, 180, 0), 0.8f, RotateMode.FastBeyond360).OnComplete(() =>
                    {
                        objectToThrow.gameObject.SetActive(true);

                        objectToThrow.gameObject.transform.DOJump(endPoint, jumpPower, numOfJumps, jumpDuration, snapping).OnComplete(() =>
                        {
                            objectToThrow.transform.parent = null;
                            spoon.transform.DORotate(new Vector3(75, 0, 0),1f,RotateMode.LocalAxisAdd);
                            clickCount++;
                            GameManager.onWinEvent?.Invoke();
                            nextLevelButton.gameObject.SetActive(true);
                            nextLevelButton.onClick.AddListener(GetNextLevel);

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
        LevelManager.Instance.NextLevel();
    }
}
