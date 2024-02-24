using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : Singleton<InputController>
{
    // Data
    //=================

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset m_inputAction = null;

    [SerializeField] private string m_name_actionMap_Cursor = null;
    [SerializeField] private string m_name_moveLeft = null;
    [SerializeField] private string m_name_moveRight = null;
    [SerializeField] private string m_name_moveUp = null;
    [SerializeField] private string m_name_moveDown = null;

    private InputAction m_action_moveLeft = null;
    private InputAction m_action_moveRight = null;
    private InputAction m_action_moveUp = null;
    private InputAction m_action_moveDown = null;

    private GameController s_gameController = null;


    // Implementations
    //=================

    protected override void Start()
    {
        base.Start();

        Init();
    }


    private void Init()
    {
        // initialize static variable
        {
            s_gameController = GameController.Instance;
        }

        // Initialize input system
        {
            m_action_moveLeft = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveLeft);
            m_action_moveRight = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveRight);
            m_action_moveUp = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveUp);
            m_action_moveDown = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveDown);

            m_action_moveLeft.performed += context => s_gameController.MoveCursor(m_action_moveLeft);
            m_action_moveRight.performed += context => s_gameController.MoveCursor(m_action_moveRight);
            m_action_moveUp.performed += context => s_gameController.MoveCursor(m_action_moveUp);
            m_action_moveDown.performed += context => s_gameController.MoveCursor(m_action_moveDown);
        }


    }



}
