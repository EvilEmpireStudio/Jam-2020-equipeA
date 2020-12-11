using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBehaviour : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (GameManager.instance.mode == GameManager.Mode.Shop)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            GetComponent<Animation>().Play("catch");
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (GameManager.instance.mode == GameManager.Mode.Shop)
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }

    void OnMouseUp()
    {
        if (GameManager.instance.mode == GameManager.Mode.Shop)
        {
            GetComponent<Animation>().Play("release");
        }
    }
}
