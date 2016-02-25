using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Ball : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    private Vector2 _direction = Vector2.left;
    private Vector2 _directionMultiplier = Vector2.left;
    public float SPEED = 150.0f;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidBody.velocity = _directionMultiplier * SPEED * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Pad" || other.gameObject.tag == "Left" || other.gameObject.tag == "Right")
        {
            float offsetFromCenter = other.transform.position.y - transform.position.y;
            offsetFromCenter = Mathf.Sign(offsetFromCenter) * Mathf.Pow(Mathf.Abs(offsetFromCenter), 1.5f);

            _rigidBody.velocity = -_rigidBody.velocity;
            _directionMultiplier = new Vector2(-_directionMultiplier.x, offsetFromCenter);
            _direction = -_direction;
            transform.rotation = Quaternion.Euler(0, _direction == Vector2.left ? 0 : 180, 0);

            Text score = null;
            if (other.gameObject.tag == "Left")
            {
                score = GameObject.Find("score_right").GetComponent<Text>();                    
            }
            else if (other.gameObject.tag == "Right")
            {
                score = GameObject.Find("score_left").GetComponent<Text>();
            }

            if (score != null)
            {
                score.text = (Int32.Parse(score.text) + 1).ToString();
                transform.position = new Vector2(0, 0);
                _directionMultiplier.y = 0;
            }
        }
        else if (other.gameObject.tag == "Wall")
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -_rigidBody.velocity.y);
            _directionMultiplier.y = -_directionMultiplier.y;
        }
    }
}
