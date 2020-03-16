using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShdowTest : MonoBehaviour
{
    // Start is called before the first frame update
    Camera m_renderCam;
    RenderTexture rt;
    void Start()
    {
        m_renderCam = transform.Find("Camera").GetComponent<Camera>();
        rt = new RenderTexture(512, 512, 24);
        m_renderCam.targetTexture = rt;
        m_renderCam.SetReplacementShader(Shader.Find("Unlit/shadermap"), "RenderType");
        GameObject plane = GameObject.Find("Plane");
        Material m = plane.GetComponent<MeshRenderer>().material;
        m.SetTexture("_depthTexture", rt);
        Matrix4x4 vp  = GL.GetGPUProjectionMatrix(m_renderCam.projectionMatrix, false) * m_renderCam.worldToCameraMatrix;
        Matrix4x4 sm = new Matrix4x4();
        sm.m00 = 0.5f;
        sm.m11 = 0.5f;
        sm.m22 = 0.5f;
        sm.m33 = 1f;
        sm.m03 = 0.5f;
        sm.m13 = 0.5f;
        sm.m23 = 0.5f;
        vp = sm * vp;
        m.SetMatrix("_vpMatrix", vp);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
