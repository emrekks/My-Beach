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
    
    public int inQueueCustomerCount = 3;
    public List<Customer> inQueueCustomers;
    private void Awake()
    {
        inQueueCustomers = new List<Customer>();
        customerPool = new ObjectPool<Customer>(customerPrefab, poolSize, transform);
        StartCoroutine(SpawnCustomer());

        SelectWayPoint();
    }

    private IEnumerator SpawnCustomer()
    {
        for (int i = 0; i < inQueueCustomerCount; i++)
        {
            if (inQueueCustomers.Count < inQueueCustomerCount)
            {
                SpawnCustomerDelay();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void SpawnCustomerDelay()
    {
        Customer newCustomer = customerPool.GetObject();
        newCustomer.customerManager = this;
        newCustomer.transform.position = spawnPoint.position;
        newCustomer.gameObject.SetActive(true);
        inQueueCustomers.Add(newCustomer);
    }

    public void SelectWayPoint()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (!wayPoints[i].isQueueBusy && inQueueCustomers.Count > 0)
            {
                Customer selectedCustomer = inQueueCustomers[0];
                inQueueCustomers.RemoveAt(0);

                wayPoints[i].customer = selectedCustomer;
                wayPoints[i].customer.Move(wayPoints[i].transform.position);

                wayPoints[i].isQueueBusy = true;
            }
        }
    }

    // Example of how to despawn a customer and return it to the pool
    private void DespawnCustomer(Customer customer)
    {
        customer.gameObject.SetActive(false);
        inQueueCustomers.Remove(customer);
    }
}