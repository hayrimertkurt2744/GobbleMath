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
        Gate,
        None
    }
    public CharacterID currentCharacterID = CharacterID.None;
    public int spoonSize;
    public Material currentMaterial;
    private PlayerController playerController;

    

    private void Start()
    {
        //currentMaterial = GetComponentInChildren<MeshRenderer>().material;
        GameManager.Instance.onStackTake += OnNewStackTake;
       
    }
    private void OnDisable()
    {
        GameManager.Instance.onStackTake -= OnNewStackTake;
    }
    private void Update()
    {
        /*if (gameObject.GetComponent<Character>().currentCharacterID==CharacterID.Stack)
        {
            float baseSize = 1;
            float animation = baseSize + Mathf.Sin(Time.time * 8f) * baseSize / 7f;
            transform.localScale = Vector3.one * animation;
        }*/
    }
    private void OnNewStackTake(int amount)
    {
      
    }
}
