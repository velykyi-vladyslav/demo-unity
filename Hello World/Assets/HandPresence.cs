using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public GameObject handModelPrefab;

    public InputDeviceCharacteristics controllerCharacteristics;

    private GameObject spawnedHandModel;

    private Animator handAnimator;

    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            Debug.Log("Device is not valid, reinitializing " + targetDevice);

            TryInitialize();
        }
        else
        {
            Debug.Log("Update hand ");

            UpdateHandAnimation();
        }
    }

    void UpdateHandAnimation()
    {
        Debug.Log("Update hand, TARGET DEVICE " + targetDevice);

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    private void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
 
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);


        if (devices.Count>0)
        {
            targetDevice = devices[0];

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
            spawnedHandModel.SetActive(true);
        }

        Debug.Log("My log 2 " + targetDevice);

    }
}
