using UnityEngine;
using TMPro;

public class MineObject : MonoBehaviour
{
    [SerializeField] private GameObject m_mine_unchecked = null;
    [SerializeField] private GameObject m_mine_labled = null;
    [SerializeField] private GameObject m_mine_indicator = null;

    [SerializeField] private Canvas m_canvas = null;
    [SerializeField] private TextMeshProUGUI m_text = null;


    public enum e_MineType { Mine, Normal };
    public enum e_MineState { UnChecked, Checked, Labeled  };

    public e_MineType Type { get { return m_type; } set { m_type = value; } }
    [SerializeField] private e_MineType m_type = e_MineType.Normal;
    public e_MineState State { get { return m_state; } set { m_state = value; } }   
    private e_MineState m_state = e_MineState.UnChecked;




    private void Start()
    {
        m_canvas.worldCamera = Camera.main;
    }


    private void Update()
    {
        m_mine_indicator.transform.LookAt(Camera.main.transform);
    }


    public void OnRightMouseClick()
    {
        m_mine_unchecked.SetActive(false);
        m_mine_indicator.SetActive(true);
    }


    public void OnScrollMouseClick() 
    {
        m_mine_unchecked.SetActive(false);
        m_mine_labled.SetActive(true);
    }


    public void InitialzieIndicator(int i_mineNear)
    {
        m_text.text = i_mineNear.ToString();
    }


    // Test
    public void SetColor()
    {
        m_text.color = Color.red;
    }

}
