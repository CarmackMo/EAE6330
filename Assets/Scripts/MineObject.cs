using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObject : MonoBehaviour
{

    [SerializeField] private GameObject m_mine_unchecked = null;

    [SerializeField] private GameObject m_mine_labled = null;

    [SerializeField] private GameObject m_mine_indicator = null;

    


    public void OnRightMouseClick()
    {

        m_mine_unchecked.SetActive(false);

    }


    public void OnScrollMouseClick() 
    {
        m_mine_unchecked.SetActive(false);
        m_mine_labled.SetActive(true);
    }
}
