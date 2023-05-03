using UnityEngine;
using Cinemachine;

public class CameraControll : MonoBehaviour {
    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float zoomSpeed = 15f;
    [SerializeField]
    private float zoomInMax = 40f;
    [SerializeField]
    private float zoomOutMax = 90f;
    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera vcam;
    private Transform cameraTransform;

    private void Awake() {
        inputProvider = GetComponent<CinemachineInputProvider>();
        vcam = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = vcam.VirtualCameraGameObject.transform;
    }

    void Start() {
        
    }

    void Update() {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);

        if (x != 0 || y != 0) {
            PanScreen(x, y);
        }
    }

    public Vector2 PanDirection(float x, float y) {
        Vector2 direction = Vector2.zero;
        if (y >= Screen.height * .95f) {
            direction.y += 1;
        } else if (y <= Screen.height * .05f) {
            direction.y -= 1;
        }

        if (x >= Screen.width * .95f) {
            direction.x += 1;
        } else if (x <= Screen.width * .05f) {
            direction.x -= 1;
        }
        return direction;
    }

    public void PanScreen(float x, float y) {
        Vector2 direction = PanDirection(x, y);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.position + (Vector3)direction, Time.deltaTime * panSpeed);
    }

    public void ZoomScreen(float increment) {
        float fov = vcam.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + increment, zoomInMax, zoomOutMax);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(fov, target, Time.deltaTime * zoomSpeed);
    }
}
