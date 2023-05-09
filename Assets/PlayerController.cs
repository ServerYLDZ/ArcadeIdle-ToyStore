using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed;
    [SerializeField] private float rotationSpeed=500;

    private Touch _touch;

    private bool isMoving;
    private bool onDragStated;

    private Vector3 dragStartPoint;
    private Vector3 dragEndPoint;

    public Animator anim;
private void Start() {
     speed=GameManager.Instance.playerCurrentSpeed;
}
private void Awake() {
   
}
    private void Update()
    {
       
        if (Input.touchCount>0)
        {
            anim.SetBool("Walk",true);
            _touch = Input.GetTouch(0);
            if(_touch.phase== TouchPhase.Began&& GameManager.Instance.canMovePlayer)
            {
                isMoving = true;
                onDragStated = true;
                dragStartPoint = _touch.position;
                dragEndPoint = _touch.position;

            }
        }
        if (onDragStated)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                dragEndPoint = _touch.position;
            }
            if (_touch.phase == TouchPhase.Ended)
            {
                isMoving = false;
                onDragStated = false;
                dragEndPoint = _touch.position;

            }
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed*Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        anim.SetBool("Walk",false);
      
    }
    Quaternion CalculateRotation()
    {
        Quaternion tmp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return tmp;
    }
    Vector3 CalculateDirection()
    {
        Vector3 tmp = (dragEndPoint - dragStartPoint).normalized;
        tmp.z = tmp.y;
        tmp.y = 0;
        return tmp;
    }
}
