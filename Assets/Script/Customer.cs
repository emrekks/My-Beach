using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public CustomerManager customerManager;
    private NavMeshAgent navMeshAgent;
    public bool reachedDestination;
    public Transform destination;
    public bool updatedNewPoint;

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

    public void Move(Transform _destination)
    {
        destination = _destination;

        //destination.position.y = transform.position.y;
        
        navMeshAgent.SetDestination(destination.position);
       
        reachedDestination = false;
    }
}