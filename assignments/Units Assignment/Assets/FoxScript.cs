using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxScript : MonoBehaviour
{

    GameObject characterCamera;

    float forwardSpeed = 15;
    float rotateSpeed = 150;
    float jumpForce = 20;
    float gravityModifier = 5f;

    public CharacterController cc;
    float yVelocity = 0;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);

        if (!cc.isGrounded)
        {
            yVelocity += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        if (cc.isGrounded)
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

        characterCamera.transform.position = transform.position + -transform.forward * 15 + Vector3.up * 10;
        characterCamera.transform.LookAt(transform);

        amountToMove.y = 0;

        animator.SetFloat("speed", amountToMove.magnitude);
        Debug.Log(amountToMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("resource"))
        {
            ResourceController.currentLevel += 25;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("wheelbarrow"))
        {
            ResourceController.currentLevel -= 25;
        }
    }

}