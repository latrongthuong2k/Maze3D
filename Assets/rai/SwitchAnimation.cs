using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject switchObject;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;
    [SerializeField]
    float animationTime;
    float time = 0;

    void Start()
    {
        switchObject.transform.position = startPos.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime / animationTime;
            if (time >= animationTime) time = animationTime;
            AnimationButton();
        }
        else
        {
            time -= Time.deltaTime / animationTime;
            if (time < 0) time = 0;
            AnimationButton();
        }
    }

    private void AnimationButton()
    {
        switchObject.transform.position = Vector3.Lerp(startPos.position, endPos.position, time / animationTime);
    }
}
