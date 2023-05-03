using UnityEngine;

public class RTSCameraControll : MonoBehaviour {
  [SerializeField] public float speed = 1f;
  [SerializeField] public float smoothing = 5f;
  [SerializeField] public Vector2 range = new (100, 100);

    private Vector3 _targetPosition;
    private Vector3 _input;

    private void Awake() {
        _targetPosition = transform.position;
    }

    private void HandleInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 right = transform.right * x;
        Vector3 forward = transform.forward * z;

        _input = (right + forward).normalized;
    }

    private void Move() {
        Vector3 nextTargetPosition = _targetPosition + _input * speed;
        if (IsInBounds(nextTargetPosition)) {
            _targetPosition = nextTargetPosition;
        }
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * smoothing);
    }

    private bool IsInBounds(Vector3 position) {
        return position.x > -range.x && position.x < range.x && position.z > -range.y && position.z < range.y;
    }

    private void Update() {
        HandleInput();
        Move();
  }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5f);
        Gizmos.DrawCube(Vector3.zero, new Vector3(range.x * 2, 5f, range.y * 2f));

  }
}
