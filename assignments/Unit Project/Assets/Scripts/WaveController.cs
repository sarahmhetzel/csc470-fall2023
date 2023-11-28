using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawn;

    public Wave[] waves;
    public int currentWave = 0;

    private bool startCountdown;

    // Start is called before the first frame update
    void Start()
    {
        startCountdown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].remainingEnemies = waves[i].enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if there are 3 waves
        if (currentWave >= waves.Length)
        {
            Debug.Log("You survived");
            return;
        }

        if (startCountdown == true)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            startCountdown = false;
            countdown = waves[currentWave].nextWave;
            Debug.Log("countdown stopping");

            //account for time when spawning
            StartCoroutine(SpawnWave());
        }
        if (waves[currentWave].remainingEnemies == 0)
        {
            startCountdown = true;
            currentWave++;
            Debug.Log("countdown starting");
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWave < waves.Length)
        {
            for (int i = 0; i < waves[currentWave].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWave].enemies[i], spawn.transform);
                enemy.transform.SetParent(spawn.transform);
                //Instantiate(waves[currentWave].enemies[i], spawn.transform);

                //wait before next spawn
                yield return new WaitForSeconds(waves[currentWave].nextEnemy);
            }
        }
    }

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public float nextEnemy;
        public float nextWave;

        [HideInInspector] public int remainingEnemies;
    }
}
