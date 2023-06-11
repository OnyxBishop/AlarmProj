using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _enterTrigger;
    [SerializeField] private UnityEvent _exitTrigger;

    public event UnityAction EnterTrigger
    {
        add => _enterTrigger.AddListener(value);
        remove => _enterTrigger.RemoveListener(value);
    }

    public event UnityAction ExitTrigger
    {
        add => _exitTrigger.AddListener(value);
        remove => _exitTrigger.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _enterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _exitTrigger?.Invoke();
        }
    }
}
