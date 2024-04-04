using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct EventResult
{
    public string text;

    public int deltaWealth;
    public int deltaStrength;
}


[CreateAssetMenu(fileName = "EventAssets", menuName = "ScriptableObject/EventAssets")]
public class EventAsset : ScriptableObject
{

    // Data
    //=========================

    [Serializable]
    public struct EventConfig
    {
        public string background;

        public List<string> badResultList;
        public List<string> goodResultList;

    }

    [SerializeField]
    private List<EventConfig> eventList = new List<EventConfig>();




    // Interface
    //=========================

    public EventResult GetEvent(bool status)
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


        // Get substring
        EventResult result;
        char deltaStrength = res[res.Length - 2];
        char deltaWealth = res[res.Length - 4];
        int strength= 0;
        int wealth = 0;

        if (Convert.ToInt32(deltaStrength) <= 122 && Convert.ToInt32(deltaStrength) >= 97)
            strength = -1 * (Convert.ToInt32(deltaStrength) - 96);
        else
            strength = int.Parse(deltaStrength.ToString());

        if (Convert.ToInt32(deltaWealth) <= 122 && Convert.ToInt32(deltaWealth) >= 97)
            wealth = -1 * (Convert.ToInt32(deltaWealth) - 96);
        else
            wealth = int.Parse(deltaWealth.ToString());

        res = res.Substring(0, res.Length - 5);

        result.text = res;
        result.deltaStrength = strength;
        result.deltaWealth = wealth;

        return result;
    }

}
