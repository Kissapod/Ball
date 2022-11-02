using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate = 1f;
    public float MinHeight = -1f;
    public float MaxHeight = 1f;


    public void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), SpawnRate, SpawnRate);
    }

    public void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject line = Instantiate(Prefab, transform.position, Quaternion.identity, transform);
        line.transform.position += Vector3.up * Random.Range(MinHeight,MaxHeight);
        line.GetComponent<Line>().speedLine = FindObjectOfType<GameManager>().SpeedLine;
    }
}
