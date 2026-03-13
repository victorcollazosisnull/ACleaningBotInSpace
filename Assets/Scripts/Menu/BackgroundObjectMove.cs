using UnityEngine;

public class BackgroundObjectMove : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    void Start()
    {
        speed = Random.Range(1f, 3f);
        rotationSpeed = Random.Range(-90f, 90f);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
    void OnMouseDown()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayDamageSound();
        }

        Destroy(gameObject);
    }
}