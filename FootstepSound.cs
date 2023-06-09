using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootstepSound : MonoBehaviour
// Plays footsteps at the end of each headbob animation.
{
    // animation
    private AnimationClip HeadbobAnimation;

    // audio
    public AudioSource AudioSource;
    public AudioClip DefaultFootstep;
    public AudioClip GrassFootstep;

    void Awake()
    {
        HeadbobAnimation = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;

        AnimationEvent onHeadbobAnimationFinish = new AnimationEvent();
        onHeadbobAnimationFinish.time = HeadbobAnimation.length;
        onHeadbobAnimationFinish.functionName = "OnHeadbobAnimationFinish";
        onHeadbobAnimationFinish.stringParameter = HeadbobAnimation.name;

        HeadbobAnimation.AddEvent(onHeadbobAnimationFinish);

        AudioSource = GameObject.Find("PlayerCapsule").GetComponent<AudioSource>();
    }

    public void OnHeadbobAnimationFinish(string animationName)
    {
        PlayFootstep(GetFootstepAudio());
    }

    private AudioClip GetFootstepAudio()
    // Returns the appropriate footstep audio to play depending on what type of ground the
    // player is walking on.
    {
        if (FirstPersonController.GroundLayer == 10)
        // if GroundLayer is Grass
        {
            return GrassFootstep;
        }

        return DefaultFootstep; // if no special layer is found, return default footstep
    }

    private void PlayFootstep(AudioClip FootstepAudio)
    {
        AudioSource.pitch = Random.Range(0.9f, 1.1f);
        AudioSource.PlayOneShot(FootstepAudio);
    }
}