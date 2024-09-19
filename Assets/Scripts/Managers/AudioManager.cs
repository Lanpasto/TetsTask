using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("References")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private string[] musicClips;
    private int currentMusicIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(currentMusicIndex);
    }

    public void PlaySFX(string clipName, bool loop = false)
    {
        if (sfxSource == null)
        {
            Debug.LogError("sfxSource is not assigned.");
            return;
        }

        if (string.IsNullOrEmpty(clipName)) return;

        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + clipName);
        if (clip != null)
        {
            sfxSource.clip = clip;
            sfxSource.loop = loop;
            sfxSource.Play();
        }
        else
        {
            Debug.LogWarning($"Clip '{clipName}' not found in Resources/Sounds.");
        }
    }

    public void StopSFX() => sfxSource?.Stop();

    public void PlayMusic(int index)
    {
        if (index < 0 || index >= musicClips.Length) return;

        AudioClip clip = Resources.Load<AudioClip>("Music/" + musicClips[index]);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Clip '{musicClips[index]}' not found in Resources/Music.");
        }
    }

    public void PlayNextMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicClips.Length;
        PlayMusic(currentMusicIndex);
    }

    public void StopMusic() => musicSource.Stop();
}
