using UnityEngine;
using System.Collections.Generic;

public abstract class GitCharacterController : MonoBehaviour {

    public float speed = 10.0f;
    public float turnSpeed = 10.0f;
    protected Vector3 target;
    protected Queue<Vector3> targetQueue = new Queue<Vector3>();
    protected PathFinding pathFinding;
    protected List<Node> path = new List<Node>();
    protected int nextPosition = 0;
        
    private bool nextTarget = true;

    protected Animator animationController;
    
    protected bool reachedDestination = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        collidedWith(coll);
    }

    protected virtual void collidedWith(Collision2D coll)
    {
    }

    protected void initialize()
    {
        target = transform.position;
        pathFinding = GetComponent<PathFinding>();
        animationController = GetComponent<Animator>();
        animationController.SetBool("Moving", false);
    }

    protected void RotateToPoint(Vector3 origin, Vector3 destination)
    {
        var direction = destination - origin;

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            float correction = -90.0f;
            float angle = correction + (Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
        }
    }

    protected void addTarget(Vector3 newTarget)
    {
        if (targetQueue.Count <= 5)
        {
            targetQueue.Enqueue(newTarget);
        }
    }

    protected Vector3 getNextTarget()
    {
        if(targetQueue.Count > 0)
        {
            return targetQueue.Dequeue();
        }
        return new Vector3(-1000,-1000,-1000);
    }

    protected void clearTargets()
    {
        targetQueue.Clear();
        nextTarget = true;
    }

    protected void MoveToPosition()
    {
        var destination = Vector3.zero;

        if (nextTarget)
        {
            destination = getNextTarget();
            if (!destination.Equals(new Vector3(-1000, -1000, -1000)) && !target.Equals(destination))
            {
                // We make sure the path is legit before changing the current one
                var tempPath = pathFinding.FindPath(transform.position, destination);
                if (tempPath.Count != 0)
                {
                    target = destination;
                    path = tempPath;
                    reachedDestination = false;
                    nextTarget = false;
                    nextPosition = 0;
                }
            }
        }

        // If a path is available to move
        if (path.Count != 0)
        {
            animationController.SetBool("Moving", true);
            // If we haven't reached our goal yet
            if (nextPosition < path.Count)
            {
                var nextPos = path[nextPosition].worldPosition_;
                nextPos.z = transform.position.z;
                // If we haven't reached the position of this node
                if (transform.position != nextPos)
                {
                    RotateToPoint(transform.position, nextPos);
                    var oldPos = transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                    if (name == "Player")
                    {
                        print("Moved: " + Vector3.Magnitude(oldPos - transform.position));
                    }

                }
                else
                {
                    // Otherwise, we seek the next position
                    nextPosition++;
                    if (nextPosition < path.Count)
                    {
                        RotateToPoint(transform.position, nextPos);
                        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                    }
                }
            }
            else
            {
                // We reached our goal, we can reset the path
                path = new List<Node>();
                nextPosition = 0;
                nextTarget = true;
                animationController.SetBool("Moving", false);
                reachedDestination = true;
            }
        }
    }
}
