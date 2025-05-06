using UnityEngine;

public class InteractiveMapItem : MonoBehaviour
{
    public GameObject droppedItemGO;
    public GameObject hint;
    private MapItem mapItem;
    private CircleCollider2D coll;

    public float activeTime = 30f;
    private float activeTimer;

    private void Awake()
    {
        mapItem = GetComponentInChildren<MapItem>();
        coll = GetComponent<CircleCollider2D>();
        activeTimer = 0;
    }

    private void Update()
    {
        ItemInteraction();
        ReactivateItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mapItem.gameObject.activeSelf)
        {
            if (collision.GetComponent<Motor>())
            {
                mapItem.gameObject.SetActive(false);
                DropItem();
            }

            if (collision.GetComponent<Player>()) 
            {
                if(!hint.activeSelf)
                    hint.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (hint.activeSelf)
                hint.SetActive(false);
        }
    }

    private void ItemInteraction() 
    {
        if (mapItem.gameObject.activeSelf)
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, coll.radius);

            foreach (var hit in collider)
            {
                if (hit.GetComponent<Player>() && Input.GetKeyDown(KeyCode.F))
                {
                    mapItem.gameObject.SetActive(false);
                    DropItem();
                }
            }
        }

        if (!mapItem.gameObject.activeSelf && hint.activeSelf)
            hint.SetActive(false);
    }

    private void DropItem()
    {
        GameObject droppedItem = Instantiate(droppedItemGO, transform.position, Quaternion.identity, ItemManager.instance.itemTransform);
    }

    private void ReactivateItem() 
    {
        if (!mapItem.gameObject.activeSelf)
            activeTimer += Time.deltaTime;

        if (activeTimer >= activeTime)
        {
            activeTimer = 0;
            mapItem.gameObject.SetActive(true);
        }
    }
}
