using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Transform partrol;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float velocity;
    //[SerializeField] BoxCollider2D box;

    private Vector3 startPos, endPos;
    private int currentPointIndex = 0;
    private List<Vector2> positions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        if (partrol != null)
        {
            for (int i = 0; i < partrol.childCount; i++)
                positions.Add(partrol.GetChild(i).position);
        }

        if (positions.Count > 0)
        {
            currentPointIndex = -1;
            GoToNextPoint();
        }
    }



    // Update is called once per frame
    void Update()
    {
        //if (positions.Count <= 0)
            //return;

        var pos = transform.position;
        var offset = endPos - pos;

        if (offset.magnitude > 0.1)
        {
            rigid.velocity = offset.normalized * velocity;
        }
        else
        {
            rigid.velocity = Vector2.zero;
            GoToNextPoint();
        }
    }

    private void GoToNextPoint()
    {
        currentPointIndex++;
        currentPointIndex = currentPointIndex % positions.Count;
        var nextPointIndex = (currentPointIndex + 1) % positions.Count;

        startPos = positions[currentPointIndex];
        endPos = positions[nextPointIndex];
        transform.position = startPos;
    }
}
