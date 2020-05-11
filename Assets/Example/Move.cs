using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 2f;
    // Update is called once per frame
    void Update()
    {
        var t = Time.time * speed;
        var pos = transform.position;
        pos.x = Mathf.Sin(t) * 4f;
        pos.y = Mathf.Cos(t * 3f) * 2f;
        transform.position = pos;
    }
}
