using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private float input_x;
    private float input_z;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input_x = Input.GetAxis("Horizontal");
        input_z = Input.GetAxis("Vertical");

        //Vector3 velocity = new Vector3(input_x, 0, input_z);
        ////ベクトルの向きを取得
        //Vector3 direction = velocity.normalized;

        ////移動距離を計算
        //float distance = speed * Time.deltaTime;
        ////移動先を計算
        //Vector3 destination = transform.position + direction * distance;

        ////移動先に向けて回転
        //transform.LookAt(destination);
        ////移動先の座標を設定
        //transform.position = destination;
        Vector3 moveDirection = new Vector3(input_x, 0, input_z);
        transform.LookAt(transform.position + moveDirection);
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

    }
}
