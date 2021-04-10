using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlight_obj : MonoBehaviour
{
    public Color startColor;
    private float distToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        startColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse");
        if (distToPlayer < 2.5f && !GameManager.GM.player.GetComponent<Player>().IsInteracting)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else {
            GetComponent<Renderer>().material.color = startColor;
        }
    }

    public void resetColor() {
        GetComponent<Renderer>().material.color = startColor;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }
}
