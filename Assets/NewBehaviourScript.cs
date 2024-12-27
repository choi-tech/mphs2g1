using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabToDisappearWithSound : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip disappearSound; // 사라질 때 재생할 사운드
    public bool useCustomAudioSource = false; // 사용자 지정 AudioSource 사용 여부
    public AudioSource customAudioSource; // 사용자 정의 AudioSource

    [Header("General Settings")]
    public float destroyDelay = 1.0f; // 사운드 재생 후 오브젝트가 삭제되기까지의 지연 시간

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 기본 AudioSource가 없으면 자동으로 추가
        if (!useCustomAudioSource && customAudioSource == null)
        {
            customAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // 사운드 재생
        if (disappearSound != null)
        {
            if (useCustomAudioSource && customAudioSource != null)
            {
                customAudioSource.PlayOneShot(disappearSound);
            }
            else if (customAudioSource != null)
            {
                customAudioSource.PlayOneShot(disappearSound);
            }
        }

        // 오브젝트 삭제
        StartCoroutine(DestroyAfterDelay());
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}