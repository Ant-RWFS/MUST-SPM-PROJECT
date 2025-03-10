using System.Collections;
using UnityEngine;

public class MotorLightController : MonoBehaviour
{
    [SerializeField] private Motor motor;
    [SerializeField] private Light headLight;
    [SerializeField] private Light bodyLight;

    [SerializeField] private Vector3[] LightRotationAngles;

    private int[] indexes = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
    private int index;
    private float xInput;
    private float yInput;
    private bool lightOn;
    private bool isLightBusy;

    private void Awake()
    {
        index = 0;
        xInput = 0;
        yInput = 0;
        lightOn = false;
        isLightBusy = false;
    }

    private void Start()
    {
    }

    private void Update()
    {
        SwitchLight();
    }

    private void SwitchLight()
    {
        xInput = motor.anim.GetFloat("xInput");
        yInput = motor.anim.GetFloat("yInput");

        ManualLightSwitch();
        LightSwitchLogic();
    }

    private void ManualLightSwitch()
    {
        if (!motor.anim.GetBool("Off") && Input.GetKeyDown(KeyCode.F))
        {
            if (lightOn)
                lightOn = false;
            else
                lightOn = true;
        }
    }

    private void LightSwitchLogic()
    {
        SwitchLightPerspective();

        if (!motor.anim.GetBool("Off") && lightOn)
        {
            LightsOn();

            if ((xInput == 0 && yInput == 0) || (xInput == 1 && yInput == 0))
                index = indexes[0];//E 0 90

            else if (xInput == 1 && yInput == 1)
                index = indexes[1];//NE -45 90

            else if (xInput == 0 && yInput == 1)
                index = indexes[2];//N  -90 90

            else if (xInput == -1 && yInput == 1)
                index = indexes[3];//NW -45 -90

            else if (xInput == -1 && yInput == 0)
                index = indexes[4];//W 0 -90

            else if (xInput == -1 && yInput == -1)
                index = indexes[5];//SW 45 -90

            else if (xInput == 0 && yInput == -1)
                index = indexes[6];//S 90 90

            else if (xInput == 1 && yInput == -1)
                index = indexes[7];//SE 45 90

            SwitchLightDirection(index);
        }
        else
        {
            LightsOff();
            lightOn = false;
        }
    }

    private void SwitchLightPerspective()
    {
        if (!isLightBusy)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                StartCoroutine(LightTraceCameraQ());

            else if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine(LightTraceCameraE());
        }
    }

    private IEnumerator LightTraceCameraQ()
    {
        isLightBusy = true;

        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] += 7;
            indexes[i] %= 8;

            yield return new WaitForFixedUpdate();
        }

        isLightBusy = false;
    }

    private IEnumerator LightTraceCameraE()
    {
        isLightBusy = true;

        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] += 1;
            indexes[i] %= 8;

            yield return new WaitForFixedUpdate();
        }

        isLightBusy = false;
    }

    private void LightsOn() 
    {
        headLight.enabled = true;
        bodyLight.enabled = true;
    }

    private void LightsOff() 
    {
       headLight.enabled = false;
       bodyLight.enabled = false;
    }
    private void SwitchLightDirection(int _index) => headLight.transform.rotation = Quaternion.Euler(LightRotationAngles[_index].x, LightRotationAngles[_index].y, LightRotationAngles[_index].z);
}
