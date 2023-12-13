using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AstronautScript : MonoBehaviour
{
    public CharacterController cc;

    float forwardSpeed = 15;
    float rotateSpeed = 150;
    float yVelocity = 0;
    float jumpForce = 10;

    GameObject playerCamera;
    public Animator animator;

    //public GameManager gameManager;
    private int partsCollected = 0;
    public TMP_Text partsCollectedText;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        partsCollectedText.text = partsCollected.ToString() + "/5 Parts Collected";

        if (partsCollected >= 5)
        {
            WinGame();
        }

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);

        if (!cc.isGrounded)
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            yVelocity = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }
        }


        Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
        amountToMove.y = yVelocity;

        cc.Move(amountToMove * Time.deltaTime);

        playerCamera.transform.position = transform.position + -transform.forward * 10 + Vector3.up * 3;
        playerCamera.transform.LookAt(transform);

        amountToMove.y = 0;
        animator.SetFloat("speed", amountToMove.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("parts"))
        {
            Destroy(other.gameObject);
            partsCollected += 1;
            Debug.Log(partsCollected);
        }
    }

    private void WinGame()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
