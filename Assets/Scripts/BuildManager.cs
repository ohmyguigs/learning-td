using UnityEngine;

public class BuildManager : MonoBehaviour {
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    public GameObject turretToBuild;
    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }
    void Awake() {
        if (instance != null) {
            return;
        }
        instance = this;
    }

    void Start() {
        turretToBuild = standardTurretPrefab;
    }
}
