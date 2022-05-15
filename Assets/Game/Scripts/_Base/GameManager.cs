﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public enum GameState
    {
        BeforeStart,
        Normal,
        Failed,
        Victory,
        LastSequence,
        TapTiming,
        Pause
    }
    //Max and min values on x axis to restrict the character. 
    public const float MAX_X =  4.25f;
    public const float MIN_X = -4.25f;

    [Header("Template Settings")]
    [SerializeField] private bool isDebug = true;

    [SerializeField] private bool startLevelMusicOnPlay = false;

    [SerializeField] private bool giveInputOnFirstClick = false;

    [SerializeField] private bool takeInputCount = false;

    [SerializeField] private bool showMenuOnNewSceneLoaded = false;

    [SerializeField] private bool useAppMetrica = false;

    [SerializeField] private bool useAppsFlyer = false;

    [SerializeField] private bool clearAllPoolObjectsOnNewLevelLoad = false;

    [SerializeField] private bool giveInputToUser = false;
    public bool GiveInputToUser { get { return giveInputToUser; } set { giveInputToUser = value; } }
    public bool IsDebug { get { return isDebug; } }
    public bool GiveInputOnFirstClick { get { return giveInputOnFirstClick; } }
    public bool TakeInputCount { get { return takeInputCount; } }
    public bool ShowMenuOnNewSceneLoaded { get { return showMenuOnNewSceneLoaded; } }
    public bool UseAppMetrica { get { return useAppMetrica; } }
    public bool UseAppsFlyer { get { return useAppsFlyer; } }
    public bool ClearAllPoolObjectsOnNewLevelLoad { get { return clearAllPoolObjectsOnNewLevelLoad; } }
    public bool StartLevelMusicOnPlay { get { return startLevelMusicOnPlay; } }

    public static Action onWinEvent;
    public static Action onLoseEvent;
    public static Action onTapTimingEvent;

    [Header("Game Settings")]
    public GameState currentState = GameState.BeforeStart;
    public Material[] allMaterials; 
    public float horizontalSpeed = 20f;
    public float forwardSpeed = 1f;
    public float playerSmooth=8f;
    public float growValue=0;
    public int globalCollectedStack = 0;

    public Action<int> onStackTake;

    private Transform actor;
    private Transform finishLine;
    private UIManager manager;


    [Header("Game Object")]
    public GameObject stackSpoon;

    [Header("TapTimingBar Features")]
    public float globalSpeedModifier=1.1f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (isDebug == true)
        {
            Debug.LogWarning("Debug Mode Active");
        }

        if (!useAppMetrica)
        {
            FindObjectOfType<AppMetrica>().gameObject.SetActive(false);
        }

        if (!useAppsFlyer)
        {
            FindObjectOfType<AppsFlyerObjectScript>().gameObject.SetActive(false);
        }
        //actor = GameObject.FindGameObjectWithTag("Player").transform;
        //finishLine = GameObject.FindGameObjectWithTag("FinishLine").transform;
        Application.targetFrameRate = 60;
        UIManager manager = FindObjectOfType<UIManager>();


    }
    private void Start()
    {
        if (IsDebug)
        {
            OnNewLevelLoaded();
        }
        LevelManager.onLevelRendered += OnNewLevelLoaded;
    }
    private void OnDisable()
    {
        LevelManager.onLevelRendered -= OnNewLevelLoaded;
    }
    private void OnNewLevelLoaded()
    {
         /*actor =GameObject.FindGameObjectWithTag("Player").transform;
         finishLine = GameObject.FindGameObjectWithTag("FinishLine").transform;
         manager.SetProgresBarMaxValue(finishLine.transform.position.z);*/
        
    }
    private void Update()
    {
        if (currentState==GameState.BeforeStart)
        {
            
            globalCollectedStack = 0;
            onStackTake(globalCollectedStack);

        }
       // manager.UpdateProgressBar(GameManager.Instance.actor.transform.position.z, 1f);
    }

}
