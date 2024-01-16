using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyGenerator : MonoBehaviour
{
    private double m_lastSpawnTime_rock = 0.0;
    private double m_lastSpawnTime_alien = 0.0;
    private double m_lastSpawnTime_bonus = 0.0;
    private double m_spawnCoolDown_rock = 0.0;
    private double m_spawnCoolDown_alien = 0.0;
    private double m_spawnCoolDown_bonus = 0.0;
    private GameController s_gameController = null;

    [SerializeField] private Vector2 m_spawnCoolDownRange_rock = Vector2.zero;
    [SerializeField] private Vector2 m_spawnCoolDownRange_alien = Vector2.zero;
    [SerializeField] private Vector2 m_spawnCoolDownRange_bonus = Vector2.zero;

    [SerializeField] private Vector2 m_enemySpeedRange_rock = Vector2.zero;
    [SerializeField] private Vector2 m_enemySpeedRange_alien = Vector2.zero;
    [SerializeField] private Vector2 m_enemySpeedRange_bonus = Vector2.zero;

    [SerializeField] private Enemy_Rock m_enemy_rock = null;
    [SerializeField] private Enemy_Alien m_enemy_alien = null;
    [SerializeField] private Enemy_Bonus m_enemy_bonus = null;


    private void Start()
    {
        s_gameController = GameController.Instance;
        m_spawnCoolDown_rock = Random.Range(m_spawnCoolDownRange_rock.x, m_spawnCoolDownRange_rock.y);
        m_spawnCoolDown_alien = Random.Range(m_spawnCoolDownRange_alien.x, m_spawnCoolDownRange_alien.y);
        m_spawnCoolDown_bonus = Random.Range(m_spawnCoolDownRange_bonus.x, m_spawnCoolDownRange_bonus.y);
    }


    private void Update()
    {
        SpawnEnemy_Rock();
        SpawnEnemy_Alien();
        SpawnEnemy_Bonus();
    }


    private void SpawnEnemy_Rock()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastSpawnTime_rock >= m_spawnCoolDown_rock)
        {
            Vector3 rockPos = Vector3.zero;
            rockPos.x = Random.Range(-1 * s_gameController.Boundary_H + 0.5f, s_gameController.Boundary_H - 0.5f);
            rockPos.y = transform.position.y;

            Vector2 rockSpeed = Vector2.zero;
            rockSpeed.y = Random.Range(m_enemySpeedRange_rock.x, m_enemySpeedRange_rock.y);

            Enemy_Rock newRock = Instantiate(m_enemy_rock, rockPos, transform.rotation);
            newRock.Speed = rockSpeed;


            m_lastSpawnTime_rock = currentTime;
            m_spawnCoolDown_rock = Random.Range(m_spawnCoolDownRange_rock.x, m_spawnCoolDownRange_rock.y);
        }
    }


    private void SpawnEnemy_Alien()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastSpawnTime_alien >= m_spawnCoolDown_alien)
        {
            Vector3 alienPos = Vector3.zero;
            alienPos.x = Random.Range(-1 * s_gameController.Boundary_H + 0.5f, s_gameController.Boundary_H - 0.5f);
            alienPos.y = transform.position.y;

            Vector2 alienSpeed = Vector2.zero;
            alienSpeed.x = Random.Range(m_enemySpeedRange_alien.x, m_enemySpeedRange_alien.y);
            alienSpeed.y = -1.0f;

            Enemy_Alien newAlien = Instantiate(m_enemy_alien, alienPos, transform.rotation);
            newAlien.Speed = alienSpeed;


            m_lastSpawnTime_alien = currentTime;
            m_spawnCoolDown_alien = Random.Range(m_spawnCoolDownRange_alien.x, m_spawnCoolDownRange_alien.y);
        }
    }


    private void SpawnEnemy_Bonus()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastSpawnTime_bonus >= m_spawnCoolDown_bonus)
        {
            Vector3 bonusPos = Vector3.zero;
            bonusPos.x = Random.Range(-1 * s_gameController.Boundary_H + 0.5f, s_gameController.Boundary_H - 0.5f);
            bonusPos.y = transform.position.y;

            float sign = Random.Range(-1.0f, 1.0f);
            Vector2 bounsSpeed = Vector2.zero;
            bounsSpeed.x = Random.Range(m_enemySpeedRange_bonus.x, m_enemySpeedRange_bonus.y);
            bounsSpeed.x *= (sign >= 0) ? 1 : -1;
            bounsSpeed.y = Random.Range(-2.0f, -1.0f);

            Enemy_Bonus newBonus = Instantiate(m_enemy_bonus, bonusPos, transform.rotation);
            newBonus.Speed = bounsSpeed;


            m_lastSpawnTime_bonus = currentTime;
            m_spawnCoolDown_bonus = Random.Range(m_spawnCoolDownRange_bonus.x, m_spawnCoolDownRange_bonus.y);
        }
    }

}
