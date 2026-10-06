using UnityEngine;

public class Turret : MonoBehaviour
{
    public float angularSpeed = 90f;
    public Transform target;

    void Update()
    {
        // I draw the up direction so I can see where the turret faces
      //  Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        // degrees per second, scaled by deltaTime so it's frame-rate independent
      //  transform.Rotate(0f, 0f, angularSpeed * Time.deltaTime);

        Vector3 toTarget = (target.position - transform.position).normalized;
        //  float dot = Vector3.Dot(transform.up, toTarget);

        // if (dot > 0f)
        //     Debug.Log("In Front");
        //  else
        //     Debug.Log("Behind");

        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float directionAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle);

        float dot = Vector3.Dot(transform.up, toTarget);

        if (dot < 0.98f)
        {
            switch (Mathf.Sign(deltaAngle))
            {
                case 1:
                    transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
                    break;
                case -1:
                    transform.Rotate(0, 0, -angularSpeed * Time.deltaTime);
                    break;
            }
        }
        
    }

    
}


