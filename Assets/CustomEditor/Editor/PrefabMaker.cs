using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabMaker : EditorWindow
{
    string prefabName = "EmptyPrefab";
    float ObjScale = 1;
    Vector3 ChildPos = new Vector3(0, 0, 0);
   // float ChildRotation = 0;
    Vector3 ChildAngle = new Vector3(0, 0, 0);
    //Vector3 ObjTransform = new Vector3(0, 0, 0);
    Object ChildObj=null;
    Object PrefabScript = null;

    
    bool SetAnimator;
    bool SetRigidBody;

    bool CreatePrefab;
    string PrefabRoute = "Assets/";
    bool OptionEnabe = true;

    [MenuItem("Tools/밥보다 쉬운 누워서 오브젝트만들기 툴")]
    public static void ShowWindow()
    {
        PrefabMaker window = (PrefabMaker)GetWindow(typeof(PrefabMaker),true, "밥보다 쉬운 누워서 오브젝트만들기 툴");
        window.Show();
        //GetWindow(typeof(PrefabMaker));
    }

    private void OnGUI()
    {
        GUILayout.Label("마참내!",EditorStyles.boldLabel);

        prefabName = EditorGUILayout.TextField("오브젝트이름",prefabName);



        OptionEnabe = EditorGUILayout.BeginToggleGroup("확장기능", OptionEnabe);
        ObjScale = EditorGUILayout.Slider("프리팹크기", ObjScale, 0, 10);
        ChildObj = EditorGUILayout.ObjectField("자식오브젝트", ChildObj, typeof(GameObject), true);
        //ObjTransform = EditorGUILayout.Vector3Field("프리팹 위치", ObjTransform);
        ChildPos = EditorGUILayout.Vector3Field("자식위치", ChildPos);
        ChildAngle = EditorGUILayout.Vector3Field("자식로테이션", ChildAngle);
        //ChildRotation = EditorGUILayout.Slider("자식 로테이션", ChildRotation, 0, 360);
        SetRigidBody = EditorGUILayout.Toggle("리지드바디추가", SetRigidBody);
        SetAnimator = EditorGUILayout.Toggle("애니매이터추가", SetAnimator);
        PrefabScript = EditorGUILayout.ObjectField("스크립트추가", PrefabScript, typeof(MonoScript), true);
        CreatePrefab = EditorGUILayout.Toggle("프리팹화", CreatePrefab);
        EditorGUILayout.EndToggleGroup();
        if(OptionEnabe==false)
        {
            ObjScale = 1;
            ChildPos = new Vector3(0, 0, 0);
            PrefabScript = null;
        }
        //PrefabScript = EditorGUILayout.
        //ChildObj = EditorGUILayout.ObjectField(ChildObj, typeof(GameObject));

        if(GUILayout.Button("즐겁다!"/*,EditorStyles.boldLabel*/))
        {
            SpawnObject();
        }

        
    }

    private void SpawnObject()
    {
        GameObject EmptyObj = new GameObject(prefabName);
        EmptyObj.transform.localScale = new Vector3(ObjScale, ObjScale, ObjScale);

        if(OptionEnabe!=false)
        {

            if(PrefabScript!=null)
            {
                MonoScript script = PrefabScript as MonoScript;
                EmptyObj.AddComponent(script.GetClass());
            }
            if(SetRigidBody!=false)
            {
                Rigidbody rb = EmptyObj.AddComponent<Rigidbody>();
            }
            if (SetAnimator != false)
            {
                Animator anim = EmptyObj.AddComponent<Animator>();
            }

            if (ChildObj != null)
            {
                Instantiate(ChildObj, ChildPos, Quaternion.Euler(ChildAngle), EmptyObj.transform);
            }

           /* if(CreatePrefab!=false)
            {
                // Material material = new Material(Shader.Find("Specular"));
                //AssetDatabase.CreateAsset(material, "Assets/MyMaterial.mat");
                if (!Directory.Exists(PrefabRoute))
                {
                    Directory.CreateDirectory(PrefabRoute);
                    //AssetDatabase.CreateFolder("Assets/My Folder", "My Another Folder");
                }
                string path = PrefabRoute + EmptyObj.name + ".prefab";
                AssetDatabase.CreateAsset(EmptyObj, path);
            }*/
        }


    }

}
