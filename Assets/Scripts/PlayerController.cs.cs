using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public enum KeyScheme { WS, ARROWS }
    private KeyScheme _scheme;

    const float SPEED = 100.0f;
    Rigidbody2D _rigidBody;

    float _offset;
    float _maxTop;
    float _maxBottom;

    void Start ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _offset = GetComponent<Collider2D>().bounds.size.y / 2.0f;

        GameObject top = GameObject.Find("limit_top");
        _maxTop = top.transform.position.y - top.GetComponent<Collider2D>().bounds.size.y / 2.0f;

        GameObject bottom = GameObject.Find("limit_bottom");
        _maxBottom = bottom.transform.position.y + bottom.GetComponent<Collider2D>().bounds.size.y / 2.0f;
    }
	
	void Update ()
    {
        float yAcceleration = 0.0f;

	    switch (_scheme)
        {
            case KeyScheme.WS:
                if (Input.GetKey(KeyCode.W))
                {
                    yAcceleration += 1.0f;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    yAcceleration -= 1.0f;
                }
                break;

            case KeyScheme.ARROWS:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    yAcceleration += 1.0f;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    yAcceleration -= 1.0f;
                }
                break;
        }

        _rigidBody.velocity = Vector3.up * yAcceleration * SPEED * Time.deltaTime;
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

    public void SetKeyScheme(KeyScheme scheme)
    {
        _scheme = scheme;
    }
}
