using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSelector : MonoBehaviour
{
    [SerializeField]
    private Collection platforms;

    private GameObject currentPlatform;
    private GameObject nextPlatform;    // Used for multiple platform mode


    void Awake()
    {
        currentPlatform = Instantiate(platforms.GetObject(), transform);
    }


}
