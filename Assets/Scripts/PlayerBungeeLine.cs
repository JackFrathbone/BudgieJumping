using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBungeeLine : MonoBehaviour
{
    [SerializeField] Transform _bungeeOrigin;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _bungeeOrigin.position);
    }
}
