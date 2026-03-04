using UnityEngine;

public class Ball : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y <= -6)
        {
            GameEvents.CallEndOfTurnEvent(false);
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bucket"))
        {
            GameEvents.CallEndOfTurnEvent(true);
            Destroy(gameObject);
            return;
        }
    }
}
