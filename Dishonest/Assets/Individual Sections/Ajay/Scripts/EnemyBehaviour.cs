using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyBehaviour : MonoBehaviour {

	private NavMeshAgent AI;
	public Transform player;
	public bool _patrolWaiting;
	public float _totalWaitTime;
	public float moveSpeed;
	public float maxDis;
	public float minDis;
	enum AIState {Chase, Patrol}
	AIState myAIState;
	[SerializeField]
	List<Waypoints> _patrolPoints;
	int _currentPatrolIndex;
	bool _travelling;
	bool _waiting;
	bool _patrolForward;
	float _waitingTimer;
	// Use this for initialization
	void Start () 
	{
		myAIState = AIState.Patrol;

		AI = GetComponent<NavMeshAgent>();

		if(AI == null)
		{
			Debug.Log("Help" + gameObject.name);
		}
		else
		{
			if(_patrolPoints != null && _patrolPoints.Count >=2)
			{
				_currentPatrolIndex = 0;
				SetDestination();
			}
			//Chase();
		}
	}
	
	private void SetDestination()
	{
		if(_patrolPoints != null)
		{
			Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
			AI.SetDestination(targetVector);
			_travelling = true;
		}
	}
	 private void ChangePatrolPoint()
	{
			_currentPatrolIndex++;

			if(_currentPatrolIndex >= _patrolPoints.Count)
			{
				_currentPatrolIndex = 0;
			}
	}

	// Update is called once per frame
	public void Update () 
	{
		
		if (Vector3.Distance(transform.position, player.position) <= minDis)
        {
         	transform.LookAt(player);
		 	transform.position += transform.forward * moveSpeed * Time.deltaTime;
 
            if (Vector3.Distance(transform.position, player.position) <= maxDis)
            {
                
            }
		}
		else
		{
			//Checking to see if close to destination
			if(_travelling && AI.remainingDistance <= 1.0f)
			{
				_travelling = false;
			
			//If going to wait, then wait
				if(_patrolWaiting)
				{
					_waiting = true;
					_waitingTimer = 0f;
				}
				else
				{
					ChangePatrolPoint();
					SetDestination();
				}
			}

			if(_waiting)
			{
				_waitingTimer += Time.deltaTime;
				if (_waitingTimer >= _totalWaitTime)
				{
					_waiting = false;
					ChangePatrolPoint();
					SetDestination();
				}
			}
		}
	}
}