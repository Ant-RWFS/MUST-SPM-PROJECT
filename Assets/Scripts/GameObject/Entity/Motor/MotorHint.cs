using UnityEngine;

public class MotorHint : MonoBehaviour
{
    public GameObject hint;
    private Motor motor;

    private void Awake()
    {
        motor = GetComponentInParent<Motor>();
    }

    private void Update()
    {
        AdjustHint();
    }

    private void AdjustHint() 
    {
        if (!motor.anim.GetBool("Off") && hint.activeSelf)
            hint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && motor.anim.GetBool("Off"))
        {
            if (hint.activeSelf == false)
                hint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && motor.anim.GetBool("Off"))
        {
            if (hint.activeSelf == true)
                hint.SetActive(false);
        }
    }
}
