using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject player;
    public PlayableDirector endScene;
    public GameObject cinemachineCam;
    public GameObject playerCam;

    public void EnableCutscene()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 5 && GameObject.Find("PhoneRinging"))
        {
            cinemachineCam.SetActive(true);
            playerCam.GetComponent<Camera>().enabled = false;
            endScene.gameObject.SetActive(true);
            endScene.Play();
        }
    }
}
