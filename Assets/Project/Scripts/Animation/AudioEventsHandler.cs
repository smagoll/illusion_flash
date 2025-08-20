using UnityEngine;

public class AnimationAudioEvents : MonoBehaviour
{
    [Header("Foot Bones")]
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;

    [Header("Sounds")]
    [SerializeField] private SoundData leftFootSound;
    [SerializeField] private SoundData rightFootSound;

    private Animator animator;

    private float prevLeftCurve;
    private float prevRightCurve;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float leftCurve = animator.GetFloat("Footstep_L");
        float rightCurve = animator.GetFloat("Footstep_R");

        if (leftCurve > 0.5f && prevLeftCurve <= 0.5f)
            PlayFootstep(leftFootSound, leftFoot);

        if (rightCurve > 0.5f && prevRightCurve <= 0.5f)
            PlayFootstep(rightFootSound, rightFoot);

        prevLeftCurve = leftCurve;
        prevRightCurve = rightCurve;
    }

    private void PlayFootstep(SoundData soundData, Transform footTransform)
    {
        if (soundData == null)
        {
            Debug.LogError("[AnimationAudioEvents] SoundData is null!");
            return;
        }

        Vector3 playPos = footTransform != null ? footTransform.position : transform.position;
        AudioSystem.Instance.Play(soundData.eventName, playPos);
    }
}