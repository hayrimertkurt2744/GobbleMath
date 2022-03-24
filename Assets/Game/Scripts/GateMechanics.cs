using UnityEngine;

using UnityEditor;


public class GateMechanics : MonoBehaviour
{
    GateMechanicsEditor gateMechanicsEditor;

  
}

[CustomEditor(typeof(GateMechanics))]
class GateMechanicsEditor : Editor
{
    float thumbnailWidth = 70;
    float thumbnailHeight = 70;
    float labelWidth = 150f;

    public string gateNumber = "0";
    bool isPositive;
    bool isNegative;

  
    public override void OnInspectorGUI() 
    {
        base.DrawDefaultInspector();
        GateMechanics tank = (GateMechanics)target; //1

       
        GUILayout.Space(20f); //2
        GUILayout.Label("Custom Editor Elements", EditorStyles.boldLabel); //3

        GUILayout.Space(10f);
        GUILayout.Label("Gate Preferences");

        GUILayout.BeginHorizontal(); //4
        GUILayout.Label("Number", GUILayout.Width(labelWidth)); //5
        gateNumber = GUILayout.TextField(gateNumber); //6
        GUILayout.EndHorizontal(); //7

       /* GUILayout.BeginHorizontal(); //4
        GUILayout.Label("Player Name", GUILayout.Width(labelWidth)); //5
        playerName = GUILayout.TextField(playerName); //6
        GUILayout.EndHorizontal(); //7*/


        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save")) //8
        {
            PlayerPrefs.SetString("Gate Number", gateNumber); //9
           

            Debug.Log(gateNumber);
        }

        if (GUILayout.Button("Reset")) //10
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs Reset");
        }

        GUILayout.EndHorizontal();

    }
}
