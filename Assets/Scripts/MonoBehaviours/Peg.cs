using UnityEngine;

public class Peg : MonoBehaviour
{
    public Sprite OrangePeg;
    public Sprite LightOrangePeg;
    public Sprite BluePeg;
    public Sprite LightBluePeg;
    public Sprite PurplePeg;
    public Sprite LightPurplePeg;

    private bool hit = false;

    public bool Hit { get => hit; }

    public enum PegType
    {
        Orange,
        Blue,
        Purple
    }

    private PegType type = PegType.Blue;

    public PegType Type { get => type; }

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        SetType(PegType.Blue);
    }

    public float GetPoint()
    {
        return type switch
        {
            PegType.Orange => 100f,
            PegType.Blue => 10f,
            PegType.Purple => 500f,
            _ => 0f
        };
    }

    public Sprite GetSprite(bool hit)
    {
        return type switch
        {
            PegType.Orange => Hit ? LightOrangePeg : OrangePeg,
            PegType.Blue => Hit ? LightBluePeg : BluePeg,
            PegType.Purple => Hit ? LightPurplePeg : PurplePeg,
            _ => null
        };
    }

    public void SetType(PegType type)
    {
        this.type = type;

        sr.sprite = GetSprite(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hit && collision.collider.CompareTag("Ball"))
        {
            hit = true;

            sr.sprite = GetSprite(true);
        }
    }
}
