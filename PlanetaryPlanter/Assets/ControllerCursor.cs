using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCursor : MonoBehaviour
{
    public bool controller;
    public GameObject cursor;
    public float speed;

    public float xMove;
    public float yMove;

    void CheckInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");

    }

    private void Move()
    {
        Vector3 movement = new Vector3(xMove, yMove, 0) * speed * Time.deltaTime;
        transform.position += movement;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Move();
    }
}
