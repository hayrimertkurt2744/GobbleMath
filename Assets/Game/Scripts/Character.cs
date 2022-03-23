using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterID
    {
        Player,
        Stack,
        Boss,
        Obstacle,
        None
    }
    public CharacterID currentCharacterID = CharacterID.None;
    public int spoonSize;
    public Material currentMaterial;
    private PlayerController playerController;

    

    private void Start()
    {
        currentMaterial = GetComponentInChildren<MeshRenderer>().material;
        GameManager.Instance.onStackTake += OnNewStackTake;
       
    }
    private void OnDisable()
    {
        GameManager.Instance.onStackTake -= OnNewStackTake;
    }

    private void OnNewStackTake(int amount)
    {
      
    }
}
