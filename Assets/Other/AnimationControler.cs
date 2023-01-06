using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationControler : MonoBehaviour
{
    [SerializeField] private Animator Animator = null;
    [SerializeField] private GameObject square2 = null;
    [SerializeField] private GameObject square3 = null;
    public Vector3 PosB;
    public Vector3 PosTween_C;
    public float TimeMove;
    private bool checkMove;
    private void Start()
    {
        checkMove = true;
    }
    private void Update()
    {
        if(!checkMove)
        {
            LerpO();
        }
    }
    public void AnimationBT()
    {
        Animator.Play("Move");

    }
    public void LerpBT()
    {
        checkMove = !checkMove;
    }
    private void LerpO()
    {
        square2.transform.position = Vector3.Lerp(square2.transform.position, PosB, TimeMove* Time.deltaTime);
    }
    public void TweenBT()
    {
        square3.transform.DOMove(PosTween_C, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    //private void Lerp1()
    //{
    //    square2.transform.position = Vector3.Lerp(square2.transform.position, -PosB, TimeMove * Time.deltaTime);
    //}
    
}
