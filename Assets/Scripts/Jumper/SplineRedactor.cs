using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class SplineRedactor : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private SplineComputer sc;
    private Vector3 thirdPointPos, secondPointPos, forthPointPosition;


    private void Start()
    {
        secondPointPos = sc.GetPointPosition(1);
        thirdPointPos = sc.GetPointPosition(2);
        forthPointPosition = sc.GetPointPosition(3);

    }
    private void FixedUpdate()
    {
        forthPointPosition.x = arrow.position.x;
        sc.SetPointPosition(3, forthPointPosition);
        thirdPointPos.x = secondPointPos.x + (arrow.position.x - secondPointPos.x)/2;
        sc.SetPointPosition(2, thirdPointPos);
    }

}
