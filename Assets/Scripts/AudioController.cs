using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioController : MonoBehaviour
{
    public AudioMixer mixer;
    public RoomController currentRoom;

    private float lowPassTarget = 0f;
    private float currentLowPass = 0f;
    void FixedUpdate()
    {
        if (currentRoom == null) return;

        switch (currentRoom.hasOxygen)
        {
            case true: 
                lowPassTarget = 1f;
                break;
            case false:
                lowPassTarget = 0f;
                break;
        }
        if (currentLowPass > lowPassTarget) currentLowPass -= 0.0005f;
        if (currentLowPass < lowPassTarget) currentLowPass += 0.0005f;
        mixer.SetFloat("LowPass", Mathf.Lerp(143f, 22000f, currentLowPass));
    }
}
