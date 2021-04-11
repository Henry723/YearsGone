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

    public GameObject player;
    public GameObject phone;
    public GameObject interactCanvas;

    public bool isDay;
    public bool isMusic = true;

    public List<GameObject> important_Objects;
    public List<AudioClip> obj_Audio;

    //counter for how many significant objects have been interacted with
    public int intr_Count = 0;

    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void checkObject(GameObject obj) {
        for (int i = 0; i < important_Objects.Count; i++) {
            if (obj.name.Equals(important_Objects[i].name)) {
                intr_Count++;
                important_Objects.RemoveAt(i);
                GetComponent<AudioSource>().PlayOneShot(obj_Audio[i]);

                if (intr_Count == 4) {
                    StartCoroutine(phoneCoroutine(obj_Audio[i]));
                }
                obj_Audio.RemoveAt(i);
                break;
            }
        }
    }

    IEnumerator phoneCoroutine(AudioClip clip) {
        yield return new WaitForSeconds(clip.length);
        phone.SetActive(true);
    }
    
 }

