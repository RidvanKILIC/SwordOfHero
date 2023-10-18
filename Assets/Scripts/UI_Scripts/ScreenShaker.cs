using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour
{

    [SerializeField] float shakeDuration;
    [SerializeField] float shakeAmount;
    [SerializeField] AnimationCurve cureve;
    Vector3 orignalPosition ;
    [SerializeField] bool callFunction = false;
    private void Start()
    {
        orignalPosition= this.transform.position;
        //InvokeRepeating("shake", 2, 2);
    }
    private void Update()
    {
        if (callFunction)
        {
            callFunction = false;
            //shake();
        }
    }
    public void shake(float _duration, float _shakeAmount)
    {
        shakeAmount = _shakeAmount;
        shakeDuration = _duration;

        StartCoroutine(Shake(shakeDuration, shakeAmount));
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            //float x = Random.Range(-0.02f, 0.02f) * magnitude;
            //float y = Random.Range(-0.02f, 0.02f) * magnitude;

            //transform.position = new Vector3(x, y,0);
            elapsed += Time.deltaTime;
            float strenght = cureve.Evaluate(elapsed / duration);
            transform.position = orignalPosition + (Random.insideUnitSphere*magnitude);
            yield return 0;
        }
        transform.position = orignalPosition;
    }
}