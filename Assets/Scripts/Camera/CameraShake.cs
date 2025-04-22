using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    protected float shakeTime = 0.2f;
    protected bool isShakeing = false;
    protected bool shakeUp = false;
    public Vector3 shakePosition;

    Vector3 moveVector = new Vector3(0.25f, 0.5f, 0.5f);
    float xAngle = 20f;
    float yAngle = 20f;
    float zAngle = 0f;
    
    public static CameraShake Instance { get; private set; }
    private void Awake()
    {
      
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void CameraShakeEffect()
    {
        

        if (!isShakeing)
        {
            StartCoroutine(Shake(transform,shakeTime));
        }


    }
    IEnumerator Shake(Transform transform, float _shakeTime)
    {
        float number = 60* _shakeTime;
        Vector3 nextMove = moveVector / number;
        Vector3 originRotate;
        originRotate=transform.rotation.eulerAngles ;
        float nextXRotate =xAngle /number;
        float nextYRotate =yAngle /number;
        float nextZRotate =zAngle /number;
        isShakeing =true;
        for (int i = 0; i < number; i++)
        {
            if (shakeUp)
            {
                shakePosition = nextMove;
           
                transform.Rotate(new Vector3(nextXRotate, nextYRotate, nextZRotate));
                
            } 
            else {
                shakePosition = -nextMove;
            
                transform.Rotate(new Vector3(-nextXRotate, -nextYRotate, -nextZRotate));
                
            }
            shakeUp = !shakeUp;
            

            yield return new WaitForFixedUpdate();
        }
        float currentZ =transform.eulerAngles.z;
        transform.eulerAngles=new Vector3(originRotate.x,originRotate.y,currentZ);
        isShakeing =false;
    }

}
