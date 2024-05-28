using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimation : MonoBehaviour
{
    [SerializeField] private Transform leftFootTarget;
    [SerializeField] private Transform rightFootTarget;

    private Vector3 initLeftFootPos, initRightFootPos;
    private Vector3 lastLeftFootPos, lastRightFootPos;

    // Start is called before the first frame update
    void Start()
    {
        initLeftFootPos = leftFootTarget.localPosition;
        initRightFootPos = rightFootTarget.localPosition;

        lastLeftFootPos = leftFootTarget.position;
        lastRightFootPos = rightFootTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        leftFootTarget.position = lastLeftFootPos;
        rightFootTarget.position = lastRightFootPos;

        lastLeftFootPos = leftFootTarget.position;
        lastRightFootPos = rightFootTarget.position;
    }
}
