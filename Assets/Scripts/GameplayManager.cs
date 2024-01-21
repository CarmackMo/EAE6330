using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameplayManager : Singleton<GameplayManager>
{
    public enum eGameState { On, Off };


    [SerializeField] private int m_mineNum = 0;
    public int MineNum { get { return m_mineNum; } }

    private int m_labeledMineNum = 0;
    private int m_labeledNum = 0;

    private Stack<Command<MineObject>> m_undoCmdStack = new Stack<Command<MineObject>>();
    private Stack<Command<MineObject>> m_redoCmdStack = new Stack<Command<MineObject>>();

    private eGameState m_gameState = eGameState.On;
    public eGameState GameState { get { return m_gameState; } set { m_gameState = value; } }

    private GameOverPanel m_gameOverPanel = null;



    protected override void Start()
    {
        base.Start();

        m_gameOverPanel = GameOverPanel.Instance;
    }


    protected override void Update()
    {
        base.Update();

        if (m_labeledMineNum == m_mineNum && m_labeledNum == m_labeledMineNum)
        {
            m_gameOverPanel.SetPanelVisible(true);
            m_gameOverPanel.SetContentVisible(true);
        }
    }


    public void ReloadScene()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.name, LoadSceneMode.Single);
    }


    public void LabelMine()
    {
        m_labeledMineNum++;
    }


    public void UnlabelMine()
    {
        m_labeledMineNum--;
    }


    public void Lable()
    {
        m_labeledNum++;
    }


    public void Unlabel()
    {
        m_labeledNum--;
    }



    public void RegisterUndoCmd(MineObject i_mineObject, Action<MineObject> i_undoAction, ECmdType i_type)
    {
        Command<MineObject> undoCmd = new Command<MineObject>(i_mineObject, i_undoAction, i_type);
        m_undoCmdStack.Push(undoCmd);
    }


    public void RegisterRedoCmd(MineObject i_mineObject, Action<MineObject> i_redoAction, ECmdType i_type)
    {
        Command<MineObject> redoCmd = new Command<MineObject>(i_mineObject, i_redoAction, i_type);
        m_redoCmdStack.Push(redoCmd); 
    }


    public void Undo()
    {
        Command<MineObject> undoCmd = null;
        if (m_undoCmdStack.TryPeek(out undoCmd))
        {
            if (undoCmd.CmdType == ECmdType.RightMouse)
            {
                Action<MineObject> action = r => r.OnRightMouseClick();
                RegisterRedoCmd(undoCmd.receiver, action, ECmdType.RightMouse);
            }
            else if (undoCmd.CmdType == ECmdType.ScrollMouse)
            {
                Action<MineObject> action = r => r.OnScrollMouseClick();
                RegisterRedoCmd(undoCmd.receiver, action, ECmdType.ScrollMouse);
            }

            undoCmd.Execute();
            m_undoCmdStack.Pop();
        }
        else
        {
            Debug.Log("Cannot undo as there is no undo command");
        }
    }


    public void Redo()
    {
        Command<MineObject> redoCmd = null;
        if (m_redoCmdStack.TryPeek(out redoCmd))
        {
            if (redoCmd.CmdType == ECmdType.RightMouse)
            {
                Action<MineObject> action = r => r.ReverseRightMouseClick();
                RegisterUndoCmd(redoCmd.receiver, action, ECmdType.RightMouse);
            }
            else if (redoCmd.CmdType == ECmdType.ScrollMouse)
            {
                Action<MineObject> action = r => r.ReverseScrollMouseClick();
                RegisterUndoCmd(redoCmd.receiver, action, ECmdType.ScrollMouse);
            }

            redoCmd.Execute();
            m_redoCmdStack.Pop();
        }
        else
        {
            Debug.Log("Cannot redo as there is no redo command");
        }
    }


    public void ResetRedoStack()
    {
        m_redoCmdStack.Clear();
    }
}
