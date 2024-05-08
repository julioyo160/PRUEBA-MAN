using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private Animator animator;
    private float distance;
    private Vector2 lastPosition;


    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // Si está moviéndose, activa el parámetro booleano "IsWalking"
            animator.SetBool("IsWalking", true);
     

            // Actualiza la última posición mientras se mueve
            lastPosition = transform.position;
        }
        else
        {
            // Si no está dentro de la distancia, desactiva el parámetro booleano "IsWalking"
            animator.SetBool("IsWalking", false);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Cuando el jugador entra en el trigger hijo, activa la animación de daño
            animator.SetBool("daño", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Cuando el jugador sale del trigger hijo, desactiva la animación de daño
            animator.SetBool("daño", false);
        }
    }
}
