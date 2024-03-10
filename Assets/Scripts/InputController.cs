using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : Singleton<InputController>
{
    // Data
    //=================

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset m_inputAction = null;

    [Header("Input Action Name")]
    [SerializeField] private string m_name_actionMap_Cursor = null;
    [SerializeField] private string m_name_actionMap_Gameplay = null;
    [SerializeField] private string m_name_moveLeft = null;
    [SerializeField] private string m_name_moveRight = null;
    [SerializeField] private string m_name_moveUp = null;
    [SerializeField] private string m_name_moveDown = null;
    [SerializeField] private string m_name_probing = null;
    [SerializeField] private string m_name_undo = null;
    [SerializeField] private string m_name_exit = null;

    private InputAction m_action_moveLeft = null;
    private InputAction m_action_moveRight = null;
    private InputAction m_action_moveUp = null;
    private InputAction m_action_moveDown = null;
    private InputAction m_action_probing = null;
    private InputAction m_action_undo = null;
    private InputAction m_action_exit = null;


    private GameController s_gameController = null;
    private GameplayPanel s_gameplayPanel = null;   


    // Implementations
    //=================

    protected override void Start()
    {
        base.Start();

        Init();
    }


    private void Init()
    {
        // Initialize static variable
        {
            s_gameController = GameController.Instance;
            s_gameplayPanel = GameplayPanel.Instance;
        }

        // Initialzie input actions
        {
            m_action_moveLeft = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveLeft);
            m_action_moveRight = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveRight);
            m_action_moveUp = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveUp);
            m_action_moveDown = m_inputAction.FindActionMap(m_name_actionMap_Cursor).FindAction(m_name_moveDown);

            m_action_probing = m_inputAction.FindActionMap(m_name_actionMap_Gameplay).FindAction(m_name_probing);
            m_action_undo = m_inputAction.FindActionMap(m_name_actionMap_Gameplay).FindAction(m_name_undo);
            m_action_exit = m_inputAction.FindActionMap(m_name_actionMap_Gameplay).FindAction(m_name_exit);
        }

        // Register action callback
        {
            m_action_moveLeft.performed += context => s_gameController.MoveCursor(new Vector2Int(-1, 0));
            m_action_moveRight.performed += context => s_gameController.MoveCursor(new Vector2Int(1, 0));
            m_action_moveUp.performed += context => s_gameController.MoveCursor(new Vector2Int(0, 1));
            m_action_moveDown.performed += context => s_gameController.MoveCursor(new Vector2Int(0, -1));
            m_action_probing.performed += context => s_gameController.ProbTile();
            m_action_undo.performed += context => s_gameController.Undo();
            m_action_exit.performed += context => s_gameplayPanel.ShowExitPanel();
        }

        // Enable input actions
        {
            m_action_moveLeft.Enable();
            m_action_moveRight.Enable();
            m_action_moveUp.Enable();
            m_action_moveDown.Enable();
            m_action_probing.Enable();
            m_action_undo.Enable();
            m_action_exit.Enable(); 
        }
    }



}
