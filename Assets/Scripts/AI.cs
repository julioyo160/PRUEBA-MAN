using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed;
    public float distanceBetween;

    private Animator animator;
    public GameObject player;
    private float distance;
    private Vector2 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        // Establece la rotaci�n inicial del objeto hacia la derecha
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                // Si est� movi�ndose, activa el par�metro booleano "IsWalking"
                animator.SetBool("IsWalking", true);

                // Actualiza la �ltima posici�n mientras se mueve
                lastPosition = transform.position;

                // Gira el objeto seg�n la direcci�n en la que se est� moviendo
                if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0); // Orientaci�n hacia la derecha
                }
                else if (direction.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0); // Orientaci�n hacia la izquierda
                }
            }
            else
            {
                // Si no est� dentro de la distancia, desactiva el par�metro booleano "IsWalking"
                animator.SetBool("IsWalking", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Cuando el jugador entra en el trigger hijo, activa la animaci�n de da�o
            animator.SetBool("da�o", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Cuando el jugador sale del trigger hijo, desactiva la animaci�n de da�o
            animator.SetBool("da�o", false);
        }
    }

    // M�todo para asignar el jugador desde el spawner
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
