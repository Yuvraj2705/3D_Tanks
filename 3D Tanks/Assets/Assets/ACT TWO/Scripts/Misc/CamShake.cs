using System.Collections;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 orgPos = transform.localPosition;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-0.01f, 0.01f) * magnitude;
            float y = Random.Range(-0.01f, 0.01f) * magnitude;

            transform.localPosition = new Vector3(x, y, orgPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = orgPos;
    }
}
