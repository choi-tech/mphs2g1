using System.Collections;
using UnityEngine;

public class lgmovejump : MonoBehaviour
{
    [Header("Jumpscare Object Settings")]
    public GameObject jumpscareObject; // 점프스케어 오브젝트
    public float displayDuration = 2f; // 점프스케어 표시 시간
    public AudioClip jumpscareSound; // 점프스케어 사운드 클립

    private AudioSource audioSource; // 오디오 소스
    private Animator jumpscareAnimator; // 점프스케어 오브젝트의 애니메이터
    private bool hasTriggered = false; // 점프스케어 트리거 상태

    private void Start()
    {
        // 점프스케어 오브젝트 비활성화
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(false);
            jumpscareAnimator = jumpscareObject.GetComponent<Animator>();
        }

        // 오디오 소스 설정
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // 트리거 활성화
            StartCoroutine(ActivateJumpscare());
        }
    }

    private IEnumerator ActivateJumpscare()
    {
        // 점프스케어 오브젝트 활성화
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(true);
        }

        // 애니메이션 실행
        if (jumpscareAnimator != null)
        {
            jumpscareAnimator.SetTrigger("ActivateJumpscare");
        }

        // 점프스케어 사운드 재생
        if (jumpscareSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }

        // 점프스케어 표시 시간 동안 대기
        yield return new WaitForSeconds(displayDuration);

        // 점프스케어 오브젝트 비활성화
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(false);
        }

        // 트리거 오브젝트 제거
        Destroy(gameObject);
    }
}