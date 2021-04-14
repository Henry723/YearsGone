using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioSource wallHit;
    public GameObject player;
    public FirstPersonCamera fpc;
    private CharacterController cc;
    public Rigidbody ball;
    public Transform shootOrigin;
    public float moveSpeed = 10f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;
    public float ballSpeed = 10f;
    public float inspectFactor = 50.0f;
    private bool groundedPlayer;
    private bool noClip;
    public bool isInteracting = false;
    public bool IsInteracting { get{ return isInteracting; } }
    //inspect stuff
    public FirstPersonCamera fpsCamera;
    public GameObject inspectContainer;
    public GameObject inspectedObject;
    public GameObject instantiatedObject;

    public GameObject hoveredObject;
    public Color objColor;

    private const int INTERACTABLE_LAYER = 8;

    Vector3 playerVelocity;
    private PlayerFootsteps playerFootsteps;

    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        playerFootsteps = GetComponent<PlayerFootsteps>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            if (!noClip)
            {
                PlayerMovements();
            }
            else
            {
                PlayerNoClipMovement();
            }
        }
        else {
            InspectMovement();
        }
        iteractFunction();
    }

    private void PlayerNoClipMovement()
    {
        playerFootsteps.EndWalk();
        NoClipMovement();
    }

    private void PlayerMovements()
    {
        GroundCheck();
        Movement();
        Jump();
        Gravity();
    }

    private void Movement()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        if (move.x != 0 && move.z != 0)
            playerFootsteps.StartWalk();
        else
            playerFootsteps.EndWalk();
        cc.Move(move * moveSpeed * Time.deltaTime);
    }

    private void NoClipMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        cc.Move(Camera.main.transform.TransformDirection(move * moveSpeed * Time.deltaTime));
    }

    private void InspectMovement() {
        inspectContainer.transform.Rotate( 0, -(Input.GetAxis("Look X") * inspectFactor * Time.deltaTime), 0, Space.Self);
    } 

    private void Jump()
    {
        if (Input.GetButton("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void GroundCheck()
    {
        groundedPlayer = cc.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }

    private void Gravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }

    private void iteractFunction()
    {
        if (Input.GetButtonDown("Interact") && !isInteracting)
        {
            int layerMask = 1 << 8;
            Debug.Log("Open Door");
            Debug.DrawRay(Camera.main.transform.position,Camera.main.transform.forward);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, 2f, layerMask)) {
                inspectedObject = hit.collider.gameObject;

                if (inspectedObject.tag == "Door")
                {
                    inspectedObject.GetComponent<interaction_Door>().interactDoor();
                }
                else if (inspectedObject.tag == "Inspectable")
                {
                    GameManager.GM.checkObject(inspectedObject);
                    //inspectedObject.GetComponent<highlight_obj>().resetColor();

                    instantiatedObject = GameObject.Instantiate(inspectedObject);

                    instantiatedObject.transform.SetParent(inspectContainer.transform);
                    instantiatedObject.transform.localPosition = Vector3.zero;
                    instantiatedObject.transform.localRotation = Quaternion.identity;

                    inspectedObject.SetActive(false);

                    isInteracting = true;
                    fpsCamera.disabled = true;
                    fpsCamera.unlockCursor();
                    inspectContainer.SetActive(true);
                    GameManager.GM.enableCanvas();
                    this.GetComponent<AudioSource>().Stop();
                }
            }
        }

        if (Input.GetButtonDown("Drop") && isInteracting) 
        {
            inspectContainer.transform.localRotation = Quaternion.identity;
            inspectContainer.SetActive(false);
            fpsCamera.disabled = false;
            isInteracting = false;

            Destroy(instantiatedObject);
            inspectedObject.SetActive(true);
            inspectedObject = null;
        }
    }

    public void disableInspect() {
        inspectContainer.transform.localRotation = Quaternion.identity;
        inspectContainer.SetActive(false);
        fpsCamera.disabled = false;
        isInteracting = false;

        Destroy(instantiatedObject);
        inspectedObject.SetActive(true);
        inspectedObject = null;
    }
}
