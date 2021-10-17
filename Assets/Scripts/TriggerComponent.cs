using UnityEngine;
using UnityEditor;

public class TriggerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        EditorApplication.isPlaying = false;
    }
}
