using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction_Door : MonoBehaviour
{
    public GameObject door;
    public bool isOpen;
    public Vector3 left_openRotation;
    public Vector3 right_openRotation;

    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;

    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interactDoor() {
        if (isOpen)
        {
            isOpen = false;
            door.transform.rotation *= Quaternion.Euler(-left_openRotation);
            source.PlayOneShot(doorCloseClip);
        }
        else {
            door.transform.rotation *= Quaternion.Euler(left_openRotation);
            isOpen = true;
            source.PlayOneShot(doorOpenClip);
        }
    }
}
