using UnityEngine;

public class Shop : MonoBehaviour {
    BuildManager buildManager;

    void Start() {
        buildManager = BuildManager.instance;
    }

    public void PurchaseMortar() {
        Debug.Log("Mortar selected!");
        buildManager.SetTuretToBuild(buildManager.mortarTurretPrefab);
    }

    public void PurchaseCannon() {
        Debug.Log("Cannon selected!");
        buildManager.SetTuretToBuild(buildManager.cannonTurretPrefab);
    }

    public void PurchaseLaser() {
        Debug.Log("Laser selected!");
        buildManager.SetTuretToBuild(buildManager.laserTurretPrefab);
    }
}
