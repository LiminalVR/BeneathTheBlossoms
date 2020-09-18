using Liminal.Core.Fader;
using Liminal.SDK.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceScreenFaderSingleton : MonoBehaviour
{
    public CompoundScreenFader CompoundScreenFader;

    // Start is called before the first frame update
    void Awake()
    {
        ExperienceApp.Initializing += Init;
    }

    private void Init()
    {
        StartCoroutine(routine());
        IEnumerator routine()
        {
            CompoundScreenFader.enabled = false;
            yield return new WaitForEndOfFrame();
            CompoundScreenFader.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
