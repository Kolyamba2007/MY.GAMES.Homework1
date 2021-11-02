using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class BaseUnit : MonoBehaviour, IPointerClickHandler
{
    [Header("Время жизни персонажа")]
    [SerializeField, Range(5, 15)] private float _liveTime;
    [Header("Максимально возможные очки за попадание")]
    [SerializeField, Range(5, 20)] private int _possibleScorePoints;
    [Header("Радиус взрыва")]
    [SerializeField, Range(2, 10)] private float _explosionRadius;
    [Header("Сила взрыва")]
    [SerializeField, Range(100, 300)] private float _explosionForce;

    private ScaleConstraint _scaleConstraint;
    private float _time = 0;

    [HideInInspector] public bool isClicked = false;

    public event Action<BaseUnit, int> Exploded;
    public event Action<BaseUnit, int> Clicked;

    void Start()
    {
        _scaleConstraint = GetComponent<ScaleConstraint>();
        Destroy(transform.gameObject, _liveTime);
    }

    void Update()
    {
        _scaleConstraint.weight = Mathf.Lerp(1, 0, _time / _liveTime);
        _time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if(!isClicked)
            Explode();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this, (int)(_time / _liveTime * _possibleScorePoints));

        isClicked = true;
        Destroy(transform.gameObject);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
