using System;
using UnityEngine;

public class CustomerTaskManager : MonoBehaviour
{
    public static CustomerTask[] CustomerTask;
    private void Start()
    {
        CustomerTask = GetComponentsInChildren<CustomerTask>();
    }
}
