using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float damage;
    public float projectileSpeed;
    Rigidbody rb;

    private void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.up * projectileSpeed;
        rb.velocity = transform.forward * projectileSpeed;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (other.CompareTag("enemy"))
            {
                Destroy(other.gameObject);
            }

        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
