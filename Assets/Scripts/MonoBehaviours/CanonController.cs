using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanonController : MonoBehaviour
{
    public int BallCount = 10;

    public GameObject PREFAB_Ball;

    public float CanonForce = 10f;

    private void Start()
    {
        GameEvents.CallBallNumberChangedEvent(BallCount);
    }

    private void OnEnable()
    {
        GameEvents.FireEvent += OnFireEventCalled;
        GameEvents.EndOfTurnEvent += OnEndOfTurnEventCalled;
    }

    private void OnDisable()
    {
        GameEvents.FireEvent -= OnFireEventCalled;
        GameEvents.EndOfTurnEvent -= OnEndOfTurnEventCalled;
    }

    private void Update()
    {
        if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.value;
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            Vector3 direction = new(screenPos.x - mousePos.x, screenPos.y - mousePos.y, 0);

            transform.up = direction;
        }
    }

    private void OnEndOfTurnEventCalled(object sender, bool arg)
    {
        if (arg)
        {
            BallCount++;
            GameEvents.CallBallNumberChangedEvent(BallCount);
        }

        if (BallCount == 0)
        {
            GameEvents.CallEndOfGameEvent(false);
        }
    }

    private void OnFireEventCalled(object sender, EventArgs args)
    {
        if (PREFAB_Ball != null)
        {
            BallCount--;
            GameEvents.CallBallNumberChangedEvent(BallCount);

            GameObject go = Instantiate(PREFAB_Ball, transform.position - transform.up, Quaternion.identity);
            go.transform.up = -transform.up;

            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

            rb.AddForce(go.transform.up * CanonForce, ForceMode2D.Impulse);
        }
    }
}
