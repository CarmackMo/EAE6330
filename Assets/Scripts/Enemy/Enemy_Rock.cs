using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rock : Enemy
{
    [SerializeField] private float m_angularSpeed = 0.0f;

    private Vector3 m_rotationAxis = Vector3.zero;


    protected override void Start()
    {
        base.Start();

        float sign = Random.Range(-1.0f, 1.0f);
        m_rotationAxis = (sign >= 0) ? Vector3.forward : Vector3.back;
    }


    protected override void Update()
    {
        base.Update();

        Rotate();
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;
    }


    protected override void Movement()
    {
        transform.Translate(m_speed * Time.deltaTime, Space.World);
    }

    
    private void Rotate()
    {
        transform.Rotate(m_rotationAxis, m_angularSpeed * Time.deltaTime, Space.Self);
    }

}
