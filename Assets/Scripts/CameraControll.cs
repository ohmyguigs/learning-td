using UnityEngine;

public class CameraController : MonoBehaviour {
  public float panSpeed = 30f;
  public float panBorderThickness = 10f;
  public float scrollSpeed = 5f;
  public float minY = 10f;
  public float maxY = 80f;
  public float minZoom = 1f;
  public float maxZoom = 40f;
  public float zoomDamp = 1f;
  public float zoomStep = 13f;

  private Camera mainCam;

  void Awake() {
    mainCam = GetComponent<Camera>();
  }

  void Update() {

    /*
    if (GameManager.GameIsOver) {
        this.enabled = false;
        return;
    }
        */

    if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
      transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
    }
    if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
      transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
    }
    if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
      transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
    }
    if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
      transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
    }

    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (scroll != 0f) {
      Debug.Log("scroll?? " + scroll + " camera pos: " + Camera.main.fieldOfView);

      float targetView = Camera.main.fieldOfView;
      targetView -= scroll * 1000 * zoomStep;

      targetView = Mathf.Clamp(targetView, minZoom, maxZoom);
      mainCam.fieldOfView = Mathf.SmoothDamp(mainCam.fieldOfView, targetView, ref scrollSpeed, zoomDamp);
    }

}
}
