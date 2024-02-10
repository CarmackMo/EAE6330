using UnityEngine;


public abstract class Bullet : MonoBehaviour
{
    protected Vector2 m_vel = Vector2.zero;

    protected Vector2 m_downLeft = Vector2.zero;
    protected Vector2 m_topRight = Vector2.zero;


    public Vector2 Velocity { get { return m_vel; } set { m_vel = value; } }


    protected virtual void Start() 
    {
        m_downLeft = GameController.Instance.DownLeft;
        m_topRight = GameController.Instance.TopRight;
    }


    protected virtual void Update()
    {
        Movement();
        CleanUp();
    }


    protected abstract void OnTriggerEnter2D(Collider2D i_collider);


    protected virtual void CleanUp()
    {
        Vector2 pos = transform.position;
        if (pos.x < m_downLeft.x - 2.0f || pos.x > m_topRight.x + 2.0f ||
            pos.y < m_downLeft.y - 2.0f || pos.y > m_topRight.y + 2.0f)
        {
            Destroy(gameObject);
        }
    }


    protected virtual void Movement()
    {
        transform.Translate(m_vel * Time.deltaTime);
    }

}
