using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class BaseUnit : MonoBehaviour, IPointerClickHandler
{
    [Header("Время жизни персонажа")]
    [SerializeField, Range(5, 15)]
    private float _liveTime;
    [Header("Максимально возможные очки за попадание")]
    [SerializeField, Range(5, 20)]
    private int _possibleScorePoints;
    [Header("Радиус взрыва")]
    [SerializeField, Range(2, 10)]
    private float _explosionRadius;
    [Header("Сила взрыва")]
    [SerializeField, Range(100, 300)]
    private float _explosionForce;
    private ScaleConstraint _scaleConstraint;
    private float time = 0;
    private bool isClicked = false;

    void Start()
    {
        _scaleConstraint = GetComponent<ScaleConstraint>();
        Destroy(transform.gameObject, _liveTime);
    }

    void Update()
    {
        _scaleConstraint.weight = Mathf.Lerp(1, 0, time/ _liveTime);
        time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if(!isClicked)
            Explode();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.PlaySound(AudioManager.ClickClip);
        GameManager.ChangingScore((int)(time/_liveTime * _possibleScorePoints));
        isClicked = true;
        Destroy(transform.gameObject);
    }

    private void Explode()
    {
        AudioManager.PlaySound(AudioManager.ExplosionClip);
        EffectsManager.Explosion(transform);
        GameManager.ChangingScore(-5);

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
