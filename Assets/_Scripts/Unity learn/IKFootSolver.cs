using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField] private LayerMask terrainLayer = default;
    [SerializeField] private Transform body = default;
    [SerializeField] private IKFootSolver otherFoot = default;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float stepDist = 4f;
    [SerializeField] private float stepLength = 4f;
    [SerializeField] private float stepHeight = 1f;
    [SerializeField] private Vector3 footOffset = default;

    private float lerp;
    private float footSpacing;
    private Vector3 oldPos, currentPos, newPos;
    private Vector3 oldNormal, currentNormal, newNormal;    

    // Start is called before the first frame update
    void Start()
    {
        footSpacing = this.transform.localPosition.x;
        currentPos = newPos = oldPos = this.transform.position;
        currentNormal = newNormal = oldNormal = this.transform.up;
        lerp = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = currentPos;
        this.transform.up = currentNormal;

        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);

        if(Physics.Raycast(ray, out RaycastHit hit, 10f, terrainLayer.value))
        {
            Debug.Log(terrainLayer.value);
            if(Vector3.Distance(newPos, hit.point) > stepDist && !otherFoot.IsMoving() && lerp >= 1f)
            {
                lerp = 0;
                int direction = body.InverseTransformPoint(hit.point).z > body.InverseTransformPoint(newPos).z ? 1 : -1;
                newPos = hit.point + (body.forward * stepLength * direction) + footOffset;
                newNormal = hit.normal;
            }
        }

        if(lerp < 1f)
        {
            Vector3 tempPos = Vector3.Lerp(oldPos, newPos, lerp);
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPos = tempPos;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPos = newPos;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPos, .05f);
    }

    public bool IsMoving()
    {
        return lerp < 1f;
    }
}
