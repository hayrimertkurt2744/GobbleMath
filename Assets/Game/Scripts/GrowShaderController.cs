using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShaderController : MonoBehaviour
{
    public float timeToGrow = 5;
    public float refreshRate = 0.05f;
    [Range(0, 1)]
    public float minGrow = 0.2f;
    [Range(0, 1)]
    public float maxGrow = 0.97f;
    public Material growMaterial;
    

    private bool fullyGrown;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.growValue = growMaterial.GetFloat("Grow_");
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(GrowVines(growMaterial));
        growMaterial.SetFloat("Grow_", GameManager.Instance.growValue);
        
    }

    IEnumerator GrowVines (Material mat)
    {
        GameManager.Instance.growValue = growMaterial.GetFloat("Grow_");

        if (!fullyGrown)
        {
            while (GameManager.Instance.growValue < maxGrow)
            {
                //GameManager.Instance.growValue += 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", GameManager.Instance.growValue);

                yield return new WaitForSeconds(refreshRate);
            }
        }
        if (GameManager.Instance.growValue>= maxGrow)
        {
            fullyGrown = true;

        }
        else
        {
            fullyGrown = false;
        }
    }

}
