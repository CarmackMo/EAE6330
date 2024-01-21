using UnityEngine;
using UnityEngine.UI;


public class GameOverPanel : Singleton<GameOverPanel>
{
    [SerializeField] private Button m_exitButton = null;
    [SerializeField] private Button m_restartButton = null;

    private GameplayManager m_gameplayManager = null;



    protected override void Start()
    {
        base.Start();

        m_exitButton.onClick.AddListener(() => { OnClickButton(m_exitButton); });
        m_restartButton.onClick.AddListener(() => { OnClickButton(m_restartButton); });

        m_gameplayManager = GameplayManager.Instance;

        SetPanelVisible(false);
    }


    private void OnClickButton(Button i_button)
    {
        if (i_button == m_exitButton) 
        {
            Application.Quit();
        }
        else if (i_button == m_restartButton)
        {
            m_gameplayManager.ReloadScene();
        }
    }


    public void SetPanelVisible(bool i_visible)
    {
        gameObject.SetActive(i_visible);
    }



}
