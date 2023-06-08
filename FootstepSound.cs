using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootstepSound : MonoBehaviour
// Plays footsteps at the end of each headbob animation.
{
    // animation
    public Animator headbobAnimator;
    private AnimationClip headbobAnimation;

    // audio
    public AudioSource playerCapsuleAudioSource;

    void Awake()
    {
        headbobAnimator = GetComponent<Animator>();
        headbobAnimation = headbobAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;

        AnimationEvent onHeadbobAnimationFinish = new AnimationEvent();
        onHeadbobAnimationFinish.time = headbobAnimation.length;
        onHeadbobAnimationFinish.functionName = "OnHeadbobAnimationFinish";
        onHeadbobAnimationFinish.stringParameter = headbobAnimation.name;
        
        headbobAnimation.AddEvent(onHeadbobAnimationFinish);

        playerCapsuleAudioSource = GameObject.Find("PlayerCapsule").GetComponent<AudioSource>();
    }

    public void OnHeadbobAnimationFinish(string animationName)
    {
        playerCapsuleAudioSource.pitch = Random.Range(0.9f, 1.1f);
        playerCapsuleAudioSource.Play(); // play footstep
    }
}