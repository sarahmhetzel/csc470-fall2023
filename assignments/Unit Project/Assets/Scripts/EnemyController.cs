using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy SharedInstance;

    [SerializeField] float moveSpeed = 5f;
    Rigidbody rb;
    Transform target;
    Vector3 moveDirection;

    private EnemyScript waveSpawn; 
    private float countdown = 5f;

    private void Awake()
    {
        if (SharedInstance != null)
        {
            Debug.Log("multiple controllers for multiple enemies!");
        }
        SharedInstance = this;

        rb = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        waveSpawn = GetComponentInParent<EnemyScript>();
        target = GameObject.Find("FoxPrefab").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.LookAt(target);
            moveDirection = direction;
        }

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Destroy(gameObject);
            waveSpawn.waves[waveSpawn.currentWave].remainingEnemies = (waveSpawn.waves[waveSpawn.currentWave].remainingEnemies) - 1;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y) * moveSpeed;
    }

}
