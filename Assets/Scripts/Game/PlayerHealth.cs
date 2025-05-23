using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public GameObject explosionPrefab;

    private void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdateLifeUI(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UIManager.Instance.UpdateLifeUI(currentHealth);

        if (currentHealth == 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        AudioManager.Instance.PlayExplosionSound();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        string message = $"Puntos: {GameManager.Instance.points}, Tiempo: {GameManager.Instance.timeSurvived:F1}s";
        FindObjectOfType<UnityToReact>().SendMessageToReact(message);

        yield return new WaitForSeconds(1f);

        float points = GameManager.Instance.points;
        float time = GameManager.Instance.timeSurvived;

        RankingManager.SaveResult(new GameResult(points, time));
        SceneManager.LoadScene("GameOver");

        Destroy(gameObject);
    }
}