using UnityEngine;


public class Enemy_Rock : Enemy_Base
{
    [SerializeField] private float m_angularSpeed = 0.0f;

    private Vector3 m_rotationAxis = Vector3.zero;

    private AudioManager s_audioManager = null;



    protected override void Init()
    {
        base.Init();    

        s_audioManager = AudioManager.Instance;

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

        Bullet_Player_Laser laser = i_collider.GetComponent<Bullet_Player_Laser>();
        if (laser != null)
        {
            s_audioManager.PlayOneShot(m_audioClipList[0]);
            EnemyGenerator.Instance.DeregisterRock(this);
            Destroy(gameObject);
        }
    }


    protected override void Movement()
    {
        transform.Translate(m_speed * Time.deltaTime, Space.World);
    }


    protected override void CleanUp()
    {
        Vector2 pos = transform.position;
        if (pos.x < m_downLeft.x - 0.25f || pos.x > m_topRight.x + 0.25f ||
            pos.y < m_downLeft.y - 0.25f || pos.y > m_topRight.y + 0.25f)
        {
            EnemyGenerator.Instance.DeregisterRock(this);
            Destroy(gameObject);
        }
    }


    private void Rotate()
    {
        transform.Rotate(m_rotationAxis, m_angularSpeed * Time.deltaTime, Space.Self);
    }

}
