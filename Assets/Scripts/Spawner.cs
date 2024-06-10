using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public float spawnInterval = 2f; // Intervalo de tiempo entre cada spawn
    public int maxEnemies = 5; // N�mero m�ximo de enemigos en pantalla
    public Transform player; // Referencia al jugador para que los enemigos sepan hacia d�nde ir

    private int currentEnemies = 0;
    private float timer = 0f;

    void Update()
    {
        // Si todav�a no hemos alcanzado el m�ximo de enemigos y ha pasado el intervalo de spawn
        if (currentEnemies < maxEnemies && Time.time - timer > spawnInterval)
        {
            SpawnEnemy(); // Llamamos a la funci�n para spawnear un enemigo
            timer = Time.time; // Reiniciamos el timer
        }
    }

    void SpawnEnemy()
    {
        // Creamos una instancia del enemigo en la posici�n del spawner
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        // Configuramos la referencia al jugador para que el enemigo sepa hacia d�nde ir
        AI enemyScript = newEnemy.GetComponent<AI>();
        if (enemyScript != null)
        {
            enemyScript.SetPlayer(player.gameObject);
        }
        currentEnemies++; // Incrementamos el contador de enemigos
    }

    // M�todo para reducir el contador de enemigos cuando uno es derrotado
    public void EnemyDefeated()
    {
        currentEnemies--;
    }
}
