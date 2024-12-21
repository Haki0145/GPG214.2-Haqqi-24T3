using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    [SerializeField]private Vector2 rightPoint;
    [SerializeField]private Vector2 leftPoint;
    [SerializeField] float moveDuration;
    [SerializeField] float unitsToMove;
    private Vector2 targetPosition;

    void Start()
    {
        rightPoint = new Vector2(transform.position.x + unitsToMove, transform.position.y);
        leftPoint = new Vector2(transform.position.x - unitsToMove, transform.position.y);
        targetPosition = rightPoint;
        StartCoroutine(MoveFromOnePointToAntother());

    }

    private void OnValidate()
    {
        rightPoint = new Vector2(transform.position.x + unitsToMove, transform.position.y);
        leftPoint = new Vector2(transform.position.x - unitsToMove, transform.position.y);
    }



    IEnumerator MoveFromOnePointToAntother()
    {
        
        float elapsedTime = 0;
        Vector2 initialPosition = transform.position;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (targetPosition.magnitude == rightPoint.magnitude)
        {
            targetPosition = leftPoint;
        }
        else if (targetPosition.magnitude == leftPoint.magnitude)
        {
            targetPosition = rightPoint;
        }
        yield return new WaitForSeconds(Random.Range(0, 2));
        StartCoroutine(MoveFromOnePointToAntother());
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, rightPoint);

        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, leftPoint);

       
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(rightPoint, 0.1f);
        Gizmos.DrawSphere(leftPoint, 0.1f);

    }

}

