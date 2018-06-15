using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoMove = false;
    public GameObject player = null;

    public Vector3 offset = new Vector3(3, 6, -3);
    public float speed = 0.25f;

    public float followSpeed = 1.0f;

    public float leftMarginPosition = -18.5f;
    public float RightMarginPosition = 22.5f;
    Vector3 depth = Vector3.zero;
    Vector3 pos = Vector3.zero;

    private void Start()
    {
        pos = player.transform.position + offset;
        gameObject.transform.position = new Vector3(pos.x, offset.y, pos.z);
    }

    private void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (autoMove)
        {
            depth = this.gameObject.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            pos = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
            gameObject.transform.position = new Vector3(Mathf.Clamp(pos.x, leftMarginPosition, RightMarginPosition), offset.y, depth.z);
        }
        else
        {
            pos = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime * followSpeed);
            gameObject.transform.position = new Vector3(Mathf.Clamp(pos.x, leftMarginPosition, RightMarginPosition), offset.y, pos.z);
        }
    }
}
