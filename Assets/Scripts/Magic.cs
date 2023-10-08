using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Transform _target = null;

    public LayerMask ObjectLayerMask;
    public LayerMask NoObjectLayerMask;

    private float _originalScale;
    private float _originalDistance;
    private float _minScale = 0.5f; // minimum scale for the object
    private float _maxScale = 2.0f; // maximum scale for the object
    private float _maxDistance = 5.0f; // maximum distance between camera and object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit objectHit;
            if (Physics.Raycast(transform.position, transform.forward, out objectHit, Mathf.Infinity, ObjectLayerMask))
            {
                _target = objectHit.transform;
                _target.GetComponent<Rigidbody>().isKinematic = true;
                _originalScale = _target.localScale.x;
                _originalDistance = Vector3.Distance(transform.position, _target.position);
            }
        }
        if (Input.GetMouseButtonUp(0) && null != _target)
        {
            _target.GetComponent<Rigidbody>().isKinematic = false;
            _target = null;
        }
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        RaycastHit noObjectHit;
        if (Physics.Raycast(transform.position, transform.forward, out noObjectHit, Mathf.Infinity, NoObjectLayerMask))
        {
            var bounds = _target.GetComponent<Renderer>().bounds;
            var positionOffset = transform.forward * (bounds.center - _target.transform.position).magnitude;

            _target.position = noObjectHit.point - positionOffset;

            float distance = Vector3.Distance(transform.position, _target.position);
            float scaleMultiplier = distance / _originalDistance;

            _target.localScale = scaleMultiplier * _originalScale * Vector3.one;

            Debug.DrawRay(transform.position, transform.forward * noObjectHit.distance, Color.green);
            Debug.Log("Detected layer: " + LayerMask.LayerToName(noObjectHit.collider.gameObject.layer));
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        }
    }


}
