using UnityEngine;

public class NodeManager : MonoBehaviour {
    public Color hoverColor;
    public Color cantBuildColor;
    public Vector3 positionOffset;

    private GameObject turret;
    private Color startColor;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown() {
        if (turret != null) {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            rend.material.color = cantBuildColor;
            return;
        }

        // build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if (turret != null) {
            rend.material.color = cantBuildColor;
            return;
        } else {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
