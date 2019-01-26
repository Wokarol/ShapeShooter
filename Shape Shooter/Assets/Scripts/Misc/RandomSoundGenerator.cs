using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundGenerator : MonoBehaviour
{
    [SerializeField] AudioSource source = null;
    [SerializeField] AudioClip[] clips = new AudioClip[0];
    [SerializeField] float interval = 1;

    float countdown;

    private void Update() {
        countdown -= Time.deltaTime;
        if(countdown < 0) {
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            countdown += interval;
        }
    }
}
