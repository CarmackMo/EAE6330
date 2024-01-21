using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameplayManager : Singleton<GameplayManager>
{

    [SerializeField] private int m_mineNum = 3;
    public int MineNum { get { return m_mineNum; } }

    private int m_labeledMineNum = 0;

    private Stack<Command<MineObject>> m_undoCmdStack = new Stack<Command<MineObject>>();
    private Stack<Command<MineObject>> m_redoCmdStack = new Stack<Command<MineObject>>();

    private GameOverPanel m_gameOverPanel = null;


    protected override void Start()
    {
        base.Start();

        m_gameOverPanel = GameOverPanel.Instance;
    }


    public void ReloadScene()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.name, LoadSceneMode.Single);
    }


    public void LabelMine()
    {
        m_labeledMineNum++;

        if (m_labeledMineNum == m_mineNum)
        {
            m_gameOverPanel.SetPanelVisible(true);
            m_gameOverPanel.SetContentVisible(true);
        }
    }


    public void UnlabelMine()
    {
        m_labeledMineNum--;
    }


    public void RegisterUndoCmd(MineObject i_mineObject, Action<MineObject> i_undoAction)
    {
        Command<MineObject> undoCmd = new Command<MineObject>(i_mineObject, i_undoAction);
        m_undoCmdStack.Push(undoCmd);
    }


    public void RegisterRedoCmd(MineObject i_mineObject, Action<MineObject> i_redoAction)
    {
        Command<MineObject> redoCmd = new Command<MineObject>(i_mineObject, i_redoAction);
        m_redoCmdStack.Push(redoCmd); 
    }


    public void Undo()
    {

    }
}
