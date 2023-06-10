using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(VolumeChanger))]
public class AlarmTrigger : MonoBehaviour
{
    private VolumeChanger _changer;

    private void Awake()
    {
        _changer = GetComponent<VolumeChanger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _changer.ChangeVolume(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _changer.ChangeVolume(false);
        }
    }
}
