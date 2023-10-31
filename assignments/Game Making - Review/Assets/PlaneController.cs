using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour

{
    float forwardSpeed = 5f;

    float xRotationSpeed = 40f;
    float zRotationSpeed = 80f;
    float yRotationSpeed = 40f;

    public GameObject cameraObject;
    public GameObject dogPrefab;
    public bool isFlying = true;
    public bool isCreated = false;
    public int rings = 0;

    public Color colorOne;
    public Color colorTwo;
    public Color colorThree;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        float xRotation = vAxis * xRotationSpeed * Time.deltaTime;
        float zRotation = hAxis * zRotationSpeed * Time.deltaTime;
        float yRotation = hAxis * yRotationSpeed * Time.deltaTime;

        transform.Rotate(xRotation, yRotation, -zRotation, Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            forwardSpeed += 20;
        }

        forwardSpeed -= transform.forward.y * 5 * Time.deltaTime;
        forwardSpeed = Mathf.Max(0, forwardSpeed);

        float terrainY = Terrain.activeTerrain.SampleHeight(transform.position);
        if (transform.position.y < terrainY)
        {
            transform.position = new Vector3(transform.position.x, terrainY, transform.position.z);
            forwardSpeed -= 100 * Time.deltaTime;
        }


        gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * forwardSpeed;

        if (isFlying)
        {
            cameraObject.transform.position = transform.position + -transform.forward * 15 + Vector3.up * 5;
            cameraObject.transform.LookAt(transform);
        }

        if (!isCreated)
        {
            if (forwardSpeed == 0)
            {
                Instantiate(dogPrefab, new Vector3(70, 28.16f, 160), Quaternion.identity);
                isCreated = true;
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ring"))
        {
            Debug.Log("RING!");
            rings += 1;
            if (rings == 1)
            {
                rend.material.color = colorOne;
            }
            else if (rings == 2)
            {
                rend.material.color = colorTwo;
            }
            else if (rings == 3)
            {
                rend.material.color = colorThree;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("surface"))
        {
            forwardSpeed -= 100 * Time.deltaTime;
            isFlying = false;
            
        }
    }
}