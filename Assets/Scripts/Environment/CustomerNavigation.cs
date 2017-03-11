using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerNavigation : MonoBehaviour
{
    private GameObject player;

    private GameObject goal;
    private NavMeshAgent _agent;
    private Animator _animator;

    private Vector3 _originPos;

    public bool timeToLeave;
    private bool triggered = false;

    public static int customerCount = 0;

    void OnDestroy()
    {
        customerCount--;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        customerCount++;
        _originPos = transform.position;
        goal = GameObject.FindGameObjectWithTag("NavMeshGoal");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.destination = goal.transform.position;
    }   

    private void Update()
    {
        if (_agent.remainingDistance < .005f == true && !triggered)
        {
            triggered = true;
            _animator.SetBool("Walking", false);
            _agent.Stop();
            StartCoroutine(RotateTowardsPlayer());
        }
        if (timeToLeave)
        {
            _agent.SetDestination(_originPos);
            _agent.Resume();
            _animator.SetBool("Walking", true);
        }
        if (timeToLeave && _agent.remainingDistance < .01f)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private IEnumerator RotateTowardsPlayer()
    {
        int iterations = 70;
        while (iterations > 0)
        {
            transform.Rotate(0f, -.5f, 0f);
            iterations--;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
