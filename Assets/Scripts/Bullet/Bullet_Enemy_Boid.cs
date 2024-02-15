using System.Collections.Generic;
using UnityEngine;


public class Bullet_Enemy_Boid : Bullet
{

    private Vector2 m_pos = Vector2.zero;
    private Vector2 m_acc = Vector2.zero;

    private bool m_isLead = false;

    private float m_raidus_flock = 5.5f;
    private float m_radius_cohere = 4.0f;
    private float m_radius_separate = 1.0f;
    private float m_radius_evad = 2.5f;
    private float m_mass = 1.0f;
    private float m_speed = 17.0f;
    private float m_speed_angular = 1.5f;
    private float m_maxForce = 20.0f;
    
    private float m_lifeTime = 12.0f;
    private float m_bornTime = 0.0f;

    private Player s_player = null;
    private List<Bullet_Enemy_Boid> m_boids = new List<Bullet_Enemy_Boid>();



    protected override void Update()
    {
        if (Time.time - m_bornTime < m_lifeTime)
            Reposition();
        else
            CleanUp();

        Flock(m_boids);
        RotateTowards(m_vel);

        m_pos = transform.position;

        m_pos += m_vel * Time.deltaTime;
        m_vel += m_acc * Time.deltaTime;

        m_acc = Vector2.zero;

        transform.position = m_pos;
    }


    protected override void OnTriggerEnter2D(Collider2D i_collider)
    {
        if (i_collider == null)
            return;

        Bullet_Player_Default bullet = i_collider.GetComponent<Bullet_Player_Default>();
        Bullet_Player_Laser laser = i_collider.GetComponent<Bullet_Player_Laser>();

        if (bullet != null || laser != null)
        {
            Destroy(gameObject);
        }
        else
            return;
    }


    private void Reposition()
    {
        Vector2 currPos = transform.position;

        if (currPos.x < m_downLeft.x)
            transform.position = new Vector3(m_topRight.x, currPos.y, 0);
        else if (currPos.x > m_topRight.x)
            transform.position = new Vector3(m_downLeft.x, currPos.y, 0);
        else if (currPos.y < m_downLeft.y)
            transform.position = new Vector3(currPos.x, m_topRight.y, 0);
        else if (currPos.y > m_topRight.y)
            transform.position = new Vector3(currPos.x, m_downLeft.y, 0);
    }


    private void ApplyForce(Vector2 i_force)
    {
        m_acc += i_force / m_mass;
    }


    private void RotateTowards(Vector3 targetDirection)
    {
        // 获取当前对象的朝向向量，这里使用transform.up作为二维朝向的例子
        Vector3 currentDirection = transform.up;

        // 确保两个朝向的Z分量都为0
        targetDirection.z = 0.0f;
        currentDirection.z = 0.0f;

        // 计算目标方向与当前方向之间的角度
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

        // 计算需要旋转的最短角度差
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        // 确定每帧旋转的角度，但不超过绝对值为1度
        float rotateAmount = Mathf.Clamp(angleDifference, -m_speed_angular, m_speed_angular);

        // 应用旋转
        transform.Rotate(0, 0, rotateAmount);
    }


    private Vector2 Align(List<Bullet_Enemy_Boid> i_boidList)
    {
        // get all other flockmates within the radius

        // accumulate their total veloctiy

        // calculate the average by dividing the number of flockmates

        int flockMates = 0;
        Vector2 steering = Vector2.zero;
        foreach (Bullet_Enemy_Boid boid in i_boidList)
        {
            if (boid != this && Vector2.Distance(m_pos, boid.m_pos) <= m_raidus_flock)
            {
                steering += boid.m_vel;
                flockMates++;
            }
        }

        if (flockMates > 0)
        {
            Vector2 target = (s_player.transform.position - transform.position);

            steering.Normalize();
            steering *= m_speed;
            steering -= m_vel;
            steering = Vector2.ClampMagnitude(steering, m_maxForce);
        }

        return steering;
    }


    private Vector2 Cohesion(List<Bullet_Enemy_Boid> i_boidList)
    {
        // get all other flockmates within the radius

        // calculate the center position of all flockmates

        // steering toward the center position

        int flockMates = 0;
        Vector2 center = Vector2.zero;
        foreach (Bullet_Enemy_Boid boid in i_boidList)
        {
            if (boid != this && Vector2.Distance(m_pos, boid.m_pos) <= m_radius_cohere)
            {
                center += boid.m_pos;
                flockMates++;
            }
        }

        Vector2 steering = Vector2.zero;
        if (flockMates > 0)
        {
            center /= flockMates;
            steering = center - m_pos;
            steering.Normalize();
            steering *= m_speed;
            steering -= m_vel;
            steering = Vector2.ClampMagnitude(steering, m_maxForce);
        }

        return steering;
    }


    private Vector2 Separation(List<Bullet_Enemy_Boid> i_boidList)
    {
        // get all flockmates that is close enough to separate

        // for each flockemate, calculate the force that steer away from it

        // accumulate all steer force, calculate the average

        int flockMates = 0;
        Vector2 steering = Vector2.zero;
        foreach (Bullet_Enemy_Boid boid in i_boidList)
        {
            if (boid != this && Vector2.Distance(m_pos, boid.m_pos) <= m_radius_separate)
            {
                Vector2 force = (m_pos - boid.m_pos);
                // steer = steer / d
                force = force * (0.25f / Vector2.Distance(m_pos, boid.m_pos));
                steering += force;

                flockMates++;
            }
        }

        if (flockMates > 0)
        {
            steering.Normalize();
            steering *= m_speed;
            steering -= m_vel;
            steering = Vector2.ClampMagnitude(steering, m_maxForce);
        }

        return steering;
    }

    
    private Vector2 EvadePlayer()
    {
        Vector2 steering = Vector2.zero;
        if (Vector2.Distance(s_player.transform.position, m_pos) <= m_radius_evad)
        {
            steering = -1 * (s_player.transform.position - transform.position);
            steering.Normalize();
            steering *= m_speed * 3;
            steering = Vector2.ClampMagnitude(steering, m_maxForce);
        }

        return steering;
    }


    private Vector2 SeekPlayer()
    {
        Vector2 steering = Vector2.zero;

        steering = (s_player.transform.position - transform.position);
        steering.Normalize();
        steering *= m_speed;
        steering = Vector2.ClampMagnitude(steering, m_maxForce);

        return steering;
    }



    private void Flock(List<Bullet_Enemy_Boid> i_boidList)
    {
        Vector2 alignment = Align(i_boidList);
        ApplyForce(alignment);

        Vector2 cohesion = Cohesion(i_boidList);
        ApplyForce(cohesion);

        Vector2 separation = Separation(i_boidList);
        ApplyForce(separation);

        Vector2 evade = EvadePlayer();
        ApplyForce(evade);

        if (m_isLead)
        {
            Vector2 seek = SeekPlayer();
            ApplyForce(seek);
        }    
    }


    // Interface 

    public void Init(Vector2 i_pos, Vector2 i_vel, bool i_isLead, List<Bullet_Enemy_Boid> i_boids)
    {
        m_pos = i_pos;
        m_vel = i_vel;
        m_boids = i_boids;
        m_isLead = i_isLead;
        m_bornTime = Time.time;

        s_player = Player.Instance;

        transform.position = m_pos;
    }
}
