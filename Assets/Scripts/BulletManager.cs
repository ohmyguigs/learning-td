using UnityEngine;

public class BulletManager : MonoBehaviour {
  public float speed = 70f;
  public GameObject impactFX;
  private Transform target;

  public void Seek(Transform _target) {
    target = _target;
  }

  void Update() {
    if (target == null) {
      Destroy(gameObject);
      return;
    }

    Vector3 direction = target.position - transform.position;
    float distanceThisFrame = speed * Time.deltaTime;

    if (direction.magnitude <= distanceThisFrame) {
      HitTarget();
      return;
    }

    transform.Translate(direction.normalized * distanceThisFrame, Space.World);
  }

  void HitTarget() {
    GameObject fxInstance = (GameObject)Instantiate(impactFX, transform.position, transform.rotation);
    Destroy(fxInstance, 1f);
    Destroy(gameObject);

    // maybe use damage in the future
    Destroy(target.gameObject);
  }
}
