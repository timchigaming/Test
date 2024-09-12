using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField, Range(1f, 10f)] float VisionRadius;
    [SerializeField] LayerMask BarrierLayer;
    [SerializeField] Transform ClosestBarrier;
    [SerializeField] bool IsHiding;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  if (Input.GetKeyDown(KeyCode.E))
  {
      if (IsHiding)
      {
    Unhide();
      }
      else
      {
    CheckForBarrierAndHide();
      }
  }
    }

    void CheckForBarrierAndHide()
    {
  Collider[] hitColliders = Physics.OverlapSphere(transform.position, VisionRadius, BarrierLayer);

  if(hitColliders.Length > 0)
  {
      ClosestBarrier = null;
      float closestDistance = Mathf.Infinity;

      foreach(Collider collider in hitColliders)
      {
        float distance = Vector3.Distance(transform.position, collider.transform.position);

        if(distance < closestDistance)
        {
            Debug.Log("E enter");

            closestDistance = distance;
            ClosestBarrier = collider.transform;
        }
      }

      if (ClosestBarrier != null)
      {
        Vector3 directionToBarrier = ClosestBarrier.position - transform.position;

        float leftDistance = Vector3.Distance(ClosestBarrier.position + ClosestBarrier.right * (ClosestBarrier.localScale.z /2), transform.position);
        float rightDistance = Vector3.Distance(ClosestBarrier.position - ClosestBarrier.right * (ClosestBarrier.localScale.z /2), transform.position);

        if (leftDistance < rightDistance)
        {
            transform.position = ClosestBarrier.position - ClosestBarrier.right * (ClosestBarrier.localScale.z / 2 + transform.localScale.z / 2);
        }
        else
        {
            transform.position = ClosestBarrier.position + ClosestBarrier.right * (ClosestBarrier.localScale.z / 2 + transform.localScale.z / 2);
        }
      }
  }
  else
  {
      // ???? 
  }
    }


    void Unhide()
    {
  // Денис, потом не забудь описать логику!!!!
    }


    void OnDrawGizmos()
    {
  Gizmos.color = Color.yellow;
  Gizmos.DrawWireSphere(transform.position, VisionRadius);
    }
}