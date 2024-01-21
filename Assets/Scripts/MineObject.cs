using UnityEngine;
using TMPro;

public class MineObject : MonoBehaviour
{
    [SerializeField] private GameObject m_mine_unchecked = null;
    [SerializeField] private GameObject m_mine_labled = null;
    [SerializeField] private GameObject m_mine_indicator = null;
    [SerializeField] private GameObject m_mine_mine = null;

    [SerializeField] private Canvas m_canvas = null;
    [SerializeField] private TextMeshProUGUI m_text = null;

    public enum e_MineType { Mine, Normal };
    public enum e_MineState { UnChecked, Checked, Labeled  };

    public e_MineType Type { get { return m_type; } set { m_type = value; } }
    private e_MineType m_type = e_MineType.Normal;
    public e_MineState State { get { return m_state; } set { m_state = value; } }   
    private e_MineState m_state = e_MineState.UnChecked;

    private GameOverPanel m_gameOverPanel = null;
    private GameplayManager m_gameplayManager = null;
    private MineContainer m_mineContainer = null;
    public MineContainer MineContainer { set { m_mineContainer = value; } }


    private void Start()
    {
        m_canvas.worldCamera = Camera.main;
        m_gameOverPanel = GameOverPanel.Instance;
        m_gameplayManager = GameplayManager.Instance;
    }


    private void Update()
    {
        m_mine_indicator.transform.LookAt(Camera.main.transform);
    }


    public void OnRightMouseClick()
    {
        if (m_type == e_MineType.Mine)
        {
            m_gameOverPanel.SetPanelVisible(true);
            m_gameOverPanel.SetContentVisible(false);
            m_mine_unchecked.SetActive(false);
            m_mine_mine.SetActive(true);
            m_gameplayManager.GameState = GameplayManager.eGameState.Off;
        }
        else
        {
            m_state = e_MineState.Checked;
            m_mine_unchecked.SetActive(false);
            m_mine_indicator.SetActive(true);
            m_mineContainer.RefreshMineIndicator();
        }

        m_gameplayManager.RegisterUndoCmd(this, r => r.ReverseRightMouseClick(), ECmdType.RightMouse);
    }


    public void ReverseRightMouseClick()
    {
        if (m_type == e_MineType.Mine)
        {
            m_gameOverPanel.SetPanelVisible(false);
            m_mine_unchecked.SetActive(true);
            m_mine_mine.SetActive(false);
            m_gameplayManager.GameState = GameplayManager.eGameState.On;
        }
        else
        {
            m_state = e_MineState.UnChecked;
            m_mine_unchecked.SetActive(true);
            m_mine_indicator.SetActive(false);
        }
    }


    public void OnScrollMouseClick() 
    {
        if (m_state == e_MineState.UnChecked)
        {
            m_state = e_MineState.Labeled;
            m_mine_unchecked.SetActive(false);
            m_mine_labled.SetActive(true);

            if (m_type == e_MineType.Mine)
            {
                m_gameplayManager.LabelMine();
            }
            m_gameplayManager.Lable();
        }
        else if (m_state == e_MineState.Labeled)
        {
            m_state = e_MineState.UnChecked;
            m_mine_unchecked.SetActive(true);
            m_mine_labled.SetActive(false);
            
            if (m_type == e_MineType.Mine)
            {
                m_gameplayManager.UnlabelMine();
            }
            m_gameplayManager.Unlabel();
        }

        m_gameplayManager.RegisterUndoCmd(this, r => r.ReverseScrollMouseClick(), ECmdType.ScrollMouse);
    }


    public void ReverseScrollMouseClick()
    {
        OnScrollMouseClick();
    }


    public void InitialzieIndicator(int i_mineNear)
    {
        m_text.text = i_mineNear.ToString();
    }

    public void HideIndicator()
    {
        m_mine_indicator.SetActive(false);
    }

}
