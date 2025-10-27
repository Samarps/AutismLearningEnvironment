using UnityEngine;
using UnityEngine.UI;

public class RewardEffect : MonoBehaviour
{
    [Header("Audio & Visual Feedback")]
    public AudioSource audioSource;
    public AudioClip successSound;
    public ParticleSystem confettiEffect;

    [Header("UI Reference")]
    public Text taskText;

    private bool triggered = false;

    public void TriggerReward()
    {
        if (triggered) return;
        triggered = true;

        // Stop any existing confetti effect (reset it)
        if (confettiEffect != null)
            confettiEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // Play success sound if available
        if (audioSource != null && successSound != null)
            audioSource.PlayOneShot(successSound);

        // Play confetti or sparkle effect if assigned
        if (confettiEffect != null)
            confettiEffect.Play();

        // Start text flashing animation
        StartCoroutine(FlashText());
    }

    private System.Collections.IEnumerator FlashText()
    {
        if (taskText == null) yield break;

        float duration = 3f;
        float elapsed = 0f;
        Color baseColor = taskText.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            taskText.color = Color.Lerp(Color.yellow, Color.green, Mathf.PingPong(Time.time * 2f, 1));
            yield return null;
        }

        taskText.color = baseColor;
    }
}
