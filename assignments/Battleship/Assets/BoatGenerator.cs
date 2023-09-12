using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShip : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject grenadePrefab;
    public GameObject explosionPrefab;
    public int numObjects = 5;
    public float radius = 20f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(shipPrefab, pos, rot);
        }
        for (int i = 0; i < 10; i++)
        {
            generateGrenade();
        }
    }
    void generateGrenade()
    {
        float x = Random.Range(-50, 50);
        float y = 20;
        float z = Random.Range(-50, 50);
        Vector3 pos = new Vector3(x, y, z);
        GameObject grenadeObj = Instantiate(grenadePrefab, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 10; i++)
            {
                generateExplosion();
            }
        }
    }
    void generateExplosion()
    {
        float x = Random.Range(-50, 50);
        float y = 1;
        float z = Random.Range(-50, 50);
        Vector3 pos = new Vector3(x, y, z);
        GameObject explosionObj = Instantiate(explosionPrefab, pos, Quaternion.identity);
    }
}