// AUTHOR: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: AudioMgr.cs
/* FILE DESCRIPTION: Manages audio that is played in the game. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    // Audio

public class AudioMgr : MonoBehaviour
{
    /*---------- AWAKE ----------*/
    // Static Instance of AudioMgr for global usage
    public static AudioMgr inst;
    private void Awake()
    {
        inst = this;
    }

    /*---------- Properties ----------*/
    [Header("Background Music")]
    public List<AudioClip> backgroundClips;

    public int currentBackgroundIndex = 0;

    public AudioSource backgroundSource;

    [Header("Footstep")]
    public List<AudioClip> footsteps;

    public AudioSource footstep;
    public float footstepCooldown;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        backgroundSource.volume = .2f;
        backgroundSource.clip = backgroundClips[currentBackgroundIndex];
        backgroundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if current track is no longer playing
        if (!backgroundSource.isPlaying)
        {
            // If current track is last in list, wrap to front
            if(currentBackgroundIndex + 1 >= backgroundClips.Count)
            {
                backgroundSource.clip = backgroundClips[0];
            }
            // Change current track to the next track
            else
            {
                currentBackgroundIndex++;
                backgroundSource.clip = backgroundClips[currentBackgroundIndex];
            }

            // Play new track
            backgroundSource.Play();
        }
    }

    public void PlayFootstep()
    {
        int index = Random.Range(0, (footsteps.Count - 1));

        footstep.clip = footsteps[index];
        footstep.Play();
    }
}
