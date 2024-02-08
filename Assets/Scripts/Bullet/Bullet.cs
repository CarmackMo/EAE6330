using UnityEngine;


public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float m_cleanUpThreshold = 0.0f;

    protected Vector2 m_speed = Vector2.zero;


    public Vector2 Speed { get { return m_speed; } set { m_speed = value; } }


    protected virtual void Start() { }


    protected virtual void Update()
    {
        Movement();
        CleanUp();
    }


    protected abstract void OnTriggerEnter2D(Collider2D i_collider);


    protected abstract void CleanUp();


    protected void Movement()
    {
        transform.Translate(m_speed * Time.deltaTime);
    }

    
}
