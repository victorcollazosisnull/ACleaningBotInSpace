using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float fallSpeed = 3f;       
    public float rotationSpeed = 100f;
    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayDamageSound();

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); 

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogError("El objeto jugador no tiene PlayerHealth.");
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("muerte"))
        {
            Destroy(gameObject);
        }
    }
}