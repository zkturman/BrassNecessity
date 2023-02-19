using UnityEngine;

public class FootSoundComponent : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private ControllerSoundManager _soundData;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();    
    }
    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            _soundData.PlayFootStepSound(transform.TransformPoint(_controller.center));
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            _soundData.PlayLandSound(transform.TransformPoint(_controller.center));
        }
    }
}
