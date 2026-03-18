using System.Collections;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public static CameraEffect Instance { get; private set; }

    [SerializeField] private Camera camera;
    private Coroutine _shakeCoroutine;
    private Vector3 _originalPos = new Vector3(0, 0, -10);

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration = 0.2f, float strength = 0.08f, float frequency = 50f)
    {
        if (_shakeCoroutine != null) StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = StartCoroutine(ShakeRoutine(duration, strength, frequency));
    }

    private IEnumerator ShakeRoutine(float duration, float strength, float frequency)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / duration;
            float damping = 1f - Mathf.SmoothStep(0f, 1f, progress);

            float x = Mathf.Sin(elapsed * frequency) * strength * damping;
            float y = Mathf.Sin(elapsed * frequency * 0.7f) * strength * 0.3f * damping;

            camera.transform.localPosition = _originalPos + new Vector3(x, y, 0f);

            yield return null;
        }

        camera.transform.localPosition = _originalPos;
        _shakeCoroutine = null;
    }
}
