using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction_Door : MonoBehaviour
{
    public GameObject door;
    public bool isOpen;
    public bool isLeft = false;
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
            if (isLeft)
            {
                isOpen = false;
                door.transform.rotation *= Quaternion.Euler(-left_openRotation);
                source.PlayOneShot(doorCloseClip);
            }
            else
            {
                isOpen = false;
                door.transform.rotation *= Quaternion.Euler(-right_openRotation);
                source.PlayOneShot(doorCloseClip);
            }
        }
        else {
            if (isLeft)
            {
                door.transform.rotation *= Quaternion.Euler(left_openRotation);
                isOpen = true;
                source.PlayOneShot(doorOpenClip);
            }
            else {
                door.transform.rotation *= Quaternion.Euler(right_openRotation);
                isOpen = true;
                source.PlayOneShot(doorOpenClip);
            }
        }
    }
}
