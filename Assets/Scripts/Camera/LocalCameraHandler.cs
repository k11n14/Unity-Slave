using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    public const float YAngle_MIN = - 90.0f;   //カメラのY方向の最小角度
    public const float YAngle_MAX = 90.0f;     //カメラのY方向の最大角度

    public Transform target;    //追跡するオブジェクトのtransform
    public Vector3 offset;      //追跡対象の中心位置調整用オフセット
    public Vector3 lookAt;     //targetとoffsetによる注視する座標

    //public float distance = 10.0f;    //キャラクターとカメラ間の角度
    //public float distance_min = 1.0f;  //キャラクターとの最小距離
    //public float distance_max = 20.0f; //キャラクターとの最大距離
    public float currentX = 0.0f;  //カメラをX方向に回転させる角度
    public float currentY = 0.0f;  //カメラをY方向に回転させる角度

    //カメラ回転用係数(値が大きいほど回転速度が上がる)
    public float moveX = 4.0f;     //マウスドラッグによるカメラX方向回転係数
    public float moveY = 2.0f;     //マウスドラッグによるカメラY方向回転係数
    public float moveX_QE = 0.1f;  //QEキーによるカメラX方向回転係数


    public Transform cameraAnchorPoint;
    //private Vector3 offset;
    //Input
    Vector2 viewInput;
   

    //Rotation
    //float cameraRotationX = 0;
    //float cameraRotationY = 0;

    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;

    private void Awake()
    {
        localCamera = GetComponent<Camera>();
        networkCharacterControllerPrototypeCustom = GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Detach camera if enabled
        if (localCamera.enabled)
            localCamera.transform.parent = null;

            offset =localCamera.transform.position - cameraAnchorPoint.position;//カメラのスタート位置
    }

    void LateUpdate()
    {
        if (cameraAnchorPoint == null)
            return;

        if (!localCamera.enabled)
            return;

        //QとEキーでカメラ回転
        if (Input.GetKey(KeyCode.Q))
        {
            currentX += -moveX_QE;
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentX += moveX_QE;
        }
        //if (Input.GetMouseButton(1))
        //{
        //    currentX += Input.GetAxis("Mouse X") * moveX;
        //    currentY += Input.GetAxis("Mouse Y") * moveY;
        //    currentY = Mathf.Clamp(currentY, YAngle_MIN, YAngle_MAX);

        //}
        //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distance_min, distance_max);

        lookAt = cameraAnchorPoint.position ;  //注視座標はtarget位置+offsetの座標

        //カメラ旋回処理
        //Vector3 dir = new Vector3(0, 0, 10);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

        //Move the camera to the position of the player
        localCamera.transform.position = cameraAnchorPoint.position + rotation*offset ;
        localCamera.transform.LookAt(lookAt);   //カメラをLookAtの方向に向けさせる

        //Calculate rotation
        // cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        // cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        // cameraRotationY += viewInput.x * Time.deltaTime * networkCharacterControllerPrototypeCustom.rotationSpeed;

        //Apply rotation
        // localCamera.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);

    }
    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
