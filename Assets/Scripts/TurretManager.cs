using UnityEngine;

public class TurretManager : MonoBehaviour {
  [Header("Attributes")]
  public float range = 15f;
  public float turnSpeed = 10f;
  public float fireRate = 1f;

  [Header("Unity Setup Fields")]
  public string enemyTag = "Enemy";
  public Transform partToRotate;
  public GameObject bulletPrefab;
  public Transform shootPoint;
  public ParticleSystem fireFX;
  private Transform target;
  private float fireCountdown = 0f;

  void Start() {
    float delay = 0f;
    float repeatRate = 0.5f;
    InvokeRepeating("UpdateTarget", delay, repeatRate);
  }

  void Update() {
    if (target == null || partToRotate == null) {
      return;
    }

    // Target lock-on rotations
    Vector3 direction = target.position - transform.position;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    // shooting mechanics
    if (fireCountdown <= 0f) {
      Shoot();
      fireCountdown = 1f / fireRate;
    }
    fireCountdown -= Time.deltaTime;
  }

  void Shoot() {
    // spawn bullet
    GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    BulletManager bullet = bulletGO.GetComponent<BulletManager>();

    // set bullet motion
    if (bullet != null) {
      bullet.Seek(target);

      GameObject fireFXInstance = (GameObject)Instantiate(fireFX.gameObject, shootPoint.position, shootPoint.rotation);
      Destroy(fireFXInstance, 1f);
    }
  }

  void UpdateTarget() {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    float shortestDistance = Mathf.Infinity;
    GameObject nearestEnemy = null;

    foreach (GameObject enemy in enemies) {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
      if (distanceToEnemy < shortestDistance)
      {
        shortestDistance = distanceToEnemy;
        nearestEnemy = enemy;
      }
    }

    if (nearestEnemy != null && shortestDistance <= range) {
      target = nearestEnemy.transform;
    } else {
      target = null;
    }
  }

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
