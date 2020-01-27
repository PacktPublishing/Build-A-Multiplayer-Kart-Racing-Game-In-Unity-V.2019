using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Renderer[] rends;
    float alpha = 1;
    float flashSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        rends = this.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            r.material.SetFloat("_Mode", 3.0f);
            r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            r.material.SetInt("_ZWrite", 0);
            r.material.DisableKeyword("_ALPHATEST_ON");
            r.material.EnableKeyword("_ALPHABLEND_ON");
            r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            r.material.renderQueue = 3000;
            r.material.color = new Color(1, 1, 1, alpha);
        }
    }

    void OnDisable()
    {
        foreach (Renderer r in rends)
        {
            r.material.SetFloat("_Mode", 0.0f);
            r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            r.material.SetInt("_ZWrite", 1);
            r.material.DisableKeyword("_ALPHATEST_ON");
            r.material.DisableKeyword("_ALPHABLEND_ON");
            r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            r.material.renderQueue = -1;
            r.material.color = new Color(1, 1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        alpha = 0.3f + Mathf.PingPong(Time.time * flashSpeed, 0.7f);
        foreach (Renderer r in rends)
        {
            r.material.color = new Color(1, 1, 1, alpha);
        }
    }
}
