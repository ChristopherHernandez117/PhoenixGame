using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Rigidbody rb;
    [SerializeField] NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        SetAgentInControl(true);
       
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        // transform.LookAt(player.transform);
    }

    public void ReactToHit()
    {
        SetAgentInControl(false);

        Vector3 reactionV = transform.position - player.transform.position;
        reactionV.Normalize();
        reactionV *= 5;
        reactionV.y = reactionV.y + 4;
        rb.AddForce(reactionV, ForceMode.Impulse);
        // rb.AddRelativeForce(Vector3.back * 2, ForceMode.Impulse);
        // rb.AddRelativeForce(Vector3.up * 2, ForceMode.Impulse);
        StartCoroutine(ReturnControlToAgentAfterDelay(0.25f));
    }

    void SetAgentInControl(bool agentInControl = true)
    {
        agent.isStopped = !agentInControl;  // if agent not in control, stop it (otherwise is should be on)
        rb.isKinematic = agentInControl;    // if agent in control, turn the rigidbody off (otherwise rb should be on)
    }

    private IEnumerator ReturnControlToAgentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetAgentInControl(true);
    }
}
