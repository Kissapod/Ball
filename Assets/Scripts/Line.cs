using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float speedLine = 5f;

    private float leftEdge;
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    void Update()
    {
        transform.position += Vector3.left * speedLine * Time.deltaTime;

        if (transform.position.x < leftEdge)
            Destroy(gameObject);
    }
}
