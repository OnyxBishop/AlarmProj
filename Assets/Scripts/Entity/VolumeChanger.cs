using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeChanger : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _fadeCoroutine;

    private float _maxVolume = 0.5f;
    private float _zeroVolume = 0f;
    private float _fadeTime = 10f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume(bool isFade)
    {
        if (isFade == true)
        {
            _audioSource.Play();
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(Fade(isFade));
    }

    public IEnumerator Fade(bool isFade)
    {
        float elapsedTime = 0f;
        float currentVolume = _audioSource.volume;
        float targetVolume = isFade == true ? _maxVolume : _zeroVolume;

        while (elapsedTime < _fadeTime)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, targetVolume, (elapsedTime) / _fadeTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (isFade == false)
        {
            _audioSource.Stop();
        }

        _audioSource.volume = targetVolume;
    }
}
