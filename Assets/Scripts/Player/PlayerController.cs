using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]public int State = Animator.StringToHash(nameof(State));

    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public States States
    {
        set { _animator.SetInteger(State, (int)value); } 
    }

    private void Awake()
    {       
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        States = States.idle;

        if (Input.GetButton("Horizontal") == true)
            Walk();
    }

    private void Walk()
    {
        States = States.walk;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);

        _spriteRenderer.flipX = direction.x < 0.0f;
    }
}

public enum States
{
    idle,
    walk
}