using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraPan : MonoBehaviour
{
    public float speed = 1f;
    public float range = 5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }
}