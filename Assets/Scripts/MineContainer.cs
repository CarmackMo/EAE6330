using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;


public class MineContainer : MonoBehaviour
{

    [SerializeField] private List<MineObject> m_mineList = new List<MineObject>(9);

    private int m_width = 3;
    private int m_length = 3;
    private int m_height = 1;


    private int m_mineNum = 3;




    private void Start()
    {
        InitializeMineContainer();
    }


    private void InitializeMineContainer()
    {

        GenerateMines();

        for (int y = 0; y < m_height; y++)
        {
        for (int z = 0; z < m_length; z++)
        {
        for (int x = 0; x < m_width; x++)
        {
            int mineNear = 0;

            if (x + 1 < m_width && m_mineList[CoordToIdx(x + 1, y, z)].Type == MineObject.e_MineType.Mine)
                mineNear++;
            if (x - 1 >= 0 && m_mineList[CoordToIdx(x - 1, y, z)].Type == MineObject.e_MineType.Mine)
                mineNear++;
            if (y + 1 < m_height && m_mineList[CoordToIdx(x, y + 1, z)].Type == MineObject.e_MineType.Mine)
                mineNear++;
            if (y - 1 >= 0 && m_mineList[CoordToIdx(x, y - 1, z)].Type == MineObject.e_MineType.Mine)
                mineNear++;
            if (z + 1 < m_length && m_mineList[CoordToIdx(x, y, z + 1)].Type == MineObject.e_MineType.Mine)
                mineNear++;
            if (z - 1 >= 0 && m_mineList[CoordToIdx(x, y, z - 1)].Type == MineObject.e_MineType.Mine)
                mineNear++;


            m_mineList[CoordToIdx(x, y, z)].InitialzieIndicator(mineNear);
        }
        }
        }
    }


    private void GenerateMines()
    {
        for (int i = 0; i < m_mineNum; i++) 
        {
            int x = Random.Range(0, m_width);
            int y = Random.Range(0, m_height);
            int z = Random.Range(0, m_length);

            while (m_mineList[CoordToIdx(x, y, z)].Type == MineObject.e_MineType.Mine)
            {
                x = Random.Range(0, m_width);
                y = Random.Range(0, m_height);
                z = Random.Range(0, m_length);
            }

            m_mineList[CoordToIdx(x, y, z)].Type = MineObject.e_MineType.Mine;
        }
    }


    private int CoordToIdx(int i_x, int i_y, int i_z)
    {
        return (i_y * 9) + (i_z * 3) + i_x;
    }

}
