using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePolicmen : MonoBehaviour
{
    private int _indexPointPosition;
    private int _startIndexPointPosotion = 0;
    private float _speed;
    [SerializeField] private float _standartSpeed;
    [SerializeField] private float _fastSpeed;
    [SerializeField] private float _rotateSpeed;
    public Transform[] pointForMove;
    private Animator _anim;

    private Rigidbody _rb;

    void Start()
    {
        _speed = _standartSpeed;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_indexPointPosition < pointForMove.Length)
        {
            Transform currenNowIndexPoint = pointForMove[_indexPointPosition];
            Vector3 direction = currenNowIndexPoint.position - new Vector3(transform.position.x, transform.position.y,
                transform.position.z);
            _rb.MovePosition(transform.position + direction.normalized * _speed * Time.deltaTime);
            Vector3 targetDirection = pointForMove[_indexPointPosition].position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                _speed * Time.deltaTime * _rotateSpeed);
            if (Vector3.Distance(transform.position, currenNowIndexPoint.position) <= 0.1f)
            {
                _indexPointPosition++;
                if (_indexPointPosition == pointForMove.Length)
                {
                    _indexPointPosition = _startIndexPointPosotion;
                }
            }
        }

        if (_indexPointPosition == 1 || _indexPointPosition == 3)
        {
            _speed = +_fastSpeed;
            _anim.SetFloat("speed", 1f);
        }
        else if (_indexPointPosition == 0 || _indexPointPosition == 2)
        {
            _speed = _standartSpeed;
            _anim.SetFloat("speed", 0f);
        }

    }
}
