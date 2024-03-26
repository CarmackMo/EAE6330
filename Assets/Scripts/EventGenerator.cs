using UnityEngine;


public class EventGenerator : MonoBehaviour
{

    public NormalEventAsset normalEvent = null;

    private GameplayPanel gameplayPanel = null;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateEvent()
    {
        float pivot = 0.0f;
        string playerEvent = "";
        pivot = Random.Range(0.0f, 100.0f);

        if (pivot < 100.0f)
        {
            playerEvent = normalEvent.GetEvent();


        }
    }
}
