using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public enum PlayMode { SINGLE, MULTI }
    public enum State { MENU, PAUSE, GAME }

    private PlayMode _mode;
    private State _state;

    private GameObject _ballNoAnim;
    private GameObject _ballAnim;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _mode = PlayMode.SINGLE;
        _state = State.MENU;
	}
	
	// Update is called once per frame
	void Update()
    {
	    if (_state == State.PAUSE)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _state = State.GAME;
                GameObject.Find("press_space").SetActive(false);
                _ballNoAnim.SetActive(false);
                _ballAnim.SetActive(true);
            }
        }
	}

    public void setMode(PlayMode mode)
    {
        _mode = mode;
    }

    public State GetState()
    {
        return _state;
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            GameObject obj = GameObject.Find("wall_left");
            obj.AddComponent<PlayerController>();
            obj.GetComponent<PlayerController>().SetKeyScheme(PlayerController.KeyScheme.ARROWS);

            obj = GameObject.Find("wall_right");
            _ballNoAnim = GameObject.Find("ball_no_anim");
            _ballAnim = GameObject.Find("ball_anim");

            if (_mode == PlayMode.MULTI)
            {
                obj.AddComponent<PlayerController>();
                obj.GetComponent<PlayerController>().SetKeyScheme(PlayerController.KeyScheme.WS);
            }
            else
            {
                obj.AddComponent<AIController>();
                obj.GetComponent<AIController>().SetBall(_ballAnim);
                obj.GetComponent<AIController>().SetController(this);
            }

            _ballAnim.SetActive(false);

            _state = State.PAUSE;
        }
    }
}
