using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //========================================

    [SerializeField] private AudioSource audioSource;

    public bool isNowPlay = false;

    private void Awake()
    {
        #region [ΩÃ±€≈Ê]
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        TryGetComponent(out audioSource);
    }

    public void PlayMusic()
    {
        audioSource.Play();
        isNowPlay = true;
    }
}
