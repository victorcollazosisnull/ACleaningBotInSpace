using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public GameObject explosionPrefab;

    [Header("Hit Player")]
    private bool isInvulnerable = false;
    public float invulnerableTime = 1.5f;

    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdateLifeUI(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        AudioManager.Instance.PlayDamageSound();

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UIManager.Instance.UpdateLifeUI(currentHealth);

        if (currentHealth == 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }
    IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        float timer = 0f;

        while (timer < invulnerableTime)
        {
            sr.color = new Color(0.56f, 0.56f, 0.56f, 1f);
            yield return new WaitForSeconds(0.1f);

            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);

            timer += 0.2f;
        }

        sr.color = Color.white;
        isInvulnerable = false;
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

        int points = GameManager.Instance.points;
        int time = Mathf.FloorToInt(GameManager.Instance.timeSurvived);
        string playerName = GameSession.Instance != null ? GameSession.Instance.PlayerName : "Invitado";

        RankingManager.SaveResult(new GameResult(points, time));

        Destroy(gameObject);

        SceneManager.LoadScene("GameOver");
    }
}