using UnityEngine;

public class EN_SoundsManager : EN_Singleton<EN_SoundsManager>
{
    [SerializeField] Transform listenerPosition = null;
    public bool IsValid => listenerPosition;

    public void PlaySound(AudioClip _clip)
    {
        if (!IsValid) return;
        AudioSource.PlayClipAtPoint(_clip, listenerPosition.position);
    }
}
