using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSlider : MonoBehaviour
{
     public Slider distanceSlider;
     public GameObject finishLine;
     public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //distanceSlider.value = finishLine.transform.position.z - player.transform.position.z;
    }
}
