using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHeightWithScrollWheel : MonoBehaviour
{
    public float min = 3f;
    public float max = 10f;
    public float durationOfMove = 1.0f;

    private float offset;
    private float elapsedTime;
    private bool isTransitioning;

    private void OnEnable()
    {
        isTransitioning = false;
        offset = min;
        transform.localPosition = new Vector3(transform.localPosition.x, offset, transform.localPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            UpdateOffset(-1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            UpdateOffset(1);
        }

    }

    public void UpdateOffset(int changeInValue)
    {
        offset += changeInValue;

        offset = Mathf.Clamp(offset,min,max);

        StartCoroutine(AdjustToOffset());
      
    }

    private IEnumerator AdjustToOffset()
    {

        if (isTransitioning == false)
        {
            isTransitioning = true;
            elapsedTime = 0;
            while (elapsedTime < durationOfMove)
            {
                elapsedTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, offset, transform.localPosition.z), elapsedTime/durationOfMove);
                yield return null;
            }

            isTransitioning = false;
        }
        else 
        {
            // Reset the other coroutine since offset has changed
            elapsedTime = 0;
        }
    }
}
