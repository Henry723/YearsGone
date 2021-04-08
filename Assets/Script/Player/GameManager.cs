using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    private const float NO_FOG_VAL = 200;
    private const float FOG_VAL = 15;
    private const string FOG_END_S = "_FogEnd";
    private const string AMBIENT = "_Ambient";
    private const float DAY_VAL = 0f;
    private const float NIGHT_VAL = -1;
    private const string FILE_NAME = "/save.data";

    public GameObject player;
    public GameObject end;
    public GameObject floor;
    public Text scoreText;
    public int score = 0;
    public int enemyHealth;
    public float enemyRespawnTime = 5f;
    public bool isDay;
    public bool isMusic = true;

    private bool isFoggy;
    private GameObject door;
    private string prevScene;
    private GameObject wallGroup;

    void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {
       
    }

    
 }

