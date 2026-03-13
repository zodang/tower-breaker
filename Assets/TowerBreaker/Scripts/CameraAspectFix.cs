using UnityEngine;

public class CameraAspectFix : MonoBehaviour
{
    public float ReferenceWidth = 1080f;
    public float ReferenceHeight = 2400f;
    public float ReferenceOrthographicSize = 5f;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

        float referenceAspect = ReferenceWidth / ReferenceHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        float ratio = referenceAspect / currentAspect;

        _camera.orthographicSize = ReferenceOrthographicSize * ratio;
    }
}
