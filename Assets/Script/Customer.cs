using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public CustomerManager customerManager;
    private NavMeshAgent navMeshAgent;
    public bool reachedDestination;
    public Vector3 destination;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false; // NavMeshAgent'in otomatik durma özelliğini kapat
    }

    private void Update()
    {
        if (!reachedDestination)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                reachedDestination = true;
            }
        }
    }

    public void Move(Vector3 _destination)
    {
        destination = _destination;

        destination.y = transform.position.y;
        
        navMeshAgent.SetDestination(destination);
       
        reachedDestination = false;
    }

    public void SelectTask()
    {
        foreach (var customerTask in CustomerTaskManager.CustomerTask)
        {
            if (customerTask.isAvailable)
            {
                Move(customerTask.TargetWayPoint.position);
                customerTask.isAvailable = false;
            }
        }
    }
}