using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public CustomerQueue[] wayPoints;
    public Transform spawnPoint;

    //Object Pooling
    public Customer customerPrefab;
    private ObjectPool<Customer> customerPool;
    public int poolSize = 10;

    public List<Customer> inQueueCustomers;

    private void Awake()
    {
        inQueueCustomers = new List<Customer>();
       
        customerPool = new ObjectPool<Customer>(customerPrefab, poolSize, transform);

        SpawnCustomer();
    }

    private void SpawnCustomer()
    {
        for (int i = 0; inQueueCustomers.Count < 3; i++)
        {
            Customer newCustomer = customerPool.GetObject();

            newCustomer.customerManager = this;

            newCustomer.transform.position = spawnPoint.position;

            newCustomer.gameObject.SetActive(true);

            inQueueCustomers.Add(newCustomer);
        }

        SelectWayPoint();
    }

    public void SelectWayPoint()
    {
        foreach (var point in wayPoints)
        {
            if (!point.isQueueBusy)
            {
                for (int i = 0; i < inQueueCustomers.Count; i++)
                {
                    Debug.Log(i);
                    if (!inQueueCustomers[i].updatedNewPoint)
                    {
                        inQueueCustomers[i].Move(point.transform);
                      
                        inQueueCustomers[i].updatedNewPoint = true;

                        break;
                    }
                }

                point.isQueueBusy = true; // Move this line outside the inner loop
            }
        }
    }


    public void SelectTask()
    {

    }


    private void DespawnCustomer(Customer customer)
    {
        customer.gameObject.SetActive(false);
        inQueueCustomers.Remove(customer);
    }
}
