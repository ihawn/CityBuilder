using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PanAndZoom : MonoBehaviour
{
    [SerializeField]
    private float panSpeed = 2;
    [SerializeField]
    private float zoomSpeed = 3;
    [SerializeField]
    private float zoomInMax = 40;
    [SerializeField]
    private float zoomOutMax = 90;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;

    public void Init()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
    }

    public void UpdateCameraTransform()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = -inputProvider.GetAxisValue(2);
        if(x != 0 || y != 0)
            PanScreen(x, y);
        if (z != 0)
            ZoomScreen(z);
    }

    public void ZoomScreen(float increment)
    {
        float fov = virtualCamera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + increment, zoomInMax, zoomOutMax);
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpeed*Time.deltaTime);
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;
        if(y >= Screen.height * 0.75f)
            direction.y += (y - Screen.height * 0.75f)/Screen.height;
        else if(y <= Screen.height * 0.25f)
            direction.y -= (Screen.height * 0.25f - y)/Screen.height;

        if (x >= Screen.width * 0.75f)
            direction.x += (x - Screen.width * 0.75f)/Screen.width;
        else if(x <= Screen.width * 0.25f)
            direction.x -= (Screen.width * 0.25f - x)/Screen.width;

        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.position + Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0) * (new Vector3(direction.x, 0, direction.y)), Time.deltaTime*panSpeed) ;
    }
}
