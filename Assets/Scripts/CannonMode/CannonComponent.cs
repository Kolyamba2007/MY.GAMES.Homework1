using System;
using System.Collections;
using UnityEngine;

public class CannonComponent : MonoBehaviour
{
    [SerializeField] private TextMesh _timerText;

    [Header("Точка, откуда вылетает снаряд")]
    [SerializeField] private Transform _firePoint;
    [Header("Минимальный угол поворота пушки")]
    [SerializeField] private int _minAngle;
    [Header("Максимальный угол поворота пушки")]
    [SerializeField] private int _maxAngle;
    [Header("Сила выстрела")]
    [SerializeField, Range(150, 300)] private float _explosionForce;

    public event Action<Vector2, Vector2> Fire;

    private IEnumerator FireTimer()
    {
        int timer = UnityEngine.Random.Range(3, 7);
        _timerText.text = timer.ToString();

        do
        {
            yield return new WaitForSeconds(1);
            timer--;
            _timerText.text = timer.ToString();
        }
        while (timer > 0);

        Fire?.Invoke(_firePoint.transform.position, (_firePoint.transform.position - transform.position) * _explosionForce);
        StartCoroutine(RotateCannon(UnityEngine.Random.Range(_minAngle, _maxAngle)));

        yield break;
    }

    private IEnumerator RotateCannon(float angle)
    {
        float t = 0;
        Quaternion startRotation = transform.rotation;

        do
        {
            t = Mathf.Clamp01(t + Time.deltaTime / 2);
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0, 0, angle), t);

            yield return null;
        }
        while (t < 1);

        StartCoroutine(FireTimer());

        yield break;
    }
}
