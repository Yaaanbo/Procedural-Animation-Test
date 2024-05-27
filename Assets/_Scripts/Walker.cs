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

    // Start is called before the first frame update
    void Start()
    {
        leftFootOffset = leftFootTarget.localPosition;
        rightFootOffset = rightFootTarget.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        leftFootTarget.localPosition = leftFootOffset +
            this.transform.InverseTransformVector(leftFootTarget.forward) * horizontalCurve.Evaluate(Time.time) +
            this.transform.InverseTransformVector(leftFootTarget.up) * verticalCurve.Evaluate(Time.time + .5f);
        rightFootTarget.localPosition = rightFootOffset +
            this.transform.InverseTransformVector(rightFootTarget.forward) * horizontalCurve.Evaluate(Time.time - 1) +
            this.transform.InverseTransformVector(rightFootTarget.up) * verticalCurve.Evaluate(Time.time - .5f); ;
    }
}
