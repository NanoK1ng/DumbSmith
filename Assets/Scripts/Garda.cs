using UnityEngine;

public class Garda : MonoBehaviour
{
    [SerializeField] private float _startHightGarda = -2f;
    [SerializeField] private float _minHightGarda = -4f;
    [SerializeField] private float _resistanceMultiplier = 0.01f;

    private Rigidbody2D _rigidbody2d;
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(0f, _startHightGarda);
    }

    private void FixedUpdate()
    {
        _rigidbody2d.velocity *= _resistanceMultiplier;
        BordersHightGarda();
    }

    private void BordersHightGarda()
    {
        if (transform.position.y <= -4f)
            transform.position = new Vector2(0f, _minHightGarda);
        if (transform.position.y >= -2f)
            transform.position = new Vector2(0f, _startHightGarda);
    }
}
