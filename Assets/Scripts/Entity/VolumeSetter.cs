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

    private float _maxVolume = 0.5f;
    private float _zeroVolume = 0f;
    private float _fadeTime = 10f;

    private void OnEnable()
    {
        _alarmTrigger = GetComponent<AlarmTrigger>();
        _alarmTrigger.EnterTrigger += OnTriggerEnterChangeVolume;
        _alarmTrigger.ExitTrigger -= OnTriggerExitChangeVolume;
    }

    private void OnDisable()
    {
        _alarmTrigger.EnterTrigger -= OnTriggerEnterChangeVolume;
        _alarmTrigger.ExitTrigger += OnTriggerExitChangeVolume;
    }

    public void OnTriggerEnterChangeVolume()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _audioSource.Play();

        _fadeCoroutine = StartCoroutine(Fade(true));

    }

    public void OnTriggerExitChangeVolume()
    {
        if(_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool isFade)
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
