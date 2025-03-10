using UnityEngine;

public class MapMask : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        transform.position = new Vector3(PlayerManager.instance.playerTransform.position.x, PlayerManager.instance.playerTransform.position.y, 0); 
    }
}
