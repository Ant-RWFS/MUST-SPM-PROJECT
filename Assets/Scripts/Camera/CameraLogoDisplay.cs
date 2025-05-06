using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CameraLogoDisplay : MonoBehaviour
{
    public float rotationTime = 10f;
    public float coloringSpeed = .05f;

    public float audioVolumnChangingSpeed = .05f;

    public float displayTime = 10f;
    private float displayTimer;

    private bool isRotating = false;
    private bool logoDisplaying = false;
    private bool destroyTrigger = false;
    private AudioSource introBGM;
    private Image logo;
    private void Awake()
    {
        introBGM = GetComponent<AudioSource>();
        logo = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        logo.color = Color.clear;
        
        if (CavasManager.instance.UICanvas.activeSelf)
            CavasManager.instance.UICanvas.SetActive(false);

        if(!isRotating)
            StartCoroutine(RotateUpward(-25, rotationTime));
    }

    private void Update()
    {
        AdjustCoordiantion();
        DisplayLogo();
        DestroyLogic();
    }

    private void OnDestroy()
    {
        if (!CavasManager.instance.UICanvas.activeSelf)
            CavasManager.instance.UICanvas.SetActive(true);

        if (PlayerManager.instance.player.stats.isInvisible)
            PlayerManager.instance.player.stats.isInvisible = false; 
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }

    private void DisplayLogo()
    {
        if (logoDisplaying) 
        {
            logo.color = new Color(1, 0, 0, logo.color.a + (Time.deltaTime * coloringSpeed));
            introBGM.volume += Time.deltaTime * audioVolumnChangingSpeed; 
        }

        if (logo.color.a >= 1)
        {
            if (logoDisplaying)
                logoDisplaying = false;
            displayTimer += Time.deltaTime;
        }

        if (displayTimer >= displayTime)
        {
            logo.color = new Color(1, 0, 0, logo.color.a - (Time.deltaTime * coloringSpeed));
            introBGM.volume -= Time.deltaTime * audioVolumnChangingSpeed;

            if (logo.color.a < 0 && !isRotating && logo.enabled)
            {
                StartCoroutine(RotateDownward(25, rotationTime));
            }
        }
        
        if (!PlayerManager.instance.player.stats.isInvisible)
            PlayerManager.instance.player.stats.isInvisible = true;
    }

    private void DestroyLogic() 
    {
        if (destroyTrigger && introBGM.volume <= 0)
            Destroy(gameObject);
    }

    private IEnumerator RotateUpward(float _angle, float _time)
    {
        float number = 60 * _time;
        float nextAngle = _angle / number;

        isRotating = true;

        for (int i = 0; i < number; i++)
        {
            Camera.main.transform.Rotate(new Vector3(nextAngle, 0, 0));

            yield return new WaitForFixedUpdate();
        }
        isRotating = false;
        logoDisplaying = true;
    }

    private IEnumerator RotateDownward(float _angle, float _time)
    {
        float number = 60 * _time;
        float nextAngle = _angle / number;

        isRotating = true;

        for (int i = 0; i < number; i++)
        {
            Camera.main.transform.Rotate(new Vector3(nextAngle, 0, 0));

            yield return new WaitForFixedUpdate();
        }
        isRotating = false;
        logo.enabled = false;

        destroyTrigger = true;
    }

    private void AdjustCoordiantion()
    {
        transform.up = PlayerManager.instance.player.transform.up;
        transform.right = PlayerManager.instance.player.transform.right;
        transform.position = PlayerManager.instance.playerTransform.position;
    }
}
