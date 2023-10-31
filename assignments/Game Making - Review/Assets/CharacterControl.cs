using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    GameObject characterCamera;
    PlaneController planeControl;

    float forwardSpeed = 15;
    float rotateSpeed = 100;
    float jumpForce = 20;
    float gravityModifier = 5f;
    bool doubleJump = false;
    public int keys = 0;
    public GameObject bonePrefab;

    public CharacterController cc;
    float yVelocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterCamera = GameObject.Find("Main Camera");
        planeControl = GameObject.Find("Plane").GetComponent<PlaneController>();
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
            doubleJump = true;
            yVelocity = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
            {
                yVelocity = jumpForce;
                doubleJump = false;
            }
        }
        

        Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
        amountToMove.y = yVelocity;

        cc.Move(amountToMove * Time.deltaTime);

        if (!planeControl.isFlying)
        {
            characterCamera.transform.position = transform.position + -transform.forward * 10 + Vector3.up * 3;
            characterCamera.transform.LookAt(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key"))
        {
            Destroy(other.gameObject);
            keys += 1; 
            if (keys == 6 && planeControl.rings == 3)
            {
                Instantiate(bonePrefab, new Vector3(146.29f, 30.37f, 197.09f), Quaternion.identity);
            }
        }
    }
}
