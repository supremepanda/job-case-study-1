using UnityEngine;
using UnityEngine.AI;

namespace Common
{
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private ApplicationType _applicationType;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _applicationType = GameManager.Instance.ApplicationType;
        }
        
        private void AgentMovement(Vector3 targetPosition)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(targetPosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                _agent.SetDestination(hit.point);
            }
        }
    
        private void Update()
        {
            if (_applicationType == ApplicationType.Build)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        AgentMovement(touch.position);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AgentMovement(Input.mousePosition);
                }
            }
        }
    }
}