using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private bool isGrappling;
    private Vector3 grappleVelocity;
    public float grappleSpeed = 10f; // Adjust this speed as needed
    private CharacterController characterController;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        characterController = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            MovePlayerTowardsGrapplePoint();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            isGrappling = true;
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            grappleVelocity = (grapplePoint - player.position).normalized * grappleSpeed; // Calculate the grapple velocity
        }
        else
        {
            Debug.Log("No valid grapple point found.");
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        isGrappling = false;
    }

    private Vector3 currentGrapplePosition;

    void MovePlayerTowardsGrapplePoint()
    {
        Vector3 direction = grapplePoint - player.position;
        if (direction.magnitude > 1f)
        {
            characterController.Move(direction.normalized * Time.fixedDeltaTime * grappleSpeed); // Move the player towards the grapple point
        }
        else
        {
            StopGrapple();
        }
    }

    void DrawRope()
    {
        if (!isGrappling) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return isGrappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
