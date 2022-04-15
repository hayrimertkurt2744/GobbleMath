using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateMechanicsEditor : MonoBehaviour
{
    [Header("Gate Settings")]
    [SerializeField] private bool isNegative;
    [SerializeField] private int gateNumber;
    public TextMeshPro gateText;
    public ParticleSystem[] particleSystems;
    private Character character;
    private int negativeIndex=1;
    private int positiveIndex=0;
   
    // Start is called before the first frame update
    void Start()
    {
        GatePreparing();
    }

    private void GatePreparing()
    {
        // gateText = GetComponent<TextMeshPro>();
        if (isNegative == true && gameObject.GetComponent<Character>().currentCharacterID == Character.CharacterID.Gate)
        {
            particleSystems[negativeIndex].Play();
            particleSystems[positiveIndex].Stop();
            gateNumber *= -1;
            gateText.text = gateNumber.ToString();

        }
        else if (isNegative == false && gameObject.GetComponent<Character>().currentCharacterID == Character.CharacterID.Gate)
        {
            particleSystems[positiveIndex].Play();
            particleSystems[negativeIndex].Stop();
            gateText.text = gateNumber.ToString();
        }
    }

  
}
