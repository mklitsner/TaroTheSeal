using UnityEngine;

public class AudioLooper : MonoBehaviour
{
    public AudioClip loopClip1;
    public AudioClip loopClip2;
    public bool isStart;
    public AudioSource countDownAudio;

    private AudioSource _audioSource;
    private bool _isChangingClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartLooping(loopClip1);
    }

    private void Update()
    {
        if (isStart && !_isChangingClip)
        {
            _isChangingClip = true;
            StartCoroutine(ChangeLoop());
        }
    }

    private void StartLooping(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    private System.Collections.IEnumerator ChangeLoop()
    {
        float remainingTime = _audioSource.clip.length - _audioSource.time;
        yield return new WaitForSeconds(remainingTime);
        StartLooping(loopClip2);
        _isChangingClip = false;
    }
}