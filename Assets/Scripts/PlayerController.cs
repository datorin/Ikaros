using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITouchListener
{
    [SerializeField] private Sprite _circle;
    [SerializeField] private Sprite _triangle;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;

    private const float Speed = 25;
    private const float HalfWidth = 0.25f;
    private const float HalfLength = 0.25f;

    private Vector3 _rightPosition;
    private Vector3 _leftPosition;

    private Vector3 _direction;
    private bool _isTouchEnded;
    private bool _isLaunched = false;

    private bool _isColliding = false;

    private void Start()
    {
        TouchManager.AddDirectionListener(this);

        _sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (!(_direction.magnitude > 0)) return;

        transform.up = _direction;

        _sprite.sprite = _triangle;

        if (!_isTouchEnded)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0;
            return;
        }

        if (_isLaunched)
        {
            GameplayManager.RestBullet();
            _isLaunched = false;
            _rb.velocity = Speed * _direction;
            _rb.gravityScale = 1f;
        }

        CheckCheckPoints();
    }

    private void CheckCheckPoints()
    {
        var checkPoints = GameplayManager.GetPassedCheckPoints();
        var firstCheckPoint = GameplayManager.FirstCheckPoint;

        if (checkPoints.Count == 0)
        {
            if (!(transform.position.y >= firstCheckPoint)) return;
            GameplayManager.AddPassedCheckPoints(firstCheckPoint);
            GameplayManager.SumBullet();
        }
        else
        {
            var nextCheckPoint = firstCheckPoint + GameplayManager.DistanceBetweeCheckPoints * checkPoints.Count;

            if (transform.position.y <= checkPoints.Last() - 1)
            {
                Destroy(gameObject);
            }

            if (!(transform.position.y >= nextCheckPoint)) return;
            GameplayManager.AddPassedCheckPoints(nextCheckPoint);
            GameplayManager.SumBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(_rb.velocity.y < 0) return;
        
        var newDirection = Vector3.zero;
        
        foreach (var contact in  other.contacts)
        {
            newDirection = Vector3.Reflect(_direction,  contact.normal).normalized;
        }

        _direction = newDirection;

        _rb.velocity = _direction * _rb.velocity.magnitude;
    }

    public void PassDirection(Vector3 direction)
    {
        _direction = -direction;
    }

    public void PassTouchEnded(bool isEnded)
    {
        _isTouchEnded = isEnded;

        if (_isTouchEnded == true)
        {
            _isLaunched = true;
        }
    }
}