using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float forwardSpeed = 6;
    float rotateSpeed = 100;
    float jumpForce = 20;
    float gravityModifier = 5f;

    float yVelocity = 0;
    int score = 0;

    public CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);

        if (!cc.isGrounded)
        {
            // only applying gravity when not touching the ground
            yVelocity += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        else
        {
            // we are on the ground
            yVelocity = -1;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }
        }


        Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
        amountToMove.y = yVelocity;

        cc.Move(amountToMove * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        { 
            Destroy(other.gameObject);
        }
    }
}