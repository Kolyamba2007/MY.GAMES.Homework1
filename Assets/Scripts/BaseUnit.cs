using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BaseUnit : MonoBehaviour
{
    [Header("Время жизни персонажа")]
    [SerializeField, Range(5, 15)]
    private float liveTime;
    [Header("Радиус взрыва")]
    [SerializeField, Range(2, 10)]
    private float _explosionRadius;
    [Header("Сила взрыва")]
    [SerializeField, Range(100, 300)]
    private float _explosionForce;
    private ScaleConstraint _scaleConstraint;
    private float time = 0;

    void Start()
    {
        _scaleConstraint = GetComponent<ScaleConstraint>();
        Destroy(transform.gameObject, liveTime);
    }

    void Update()
    {
        _scaleConstraint.weight = Mathf.Lerp(1, 0, time/liveTime);
        time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        Explode();
    }

    private void Explode()
    {
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
    
        foreach(Collider2D obj in overlappedColliders)
        {
            Rigidbody2D rigidbody = obj.attachedRigidbody;

            if (rigidbody)
            {
                Vector2 direction = obj.transform.position - transform.position;

                rigidbody.AddForce(direction * _explosionForce);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
