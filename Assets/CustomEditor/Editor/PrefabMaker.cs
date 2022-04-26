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

    [MenuItem("Tools/�亸�� ���� ������ ������Ʈ����� ��")]
    public static void ShowWindow()
    {
        PrefabMaker window = (PrefabMaker)GetWindow(typeof(PrefabMaker),true, "�亸�� ���� ������ ������Ʈ����� ��");
        window.Show();
        //GetWindow(typeof(PrefabMaker));
    }

    private void OnGUI()
    {
        GUILayout.Label("������!",EditorStyles.boldLabel);

        prefabName = EditorGUILayout.TextField("������Ʈ�̸�",prefabName);



        OptionEnabe = EditorGUILayout.BeginToggleGroup("Ȯ����", OptionEnabe);
        ObjScale = EditorGUILayout.Slider("������ũ��", ObjScale, 0, 10);
        ChildObj = EditorGUILayout.ObjectField("�ڽĿ�����Ʈ", ChildObj, typeof(GameObject), true);
        //ObjTransform = EditorGUILayout.Vector3Field("������ ��ġ", ObjTransform);
        ChildPos = EditorGUILayout.Vector3Field("�ڽ���ġ", ChildPos);
        ChildAngle = EditorGUILayout.Vector3Field("�ڽķ����̼�", ChildAngle);
        //ChildRotation = EditorGUILayout.Slider("�ڽ� �����̼�", ChildRotation, 0, 360);
        SetRigidBody = EditorGUILayout.Toggle("������ٵ��߰�", SetRigidBody);
        SetAnimator = EditorGUILayout.Toggle("�ִϸ������߰�", SetAnimator);
        PrefabScript = EditorGUILayout.ObjectField("��ũ��Ʈ�߰�", PrefabScript, typeof(MonoScript), true);
        CreatePrefab = EditorGUILayout.Toggle("������ȭ", CreatePrefab);
        EditorGUILayout.EndToggleGroup();
        if(OptionEnabe==false)
        {
            ObjScale = 1;
            ChildPos = new Vector3(0, 0, 0);
            PrefabScript = null;
        }
        //PrefabScript = EditorGUILayout.
        //ChildObj = EditorGUILayout.ObjectField(ChildObj, typeof(GameObject));

        if(GUILayout.Button("��̴�!"/*,EditorStyles.boldLabel*/))
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
