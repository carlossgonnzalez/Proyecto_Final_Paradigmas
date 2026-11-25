
using UnityEngine;

public class GhostPerception : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float detectionRange = 8f;

    [SerializeField] private float eyeHeight = 1.5f;

    [SerializeField] private LayerMask obstacleMask;

    public Transform Player => player;

    public bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector3 origin =
            transform.position + Vector3.up * eyeHeight;

        Vector3 target =
            player.position + Vector3.up * 0.5f;

        if (Vector3.Distance(origin, target) > detectionRange)
            return false;

        bool blocked = Physics.Linecast(
            origin,
            target,
            obstacleMask,
            QueryTriggerInteraction.Ignore
        );

        return !blocked;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position + Vector3.up * eyeHeight,
            detectionRange
        );
    }
}
