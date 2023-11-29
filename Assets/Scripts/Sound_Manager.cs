using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static AudioClip car_hit, car_horn, car_drive;
    public static AudioSource audioSrc;
    void Start()
    {
        car_hit = Resources.Load<AudioClip>("car_hit");
        car_horn = Resources.Load<AudioClip>("car_horn");
        car_drive = Resources.Load<AudioClip>("car_drive");
        audioSrc = GameObject.FindObjectOfType<Sound_Manager>().GetComponent<AudioSource>();
    }

    public static void playsound(string clip)
    {
        switch (clip)
        {
            case "car_hit":
                audioSrc.PlayOneShot(car_hit);
                break;
            case "car_horn":
                audioSrc.volume = 0.2f;
                audioSrc.PlayOneShot(car_horn);
                break;
            case "car_drive":
                audioSrc.volume = 0.3f;
                audioSrc.PlayOneShot(car_drive);
                break;
        }
    }
    public static void stopsound(string clip)
    {
        switch (clip)
        {
            case "car_drive":
                audioSrc.volume = 0.5f;
                audioSrc.loop = false;
                audioSrc.Stop();
                break;
        }
    }
}
