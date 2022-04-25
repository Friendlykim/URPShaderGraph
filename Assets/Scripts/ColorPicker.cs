using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    SkinnedMeshRenderer mesh;
    [SerializeField]
    Material Mat;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Obj = GameObject.Find("BodyUp");
        mesh = Obj.GetComponent<SkinnedMeshRenderer>();

        Mat = mesh.sharedMaterial;


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
            Color color = Color.Lerp(new Color(0.3764706f, 0.1137255f, 0.1137255f), new Color(0.1137255f, 0.2929987f, 0.3764706f), Time.deltaTime);
            Color color2 = Color.Lerp(new Color(0.1137255f, 0.2929987f, 0.3764706f), new Color(0.3764706f, 0.1137255f, 0.1137255f), Time.deltaTime);
            Mat.SetColor("Maincolor", color);
            Mat.SetColor("LightColor", color2);
            Color cc11 = GetRGBToHSV(new Color(0.3764706f, 0.1137255f, 0.1137255f));
        }
    }

    Color GetRGBToHSV(Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return new Color(h, s, v);
    }
}
