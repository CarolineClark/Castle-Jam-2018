using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

    public List<string> names;
    private List<GroundDetectorSingle> groundDetectors = new List<GroundDetectorSingle>();

    private void Start()
    {
        foreach (string n in names) {
            groundDetectors.Add(findGroundDetectorByName(n));
        }
    }

    private GroundDetectorSingle findGroundDetectorByName(string childName)
    {
        return transform.Find(childName).gameObject.GetComponent<GroundDetectorSingle>();
    }

    public bool IsTouchingGroundSignPlatform()
    {
        bool grounded = false;
        foreach (GroundDetectorSingle detector in this.groundDetectors)
        {
            grounded = grounded || detector.RaycastHitsGroundSignPlatform();
        }
        return grounded;
    }

    public bool IsTouchingGroundSignPlayer()
    {
        bool grounded = false;
        foreach (GroundDetectorSingle detector in this.groundDetectors)
        {
            grounded = grounded || detector.RaycastHitsGroundSignPlayer();
        }
        return grounded;
    }

    public bool IsTouchingGroundSign()
    {
        bool grounded = false;
        foreach (GroundDetectorSingle detector in this.groundDetectors)
        {
            grounded = grounded || detector.RaycastHitsGroundSign();
        }
        return grounded;
    }

    public bool CanSeeGroundSignPlatform() {
        bool canSee = false;
        foreach (GroundDetectorSingle detector in this.groundDetectors)
        {
            canSee = canSee || detector.RaycastDetectsGroundSignPlatform();
        }
        return canSee;
    }
}
