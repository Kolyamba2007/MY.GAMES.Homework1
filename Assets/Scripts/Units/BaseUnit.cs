using System;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class BaseUnit : MonoBehaviour
{
    [Header("Время жизни персонажа")]
    [SerializeField, Range(5, 15)] protected float _liveTime;
    [Header("Максимально возможные очки за попадание")]
    [SerializeField, Range(5, 20)] protected int _possibleScorePoints;
    [Header("Радиус взрыва")]
    [SerializeField, Range(2, 10)] private float _explosionRadius;
    [Header("Сила взрыва")]
    [SerializeField, Range(100, 300)] private float _explosionForce;

    private ScaleConstraint _scaleConstraint;
    protected float _time = 0;

    [HideInInspector] public bool isInteracted = false;

    public event Action<BaseUnit, int> Exploded;
    public event Action<BaseUnit, int> Interacted;

    private void Start()
    {
        _scaleConstraint = GetComponent<ScaleConstraint>();
        Destroy(transform.gameObject, _liveTime);
    }

    private void Update()
    {
        _scaleConstraint.weight = Mathf.Lerp(1, 0, _time / _liveTime);
        _time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if(!isInteracted)
            Explode();
    }

    private void Explode()
    {
        Exploded?.Invoke(this, -5);

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

    protected void InteractionHandler(int score)
    {
        Interacted?.Invoke(this, score);

        isInteracted = true;
        Destroy(transform.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
