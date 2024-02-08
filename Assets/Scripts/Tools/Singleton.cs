using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;


    protected virtual void Awake()
    {
        if (instance == null)
            instance = GameObject.FindAnyObjectByType<T>();

        if (instance != null && instance.GetInstanceID() != this.GetInstanceID())
            Destroy(gameObject);
    }

    protected abstract void Start();

    protected abstract void Update();

    protected abstract void OnDestroy();
}
