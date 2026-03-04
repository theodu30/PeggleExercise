using UnityEngine;

public class Bucket : MonoBehaviour
{
    private Vector3 startPosition;

    public float XVariation = 4f;
    public float Speed = .5f;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float time = Time.time;

        transform.position = startPosition + new Vector3(Mathf.Sin(time * Speed) * XVariation, 0, 0);
    }
}
