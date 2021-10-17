using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Точка появления персонажей")]
    [SerializeField]
    private Transform SpawnPoint;
    [Header("Персонаж")]
    [SerializeField]
    private GameObject Egg, Sausage, Tomato;
    [Header("Время между появлением персонажей")]
    [SerializeField, Range(1, 5)]
    private float time;

    void Start()
    {
        StartCoroutine(Spawn(time));
    }

    void Update()
    {
        
    }

    private IEnumerator Spawn(float time)
    {
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    Instantiate(Egg, SpawnPoint.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Tomato, SpawnPoint.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Sausage, SpawnPoint.position, Quaternion.identity);
                    break;
            }
            yield return new WaitForSeconds(time);
        }
    }

    //event EndGame
}
