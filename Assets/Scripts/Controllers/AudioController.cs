using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip ballHit;
    [SerializeField] private AudioClip ballGoal;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play audio when the ball hits something.
    /// </summary>
    public void PlayBallHit()
    {
        if (!audioSource) return;
        audioSource.PlayOneShot(ballHit);
    }

    /// <summary>
    /// When the ball hits the gaol, play a sound.
    /// </summary>
    public void PlayBallGoal()
    {
        if (!audioSource) return;
        audioSource.PlayOneShot(ballGoal);
    }
}
