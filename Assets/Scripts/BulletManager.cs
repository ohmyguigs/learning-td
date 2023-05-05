using UnityEngine;

public class BulletManager : MonoBehaviour {
  public float speed = 70f;
  public int damage = 50;
  public float explosionRadius = 0f;
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
    transform.LookAt(target);
  }

  void HitTarget() {
    GameObject fxInstance = (GameObject)Instantiate(impactFX, transform.position, transform.rotation);
    Destroy(fxInstance, 1f);
    if (explosionRadius > 0f) {
      Explode();
    } else {
      Damage(target);
    }
    Destroy(gameObject);
  }

  void Explode() {
    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    foreach (Collider collider in colliders) {
      if (collider.tag == "Enemy") {
        Damage(collider.transform);
      }
    }
  }

  void Damage(Transform enemy) {
    // temporary insta-destroy
    Destroy(enemy.gameObject);

    /* EnemyManager e = enemy.GetComponent<EnemyManager>();
    if (e != null) {
      e.TakeDamage(damage);
    } */
  }

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, explosionRadius);
  }
}
