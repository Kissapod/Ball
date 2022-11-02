using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float strength = 2f;

    private Vector3 _direction;
    private GameManager _gameManager;
    private float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StrengthUp();

        if (Input.GetKey(KeyCode.UpArrow))
            _direction = Vector3.up * strength;
        else
            _direction = Vector3.down * strength;

        transform.position += _direction * Time.deltaTime;
    }

    private void StrengthUp()
    {
        _timer += Time.deltaTime;
        if (_timer >= 15)
        {
            _timer = 0;
            strength += 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Line")
        {
            _gameManager.GameOver();
            Destroy(gameObject);
        }
    }
}
