using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private float m_speed = 0.0f;
    [SerializeField] private double m_lastBonusTime = 0.0f;
    [SerializeField] private double m_bonusCooldown = 0.0f;
    [SerializeField] private double m_bonusDuration = 0.0f;
    [SerializeField] private bool m_isBonusState = false;

    [SerializeField] private AudioSource m_audioSource = null;

    [SerializeField] private Bullet m_prefab_bullet = null;

    private GameController s_gameController;


    protected override void Start()
    {
        s_gameController = GameController.Instance;
    }


    protected override void Update()
    {
        PlayerControl();

        if (m_isBonusState == false)
            ShootBullet();
        else
        {
            ShootThreeBullets();
            if (Time.time - m_lastBonusTime >= m_bonusDuration)
            {
                m_isBonusState = false;
            }
        }
    }


    protected void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Enemy_Bonus bonus = i_collider.GetComponent<Enemy_Bonus>();

        if (bonus != null)
        {
            double currentTime = Time.time;
            if (currentTime - m_lastBonusTime >= m_bonusCooldown)
            {
                m_isBonusState = true;
                m_lastBonusTime = currentTime;
            }
        }
    }


    private void PlayerControl()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }

        if (Input.GetKey(KeyCode.W)) 
        {
            direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        transform.Translate(direction * m_speed * Time.deltaTime);

        if (transform.position.x < -1 * s_gameController.Boundary_H)
            transform.position = new Vector3(s_gameController.Boundary_H - 0.5f, transform.position.y, 0);
        else if (transform.position.x > s_gameController.Boundary_H)
            transform.position = new Vector3(-1 * s_gameController.Boundary_H + 0.5f, transform.position.y, 0);

        if (transform.position.y < -1 * s_gameController.Boundary_V)
            transform.position = new Vector3(transform.position.x, s_gameController.Boundary_V - 0.5f, 0);
        else if (transform.position.y > s_gameController.Boundary_V)
            transform.position = new Vector3(transform.position.x, -1 * s_gameController.Boundary_V + 0.5f, 0);
    }


    private void ShootBullet()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            Bullet newBullet = Instantiate(m_prefab_bullet, transform.position, transform.rotation);
            newBullet.Speed = new Vector2(0, 5);

            m_audioSource.Play();
        }   
    }


    private void ShootThreeBullets()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Bullet newBullet_1 = Instantiate(m_prefab_bullet, transform.position, transform.rotation);
            newBullet_1.Speed = new Vector2(0, 5);

            Bullet newBullet_2 = Instantiate(m_prefab_bullet, transform.position, transform.rotation);
            newBullet_2.Speed = new Vector2(4, 3);

            Bullet newBullet_3 = Instantiate(m_prefab_bullet, transform.position, transform.rotation);
            newBullet_3.Speed = new Vector2(-4, 3);

            m_audioSource.Play();
        }
    }

}
