using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    Rigidbody _rigidbody;
    public Transform body;
    public Transform target;
    public float speed = 10f, smoothing = .1f;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        body = this.transform;
        _rigidbody = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        material.color = onGround ? Color.green : Color.red;
        Target();
        //float speed = 1f;   
        //Vector3 direction = (target.position - body.position) * Time.fixedDeltaTime;
        //direction += Vector3.up * -Physics.gravity.y * Time.fixedDeltaTime;
        if (onGround)
        {
            //direction *= speed;
            //_rigidbody.MovePosition(targetPosition + misalignment);
            Vector3 misalignment = _rigidbody.position - body.position;
            _rigidbody.linearVelocity = Vector3.Lerp(
                _rigidbody.linearVelocity, 
                (target.position - body.position + misalignment) * speed, 
                smoothing
            );

            _rigidbody.AddForce(-Physics.gravity * Time.deltaTime);
            //_rigidbody.MovePosition(target.position + misalignment);
        }
    }

    bool onGround;
    void Target()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        onGround = Physics.Raycast(ray, out hit, .5f);
        if (onGround)
        {

            float min = .2f;
            float max = .4f;
            var targetHeight = Map(Mathf.Sin(Time.time * Mathf.PI * 2), -1, 1, min, max);
            Vector3 targetPosition = hit.point + Vector3.up * targetHeight;
            target.position = targetPosition;
        }

    }

    public static float Map(float x, float a1, float a2, float b1, float b2)
    {
        // Calculate the range differences
        float inputRange = a2 - a1;
        float outputRange = b2 - b1;

        // Calculate the normalized value of x within the input range
        float normalizedValue = (x - a1) / inputRange;

        // Map the normalized value to the output range
        float mappedValue = b1 + (normalizedValue * outputRange);

        return mappedValue;
    }

    void old()
    {
        Vector3 offset = _rigidbody.position - body.position;
        Vector3 velocity = _rigidbody.linearVelocity;
        var targetPosition = target.position;

        _rigidbody.position = Vector3.SmoothDamp(
            current: _rigidbody.position,
            target: targetPosition,
            currentVelocity: ref velocity,
            smoothTime: smoothing,
            maxSpeed: speed,
            deltaTime: Time.fixedDeltaTime
        );
    }
}
