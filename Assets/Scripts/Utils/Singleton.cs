using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake()
    {
        Singleton<T> singleton = this;
        if (_instance != null)
        {
            //Debug.LogError("A instance already exists");
            Destroy(singleton.gameObject); //Or GameObject as appropriate
            return;
        }
        _instance = (T)FindObjectOfType(typeof(T));
    }
}
