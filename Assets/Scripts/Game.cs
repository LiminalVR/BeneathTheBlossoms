using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Core.Fader;
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject windPrefab;
    private GameObject wind;
    [SerializeField] Transform hand;
    bool hasTarget = false;
    Transform target;

    private void Awake()
    {
        ServiceLocator.Register<Game>(this);
    }

    private void Start()
    {
        wind = Instantiate(windPrefab);
        wind.transform.position = hand.position;
        wind.SetActive(false);
    }

    private void Update()
    {
        var avatar = VRAvatar.Active;
        if (avatar == null)
            return;

        bool hold = VRDevice.Device.GetButton(VRButton.One);
        if (hold && hasTarget)
        {
            wind.transform.position = target.position;
            wind.GetComponent<Wind>().SetInitialVelocity(-hand.forward);
            wind.SetActive(true);
        }
        else
        {
            wind.SetActive(false);
        }

        // Any input
        // VRDevice.Device.GetButtonDown(VRButton.One);
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

    /// <summary>
    /// End will only close the application when you're within the platform
    /// </summary>
    public void End()
    {
        ExperienceApp.End();
    }

    public void SetTarget(Transform transform)
    {
        hasTarget = true;
        target = transform;
    }

    public void UnsetTarget(Transform transform) 
    {
        if(transform == target)
            hasTarget = false;
    }

}
