using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDuration = 0.2f; // Duraci�n del ataque
    public Collider2D attackCollider; // Collider para el �rea de ataque

    void Start()
    {
        // Aseg�rate de que el Collider est� desactivado al inicio
        attackCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // Activar el Collider de ataque
        attackCollider.enabled = true;

        // Esperar durante la duraci�n del ataque
        yield return new WaitForSeconds(attackDuration);

        // Desactivar el Collider de ataque
        attackCollider.enabled = false;
    }
}
