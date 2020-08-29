using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float speed = 30f;
    [SerializeField] private string axis;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float ver = Input.GetAxisRaw(axis);
        rb2D.velocity = new Vector2(0, ver * speed);

    }
}
