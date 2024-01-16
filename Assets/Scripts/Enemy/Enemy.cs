using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    [SerializeField] protected int m_HP = 0;

    [SerializeField] protected float m_cleanUpBoundary = 0.0f;

    [SerializeField] protected List<AudioClip> m_audioClipList = new List<AudioClip>();

    protected Vector2 m_speed = Vector2.zero;

    public Vector2 Speed { get { return m_speed; } set { m_speed = value; } }


    protected virtual void Start()
    {

    }


    protected virtual void Update()
    {
        Movement();

        CleanUp();
    }


    protected abstract void OnTriggerEnter2D(Collider2D i_collider);


    protected abstract void Movement();


    private void CleanUp()
    {
        if (transform.position.y < m_cleanUpBoundary)
            Destroy(gameObject);
    }


}
