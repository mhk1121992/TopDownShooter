using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static MenuMusic instance;

    private void Awake()
    {
    	if (instance == null)
    	{
    		instance = this;
    		DontDestroyOnLoad(instance);
    	}
    	else
    	{
    		Destroy(gameObject);
    	}
    }
}
