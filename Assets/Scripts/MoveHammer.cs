using UnityEngine;

public class MoveHammer : MonoBehaviour
{
    [SerializeField] private Vector2 _initialPosition = new Vector2(3,1);
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _initialAngle = 90f;
    [SerializeField] private float _maxAngle = 30f;

    private Touch _touch;
    private Vector2 _touchStartPosition;
    private Vector2 _touchEndPosition;
    private Rigidbody2D _rigidbody2d;
    private string _direction;


    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        StartPosition();
    }

    private void FixedUpdate()
    {
        TrackingTouch();
    }

    private void TrackingTouch()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _rigidbody2d.isKinematic = false;

            if (_touch.phase == TouchPhase.Began)
            {
                _touchStartPosition = _touch.position;
            }
            else if (_touch.phase == TouchPhase.Moved)
            {
                _touchEndPosition = _touch.position;

                float x = _touchEndPosition.x - _touchStartPosition.x;
                float y = _touchEndPosition.y - _touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    Debug.Log("Tapped");
                }
                else
                {

                    Vector2 velocity = new Vector2(x, y).normalized * _speed;

                    _rigidbody2d.velocity = velocity;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {

                        _direction = x > 0 ? "Right" : "Left";
                        Debug.Log(_direction);
                    }
                    else
                    {
                        float targetAngle = y > 0 ? Mathf.Lerp(_initialAngle, _maxAngle, Mathf.Abs(y) / Screen.height)
                                                    : _initialAngle;
                        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);

                        _direction = y > 0 ? "Up" : "Down";
                        Debug.Log(_direction);
                    }

                }

            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                _rigidbody2d.isKinematic = true;
                _rigidbody2d.velocity = Vector2.zero;

                StartPosition();


            }

        }
    }

    private void StartPosition()
    {
        transform.position = _initialPosition;
        transform.rotation = Quaternion.Euler(0f, 0f, _initialAngle);
    }
}
