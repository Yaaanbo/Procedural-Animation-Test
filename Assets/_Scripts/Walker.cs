using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform leftFootTarget;
    [SerializeField] private Transform rightFootTarget;

    [Header("AnimationCurve")]
    [SerializeField] private AnimationCurve horizontalCurve;
    [SerializeField] private AnimationCurve verticalCurve;

    private Vector3 leftFootOffset;
    private Vector3 rightFootOffset;

    private float leftLegLast = 0f;
    private float rightLegLast = 0f;

    // Start is called before the first frame update
    void Start()
    {
        leftFootOffset = leftFootTarget.localPosition;
        rightFootOffset = rightFootTarget.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float leftLegForwardMovement = horizontalCurve.Evaluate(Time.time);
        float rightLegForwardMovement = horizontalCurve.Evaluate(Time.time - 1f);

        leftFootTarget.localPosition = leftFootOffset +
            this.transform.InverseTransformVector(leftFootTarget.forward) * leftLegForwardMovement +
            this.transform.InverseTransformVector(leftFootTarget.up) * verticalCurve.Evaluate(Time.time + .5f);
        rightFootTarget.localPosition = rightFootOffset +
            this.transform.InverseTransformVector(rightFootTarget.forward) * rightLegForwardMovement +
            this.transform.InverseTransformVector(rightFootTarget.up) * verticalCurve.Evaluate(Time.time - .5f);

        float leftLegDir = leftLegForwardMovement - leftLegLast;
        float rightLegDir = rightLegForwardMovement - rightLegLast;

        RaycastHit hit;
        if(leftLegDir < 0f &&
            Physics.Raycast(leftFootTarget.position + leftFootTarget.up, -leftFootTarget.up, out  hit, Mathf.Infinity))
        {
            leftFootTarget.position = hit.point;
            this.transform.position += this.transform.forward * Mathf.Abs(leftLegDir);
        }

        if (rightLegDir < 0f &&
            Physics.Raycast(rightFootTarget.position + rightFootTarget.up, -rightFootTarget.up, out hit, Mathf.Infinity))
        {
            rightFootTarget.position = hit.point;
            this.transform.position += this.transform.forward * Mathf.Abs(rightLegDir);
        }

        leftLegLast = leftLegForwardMovement;
        rightLegLast = rightLegForwardMovement; 
    }
}
