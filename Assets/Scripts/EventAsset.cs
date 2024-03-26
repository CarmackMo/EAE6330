using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EventAssets", menuName = "ScriptableObject/EventAssets")]
public class EventAsset : ScriptableObject
{

    // Data
    //=========================

    [Serializable]
    public struct EventConfig
    {
        public string background;

        public List<string> goodResultList;

        public List<string> badResultList;
    }

    [SerializeField]
    private List<EventConfig> eventList = new List<EventConfig>();



    // Implementation
    //=========================

    public string GetEvent(bool status)
    {
        int idx = 0;
        string res = "";

        idx = UnityEngine.Random.Range(0, eventList.Count);
        EventConfig config = eventList[idx];

        res += config.background;

        if (status) 
        {
            idx = UnityEngine.Random.Range(0, config.goodResultList.Count);
            if (config.goodResultList.Count > 0)
                res += config.goodResultList[idx];
        }
        else
        {
            idx = UnityEngine.Random.Range(0, config.badResultList.Count);
            if (config.badResultList.Count > 0)
                res += config.badResultList[idx];
        }

        return res;
    }

}
