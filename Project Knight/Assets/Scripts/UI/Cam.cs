using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // ∆елаема€ ширина камеры в пиксел€х.
    public float desiredWidth = 1760;

    //  оличество пикселей на единицу мирового пространства.
    public float pixelsPerUnit = 160;

    

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        // ¬ычисл€ем и устанавливаем размер ортографической камеры.
        Camera camera = GetComponent<Camera>();
        float orthographicSize = desiredWidth / pixelsPerUnit - 0.2f;
        camera.orthographicSize = orthographicSize;
    }
    private void Update()
    {
        Camera camera = GetComponent<Camera>();
        float orthographicSize = (desiredWidth) / pixelsPerUnit - 0.2f;
        camera.orthographicSize = orthographicSize;
    }

    //public Vector2 DefaultResolution = new Vector2(720, 1280);
    //[Range(0f, 4f)] public float WidthOrHeight = 0;

    //private Camera componentCamera;

    //private float initialSize;
    //private float targetAspect;

    //private float initialFov;
    //private float horizontalFov = 120f;

    //private void Start()
    //{
    //    componentCamera = GetComponent<Camera>();
    //    initialSize = componentCamera.orthographicSize;

    //    targetAspect = DefaultResolution.x / DefaultResolution.y;

    //    initialFov = componentCamera.fieldOfView;
    //    horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);

    //    Application.targetFrameRate = 60;
    //    QualitySettings.vSyncCount = 0;
    //}

    //private void Update()
    //{
    //    if (componentCamera.orthographic)
    //    {
    //        float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
    //        componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
    //    }
    //    else
    //    {
    //        float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
    //        componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
    //    }
    //}

    //private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    //{
    //    float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

    //    float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

    //    return vFovInRads * Mathf.Rad2Deg;
    //}
}
