using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnNewScene : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
