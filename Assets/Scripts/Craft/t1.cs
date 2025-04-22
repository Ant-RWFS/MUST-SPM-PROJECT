using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseWorldPosition3D : MonoBehaviour
{
    public GameObject bs;
    public GameObject player;
    public Camera miniMapCamera;

    Ray ray;
    void Update()
    {
        Vector3? worldPosition = GetMouseWorldPosition();
        if (worldPosition.HasValue)
            bs.transform.position = worldPosition.Value;
        else
            Debug.Log("未获取到世界位置");
        
    }
    Vector3? GetMouseWorldPosition()
    {
        Vector3 worldPosition = miniMapCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, miniMapCamera.nearClipPlane));

        Vector3 rayOrigin = new Vector3(worldPosition.x + PlayerManager.instance.playerTransform.position.x * 10, worldPosition.y + PlayerManager.instance.playerTransform.position.y * 10, 0);

        ray = new Ray(rayOrigin, Vector3.forward);

        Vector3 intersectionPoint = ray.origin;

        return new Vector3(intersectionPoint.x / 12, intersectionPoint.y / 12, 0f);
    }
}