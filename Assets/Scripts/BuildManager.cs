using UnityEngine;

public class BuildManager : MonoBehaviour {
    public static BuildManager instance;
    public GameObject cannonTurretPrefab;
    public GameObject mortarTurretPrefab;
    public GameObject laserTurretPrefab;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }

    public void SetTuretToBuild(GameObject turret) {
        turretToBuild = turret;
    }

    void Awake() {
        if (instance != null) {
            return;
        }
        instance = this;
    }
}
