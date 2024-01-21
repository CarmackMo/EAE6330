using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlManager : Singleton<ControlManager>
{
    [SerializeField] private float m_rotationSpeed = 0.0f;

    [SerializeField] private GameObject m_mineContaimer = null;

    private GameplayManager m_gameplayManager = null;



    protected override void Start()
    {
        base.Start();

        m_gameplayManager = GameplayManager.Instance;
    }


    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            float mousX = Input.GetAxis("Mouse X");
            float mousY = Input.GetAxis("Mouse Y");
            m_mineContaimer.transform.Rotate(mousY * m_rotationSpeed, -mousX * m_rotationSpeed, 0, Space.World);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000) )
            {
                MineObject mineObj = hit.collider.gameObject.GetComponent<MineObject>();
                if (mineObj != null) 
                {
                    mineObj.OnRightMouseClick();
                }
            }
        }
        else if (Input.GetMouseButtonUp(2)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                MineObject mineObj = hit.collider.gameObject.GetComponent<MineObject>();
                if (mineObj != null)
                {
                    mineObj.OnScrollMouseClick();
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) 
        {
            if (Input.GetKeyUp(KeyCode.Z))
                m_gameplayManager.Undo();
            else if (Input.GetKeyUp(KeyCode.Y))
                m_gameplayManager.Redo();
        }
    }
}
