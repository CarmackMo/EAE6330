using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base: MonoBehaviour
{
    [SerializeField] protected int m_HP = 0;

    [SerializeField] protected float m_cleanUpBoundary = 0.0f;

    [SerializeField] protected List<AudioClip> m_audioClipList = new List<AudioClip>();

    protected Vector2 m_speed = Vector2.zero;
    protected Vector2 m_downLeft = Vector2.zero;
    protected Vector2 m_topRight = Vector2.zero;

    public Vector2 Speed { get { return m_speed; } set { m_speed = value; } }


    protected virtual void Start()
    {
        Init();
    }


    protected virtual void Update()
    {
        Movement();

        CleanUp();
    }


    protected virtual void Init()
    {
        m_topRight = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -15));
        m_downLeft = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -15));
    }


    protected abstract void OnTriggerEnter2D(Collider2D i_collider);


    protected abstract void Movement();


    protected virtual void CleanUp()
    {
        Vector2 pos = transform.position;
        if (pos.x < m_downLeft.x - 2.0f || pos.x > m_topRight.x + 2.0f ||
            pos.y < m_downLeft.y - 2.0f || pos.y > m_topRight.y + 2.0f)
        {
            Destroy(gameObject);
        }
    }


}
