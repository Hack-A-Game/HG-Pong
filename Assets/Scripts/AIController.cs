using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    const float SPEED = 100.0f;
    Rigidbody2D _rigidBody;

    float _offset;
    float _maxTop;
    float _maxBottom;

    GameObject _ball;
    Rigidbody2D _ballRigid;
    Controller _controller;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _offset = GetComponent<Collider2D>().bounds.size.y / 2.0f;

        GameObject top = GameObject.Find("limit_top");
        _maxTop = top.transform.position.y - top.GetComponent<Collider2D>().bounds.size.y / 2.0f;

        GameObject bottom = GameObject.Find("limit_bottom");
        _maxBottom = bottom.transform.position.y + bottom.GetComponent<Collider2D>().bounds.size.y / 2.0f;
    }

    public void SetBall(GameObject ball)
    {
        _ball = ball;
        _ballRigid = ball.GetComponent<Rigidbody2D>();
    }

    public void SetController(Controller controller)
    {
        _controller = controller;
    }

    void Update()
    {
        if (_controller.GetState() != Controller.State.GAME)
        {
            return;
        }

        float yAcceleration = 0.0f;

        if (Random.Range(0.0f, 1.0f) > 0.8)
        {
            float randomOffset = Random.Range(-2.0f, 2.0f) * _ballRigid.velocity.y;
            if (_ball.transform.position.y + randomOffset - transform.position.y > 0.2f)
            {
                yAcceleration += 1.0f;
            }

            if (_ball.transform.position.y + randomOffset - transform.position.y < 0.2f)
            {
                yAcceleration -= 1.0f;
            }

            _rigidBody.velocity = Vector3.up * yAcceleration * SPEED * Time.deltaTime;
        }
    }

    public void LateUpdate()
    {
        if (transform.position.y + _offset > _maxTop)
        {
            transform.position = new Vector2(transform.position.x, _maxTop - _offset);
        }
        else if (transform.position.y - _offset < _maxBottom)
        {
            transform.position = new Vector2(transform.position.x, _maxBottom + _offset);
        }
    }
}
