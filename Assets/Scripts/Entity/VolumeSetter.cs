using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AlarmTrigger))]
public class VolumeSetter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _fadeCoroutine;

    private AlarmTrigger _alarmTrigger;

    private bool _isPlaying = false;

    private float _maxVolume = 0.5f;
    private float _zeroVolume = 0f;
    private float _fadeTime = 10f;

    private void Awake()
    {
        _alarmTrigger = GetComponent<AlarmTrigger>();
    }

    private void OnEnable()
    {
        _alarmTrigger.Reached += OnChangeVolume;
    }

    private void OnDisable()
    {
        _alarmTrigger.Reached -= OnChangeVolume;
    }

    private void OnChangeVolume()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(Fade(_alarmTrigger.IsStartPlaying));
    }   

    private IEnumerator Fade(bool isFade)
    {
        float elapsedTime = 0f;
        float currentVolume = _audioSource.volume;
        float targetVolume = isFade == true ? _maxVolume : _zeroVolume;

        if (_isPlaying == false)
        {
            _audioSource.Play();
            _isPlaying = true;
        }

        while (elapsedTime < _fadeTime)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, targetVolume, (elapsedTime) / _fadeTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (_isPlaying == true)
        {
            _audioSource.Stop();
            _isPlaying = false;
        }

        _audioSource.volume = targetVolume;
    }
}
