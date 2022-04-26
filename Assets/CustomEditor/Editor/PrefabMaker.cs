using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabMaker : EditorWindow
{
    //float recty;

    private string prefabName = "EmptyPrefab";
    private float ObjScale = 1;

    private Vector3 ChildPos = new Vector3(0, 0, 0);
    // float ChildRotation = 0;
    private Vector3 ChildAngle = new Vector3(0, 0, 0);
    //Vector3 ObjTransform = new Vector3(0, 0, 0);
    private Object ChildObj =null;

    private Object PrefabScript = null;

    private bool SetAnimator;
    private Animator anim;
    private Object AnimationController = null;
    private Object Avatar = null;

    private bool SetRigidBody;
    private bool SetGravity;
    private bool SetKinetic;

    private bool CreatePrefab;
    const string PrefabRoute = "Assets/";
    private string folderRoute;

    private bool OptionEnabe = true;

    [MenuItem("Tools/�亸�� ���� ������ �����ո���� ��")]
    public static void ShowWindow()
    {

        //PrefabMaker window = (PrefabMaker)GetWindow(typeof(PrefabMaker),true, "�亸�� ���� ������ �����ո���� ��");
        PrefabMaker window = (PrefabMaker)GetWindowWithRect(typeof(PrefabMaker),new Rect(0,0,500,450),true, "�亸�� ���� ������ �����ո���� ��");
        window.Show();
        //GetWindow(typeof(PrefabMaker));

        
    }


    private void OnGUI()
    {
        GUILayout.Label("������!",EditorStyles.boldLabel);

        EditorGUILayout.Space();

        prefabName = EditorGUILayout.TextField("������Ʈ�̸�",prefabName);
        folderRoute = EditorGUILayout.TextField("�����հ��", folderRoute);
        CreatePrefab = EditorGUILayout.Toggle("������ȭ", CreatePrefab);
        ObjScale = EditorGUILayout.Slider("������ũ��", ObjScale, 0, 10);

        EditorGUILayout.Space();

        OptionEnabe = EditorGUILayout.BeginToggleGroup("Ȯ����", OptionEnabe);
        if(OptionEnabe)
        {
            ChildObj = EditorGUILayout.ObjectField("�ڽĿ�����Ʈ", ChildObj, typeof(GameObject), true);
            ChildPos = EditorGUILayout.Vector3Field("�ڽ���ġ", ChildPos);
            ChildAngle = EditorGUILayout.Vector3Field("�ڽķ����̼�", ChildAngle);

            EditorGUILayout.Space();

            SetRigidBody = EditorGUILayout.Toggle("������ٵ��߰�", SetRigidBody);
            if (SetRigidBody != false)
            {
                SetGravity = EditorGUILayout.Toggle("�߷�����", SetGravity);
                SetKinetic = EditorGUILayout.Toggle("Ű��ƽ", SetKinetic);
            }

            EditorGUILayout.Space();

            SetAnimator = EditorGUILayout.Toggle("�ִϸ������߰�", SetAnimator);
            if (SetAnimator == true)
            {
                AnimationController = EditorGUILayout.ObjectField("�ִϸ��̼� ��Ʈ�ѷ�", AnimationController, typeof(RuntimeAnimatorController));
                Avatar = EditorGUILayout.ObjectField("�ƹ�Ÿ", Avatar, typeof(Avatar));
            }

            EditorGUILayout.Space();

            PrefabScript = EditorGUILayout.ObjectField("��ũ��Ʈ�߰�", PrefabScript, typeof(MonoScript), true);



        }

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
                rb.useGravity = SetGravity ? false : true;
                rb.isKinematic = SetKinetic;
            }
            if (SetAnimator != false)
            {
                anim = EmptyObj.AddComponent<Animator>();
                if(AnimationController!=null)
                {
                    anim.runtimeAnimatorController = AnimationController as RuntimeAnimatorController;

                }
                if(Avatar!=null)
                {
                    anim.avatar = Avatar as Avatar;
                }
            }

            if (ChildObj != null)
            {
                Instantiate(ChildObj, ChildPos, Quaternion.Euler(ChildAngle), EmptyObj.transform);
            }

            if(CreatePrefab!=false)
            {
                

                string path = PrefabRoute + folderRoute +"/"+ EmptyObj.name + ".prefab";
                //AssetDatabase.CreateAsset(EmptyObj, path);
                PrefabUtility.SaveAsPrefabAssetAndConnect(EmptyObj, path, InteractionMode.UserAction);
            }
        }


    }

}
