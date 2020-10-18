using Liminal.SDK.VR.Avatars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideControllers : MonoBehaviour
{
    public VRAvatarController PrimaryController;
    public bool PrimaryEnabled;
    public VRAvatarController SecondaryController;
    public bool SecondaryEnabled;
    public bool ShowControllers;

    // Update is called once per frame
    void Update()
    {
        if (PrimaryController != null && PrimaryController.gameObject.activeSelf != (ShowControllers|| PrimaryEnabled))
        {
            PrimaryController.gameObject.SetActive((ShowControllers || PrimaryEnabled));
        }

        if (SecondaryController != null && SecondaryController.gameObject.activeSelf != (ShowControllers || SecondaryEnabled))
        {
            SecondaryController.gameObject.SetActive((ShowControllers || SecondaryEnabled));
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PrimaryEnabled = !PrimaryEnabled;
        }
    }

    public void SetShowControllers(bool state)
    {
        ShowControllers = state;
    }
}
