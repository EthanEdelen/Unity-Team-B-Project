using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerWaterMovement : MonoBehaviour
{
    float theta;
    float amp;

    void Start()
    {
        theta = 0;
        amp = 0.5f;
    }

    void FixedUpdate()
    {
        theta -= 0.07f;
        float offset = theta;
        for (int i = 0; i < 12; i++)
        { 
            Transform curChild = this.gameObject.transform.GetChild(i);
            float newY = Mathf.Sin(offset) * amp - 110;
            curChild.position = new Vector3(curChild.position.x, newY, curChild.position.z);
            offset += 0.7f;
        }
    }
}
