using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyGenerator : Singleton<EnemyGenerator>
{
    private double m_lastSpawnTime_rock = 0.0;
    private double m_lastSpawnTime_alien = 0.0;
    private double m_lastSpawnTime_boss = -20.0;
    private double m_lastSpawnTime_bonus = 0.0;
    private double m_spawnCoolDown_rock = 0.0;
    private double m_spawnCoolDown_boss = 0.0;
    private double m_spawnCoolDown_alien = 0.0;
    private double m_spawnCoolDown_bonus = 0.0;

    private bool isBossExist = false;

    private List<Enemy_Rock> m_rockList = new List<Enemy_Rock>();

    private GameController s_gameController = null;

    public List<Enemy_Rock> RockList { get { return m_rockList; } }



    [SerializeField] private Vector2 m_spawnCoolDownRange_rock = Vector2.zero;
    [SerializeField] private Vector2 m_spawnCoolDownRange_alien = Vector2.zero;
    [SerializeField] private Vector2 m_spawnCoolDownRange_boss = Vector2.zero;
    [SerializeField] private Vector2 m_spawnCoolDownRange_bonus = Vector2.zero;

    [SerializeField] private Vector2 m_enemySpeedRange_rock = Vector2.zero;
    [SerializeField] private Vector2 m_enemySpeedRange_alien = Vector2.zero;
    [SerializeField] private Vector2 m_enemySpeedRange_bonus = Vector2.zero;

    [SerializeField] private Enemy_Rock m_enemy_rock = null;
    [SerializeField] private Enemy_Alien m_enemy_alien = null;
    [SerializeField] private Enemy_Boss m_enemy_boss = null;    
    [SerializeField] private Bonus_Shotgun m_bonus_shotgun = null;
    [SerializeField] private Bonus_Laser m_bonus_laser = null;


    protected override void Start()
    {
        s_gameController = GameController.Instance;
        m_spawnCoolDown_rock = Random.Range(m_spawnCoolDownRange_rock.x, m_spawnCoolDownRange_rock.y);
        m_spawnCoolDown_alien = Random.Range(m_spawnCoolDownRange_alien.x, m_spawnCoolDownRange_alien.y);
        m_spawnCoolDown_boss = Random.Range(m_spawnCoolDownRange_boss.x, m_spawnCoolDownRange_boss.y);
        m_spawnCoolDown_bonus = Random.Range(m_spawnCoolDownRange_bonus.x, m_spawnCoolDownRange_bonus.y);
    }


    protected override void Update()
    {
        SpawnEnemy_Rock();
        SpawnEnemy_Alien();
        SpawnEnemy_Bonus();
        SpawnEnemy_Boss();
    }


    private void SpawnEnemy_Rock()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastSpawnTime_rock >= m_spawnCoolDown_rock)
        {
            Vector3 rockPos = Vector3.zero;
            rockPos.x = Random.Range(s_gameController.DownLeft.x + 0.5f, s_gameController.TopRight.x - 0.5f);
            rockPos.y = transform.position.y;

            Vector2 rockSpeed = Vector2.zero;
            rockSpeed.y = Random.Range(m_enemySpeedRange_rock.x, m_enemySpeedRange_rock.y);

            Enemy_Rock newRock = Instantiate(m_enemy_rock, rockPos, transform.rotation);
            newRock.Speed = rockSpeed;


            m_lastSpawnTime_rock = currentTime;
            m_spawnCoolDown_rock = Random.Range(m_spawnCoolDownRange_rock.x, m_spawnCoolDownRange_rock.y);
            RegisterRock(newRock);
        }
    }


    private void SpawnEnemy_Alien()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastSpawnTime_alien >= m_spawnCoolDown_alien)
        {
            Vector3 alienPos = Vector3.zero;
            alienPos.x = Random.Range(s_gameController.DownLeft.x + 0.5f, s_gameController.TopRight.x - 0.5f);
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

    private void SpawnEnemy_Boss()
    {
        float currentTime = Time.time;

        if (isBossExist == false && currentTime - m_lastSpawnTime_boss >= m_spawnCoolDown_boss)
        {
            Vector3 bossPos = Vector3.zero;
            bossPos.x = Random.Range(s_gameController.DownLeft.x + 0.5f, s_gameController.TopRight.x - 0.5f);
            bossPos.y = transform.position.y - 3.5f;

            Enemy_Boss newboss = Instantiate(m_enemy_boss, bossPos, transform.rotation);

            isBossExist = true;
            m_lastSpawnTime_boss = currentTime;
            m_spawnCoolDown_boss = Random.Range(m_spawnCoolDownRange_boss.x, m_spawnCoolDownRange_boss.y);
        }
    }


    private void SpawnEnemy_Bonus()
    {
        float currentTime = Time.time;

        if (currentTime - m_lastSpawnTime_bonus >= m_spawnCoolDown_bonus)
        {
            Vector3 bonusPos = Vector3.zero;
            bonusPos.x = Random.Range(s_gameController.DownLeft.x + 0.5f, s_gameController.TopRight.x - 0.5f);
            bonusPos.y = transform.position.y;

            float sign = Random.Range(-1.0f, 1.0f);
            Vector2 bounsSpeed = Vector2.zero;
            bounsSpeed.x = Random.Range(m_enemySpeedRange_bonus.x, m_enemySpeedRange_bonus.y);
            bounsSpeed.x *= (sign >= 0) ? 1 : -1;
            bounsSpeed.y = Random.Range(-2.0f, -1.0f);


            sign = Random.Range(0.0f, 100.0f);
            Bonus_Base bonus = null;
            if (sign > 50.0f)
                bonus = Instantiate(m_bonus_shotgun, bonusPos, transform.rotation);
            else
                bonus = Instantiate(m_bonus_laser, bonusPos, transform.rotation);
            //bonus = Instantiate(m_enemy_bonus, bonusPos, transform.rotation);
            bonus.Speed = bounsSpeed;


            m_lastSpawnTime_bonus = currentTime;
            m_spawnCoolDown_bonus = Random.Range(m_spawnCoolDownRange_bonus.x, m_spawnCoolDownRange_bonus.y);
        }
    }


    public void RegisterRock(Enemy_Rock i_rock)
    {
        m_rockList.Add (i_rock);
    }


    public void DeregisterRock(Enemy_Rock i_rock)
    {
        m_rockList.Remove(i_rock);
    }


    public void DeregisterBoss()
    {
        isBossExist = false;
    }

}
