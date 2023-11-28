using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxScript : MonoBehaviour
{

    GameObject characterCamera;
    float forwardSpeed = 15;
    float rotateSpeed = 100;
    public Transform firePosition;
    public GameObject projectilePrefab;

    float yVelocity = 0;

    CharacterController cc;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        characterCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            Debug.Log("shooting");
        }

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);

        Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
        amountToMove.y = yVelocity;

        cc.Move(amountToMove * Time.deltaTime);

        characterCamera.transform.position = transform.position + -transform.forward * 15 + Vector3.up * 5;
        characterCamera.transform.LookAt(transform);

        amountToMove.y = 0;

        animator.SetFloat("speed", amountToMove.magnitude);
    }

    private void Shoot()
    {
        GameObject obj = ObjectPooling.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = firePosition.position;
        obj.transform.rotation = firePosition.rotation;
        obj.SetActive(true);
            //Vector3 inFront = transform.position + transform.forward * 5;
            //GameObject projectile = Instantiate(projectilePrefab, inFront, transform.rotation);

            //Rigidbody rb = projectile.GetComponent<Rigidbody>();
            //rb.AddForce(projectile.transform.forward * 2000);
    }

}
