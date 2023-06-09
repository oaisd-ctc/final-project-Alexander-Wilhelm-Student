using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRotate : MonoBehaviour
{
    [SerializeField] float spinSpeed;
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(1, 0, transform.rotation.eulerAngles.z + (spinSpeed * Time.deltaTime));
    }
}
