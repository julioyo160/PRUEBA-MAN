using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed;
    public float distanceBetween;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject player;
    private Spawner spawner; // Referencia al spawner
    private float distance;
    private Vector2 lastPosition;
    private bool facingRight = true; // Indica la direcci�n en la que est� mirando el enemigo

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        // Desactivar la gravedad
        rb.gravityScale = 0;

        // Buscar el jugador en la escena si no se ha asignado
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Encontrar el spawner en la escena
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (Vector2)player.transform.position - rb.position;
            direction.Normalize();

            rb.velocity = direction * speed;

            // Si est� movi�ndose, activa el par�metro booleano "IsWalking"
            animator.SetBool("IsWalking", true);

            // Voltear el sprite seg�n la direcci�n en la que se est� moviendo
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        // Cambiar la direcci�n en la que est� mirando el enemigo
        facingRight = !facingRight;

        // Multiplicar la escala x por -1 para voltear el sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador entra en el trigger hijo, activa la animaci�n de da�o
            animator.SetTrigger("HitPlayer");
        }

        if (other.CompareTag("PlayerAttack"))
        {
            // Cuando el enemigo recibe un golpe del jugador, notificar al spawner y destruir el enemigo
            if (spawner != null)
            {
                spawner.EnemyDefeated();
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador sale del trigger hijo, desactiva la animaci�n de da�o
            animator.ResetTrigger("HitPlayer");
        }
    }

    // M�todo para asignar el jugador desde el spawner
    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
