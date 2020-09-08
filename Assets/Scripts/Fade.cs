using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Text text;
    [SerializeField] float fadeOutTime;
    Color originColor;
    float currTime = 0;
    
    public void FadeOut()
    {
        originColor = text.color;
        button.interactable = false;
        StartCoroutine("FadeOutRoutine");
    }

    public IEnumerator FadeOutRoutine() 
    {
        while (text.color != Color.clear) 
        {
            text.color = Vector4.Lerp(originColor, Color.clear, currTime / fadeOutTime);
            currTime += Time.deltaTime;
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
