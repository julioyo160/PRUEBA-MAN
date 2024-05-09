using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
    public HP pHealth;
    public float damage;
    public float tiempoEntreDaño = 2f; // Intervalo de tiempo entre cada aplicación de daño

    private bool playerInside = false; // Indica si el jugador está dentro del área de activación

    // Invocar repetidamente el daño cada 2 segundos si el jugador está dentro del área de activación
    private void Start()
    {
        if (playerInside)
        {
            InvokeRepeating("ApplyDamage", 0f, tiempoEntreDaño);
        }
    }

    // Aplicar el daño al jugador si está dentro del área de activación
    private void ApplyDamage()
    {
        pHealth.health -= damage;
    }

    // Iniciar la repetición del daño cuando el jugador entra en el área de activación
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            InvokeRepeating("ApplyDamage", 0f, tiempoEntreDaño);
        }
    }

    // Detener la repetición del daño si el jugador sale del área de activación
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            CancelInvoke("ApplyDamage");
        }
    }
}
