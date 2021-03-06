﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoMove = false;
    public GameObject player = null;

    [SerializeField]
    private Vector3 offset = new Vector3();

    public Vector3 smallScreenOffset = new Vector3(3, 6, -2);
    public Vector3 bigScreenOffset = new Vector3(3, 6, -3);

    public float speed = 0.25f;

    private float followSpeed = 1.0f;
    public float followSpeedPortrait = 1.0f;
    public float followSpeedLandscape = 1.0f;

    private float leftMarginPosition = -18.5f;
    private float RightMarginPosition = 22.5f;
    public float leftMarginPositionPortrait = -22.5f;
    public float RightMarginPositionPortrait = 23.5f;
    public float leftMarginPositionLandscape = -18.5f;
    public float RightMarginPositionLandscape = 22.5f;

    Vector3 depth = Vector3.zero;
    Vector3 pos = Vector3.zero;

    private void Start()
    {
        //in portrait or landscape mode we have different offsets
        if (Camera.main.aspect < 1.0f) { 
            offset = smallScreenOffset;
            followSpeed = followSpeedPortrait;
            leftMarginPosition = leftMarginPositionPortrait;
            RightMarginPosition = RightMarginPositionPortrait;
        }
        else { 
            offset = bigScreenOffset;
            followSpeed = followSpeedLandscape;
            leftMarginPosition = leftMarginPositionLandscape;
            RightMarginPosition = RightMarginPositionLandscape;
        }
        pos = player.transform.position + offset;
        gameObject.transform.position = new Vector3(pos.x, offset.y, pos.z);
    }

    private void Update()
    {
        if (!Manager.Instance.CanPlay()) return;

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
