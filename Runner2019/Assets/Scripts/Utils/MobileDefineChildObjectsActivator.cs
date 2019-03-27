using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MobileDefineChildObjectsActivator : MonoBehaviour
{
#if !UNITY_EDITOR
    private void OnEnable()
    {
        ControlChildObjectsActiveState();
    }
#endif

    private void Start()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
#endif
        {
            CreateEventSystemObjectIfAbsent();
        }
    }

    private void CreateEventSystemObjectIfAbsent()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystemGO = new GameObject("EventSystem");

            eventSystemGO.AddComponent<EventSystem>();
            eventSystemGO.AddComponent<StandaloneInputModule>();
        }        
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        EditorUserBuildSettings.activeBuildTargetChanged += ControlChildObjectsActiveState;
        EditorApplication.update += ControlChildObjectsActiveState;
    }

    private void OnDisable()
    {
        EditorUserBuildSettings.activeBuildTargetChanged -= ControlChildObjectsActiveState;
        EditorApplication.update -= ControlChildObjectsActiveState;
    }

#endif
    
    private void ControlChildObjectsActiveState()
    {
#if MOBILE_INPUT
        EnableChildObjects();
#else
        DisableChildObjects();
#endif
    }

    private void EnableChildObjects()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    private void DisableChildObjects()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

}
