using UnityEngine;
using UnityEngine.UI;

public class RewardEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip successSound;
    public Text taskText;
    private bool triggered = false;

    public void TriggerReward()
    {
        if (triggered) return;
        triggered = true;

        Debug.Log("✅ RewardEffect triggered!");

        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound);
            Debug.Log("🎵 Playing success sound: " + successSound.name);
        }
        else
        {
            if (audioSource == null)
                Debug.LogWarning("⚠️ RewardEffect: Missing AudioSource reference!");
            if (successSound == null)
                Debug.LogWarning("⚠️ RewardEffect: Missing Success Sound reference!");
        }

        // Start the flash animation
        StartCoroutine(FlashText());
    }

    private System.Collections.IEnumerator FlashText()
    {
        if (taskText == null)
        {
            Debug.LogWarning("⚠️ RewardEffect: No TaskText assigned for flashing effect!");
            yield break;
        }

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
