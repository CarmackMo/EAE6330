using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NormalEventAssets", menuName = "ScriptableObject/NormalEventAssets")]
public class NormalEventAsset : ScriptableObject
{

    // Data
    //=========================

    [SerializeField]
    private List<string> eventList = new List<string>();

    private HashSet<int> acquireID = new HashSet<int>();


    // Implementation
    //=========================

    public string GetEvent()
    {
        int idx = UnityEngine.Random.Range(0, eventList.Count);

        while (acquireID.Contains(idx)) 
        {
            idx = UnityEngine.Random.Range(0, eventList.Count);
        }

        acquireID.Add(idx);

        return eventList[idx];
    }


}
