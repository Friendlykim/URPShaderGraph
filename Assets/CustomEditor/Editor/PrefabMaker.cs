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

    [MenuItem("Tools/밥보다 쉬운 누워서 프리팹만들기 툴")]
    public static void ShowWindow()
    {

        //PrefabMaker window = (PrefabMaker)GetWindow(typeof(PrefabMaker),true, "밥보다 쉬운 누워서 프리팹만들기 툴");
        PrefabMaker window = (PrefabMaker)GetWindowWithRect(typeof(PrefabMaker),new Rect(0,0,500,450),true, "밥보다 쉬운 누워서 프리팹만들기 툴");
        window.Show();
        //GetWindow(typeof(PrefabMaker));

        
    }


    private void OnGUI()
    {
        GUILayout.Label("마참내!",EditorStyles.boldLabel);

        EditorGUILayout.Space();

        prefabName = EditorGUILayout.TextField("오브젝트이름",prefabName);
        folderRoute = EditorGUILayout.TextField("프리팹경로", folderRoute);
        CreatePrefab = EditorGUILayout.Toggle("프리팹화", CreatePrefab);
        ObjScale = EditorGUILayout.Slider("프리팹크기", ObjScale, 0, 10);

        EditorGUILayout.Space();

        OptionEnabe = EditorGUILayout.BeginToggleGroup("확장기능", OptionEnabe);
        if(OptionEnabe)
        {
            ChildObj = EditorGUILayout.ObjectField("자식오브젝트", ChildObj, typeof(GameObject), true);
            ChildPos = EditorGUILayout.Vector3Field("자식위치", ChildPos);
            ChildAngle = EditorGUILayout.Vector3Field("자식로테이션", ChildAngle);

            EditorGUILayout.Space();

            SetRigidBody = EditorGUILayout.Toggle("리지드바디추가", SetRigidBody);
            if (SetRigidBody != false)
            {
                SetGravity = EditorGUILayout.Toggle("중력제거", SetGravity);
                SetKinetic = EditorGUILayout.Toggle("키네틱", SetKinetic);
            }

            EditorGUILayout.Space();

            SetAnimator = EditorGUILayout.Toggle("애니매이터추가", SetAnimator);
            if (SetAnimator == true)
            {
                AnimationController = EditorGUILayout.ObjectField("애니메이션 컨트롤러", AnimationController, typeof(RuntimeAnimatorController));
                Avatar = EditorGUILayout.ObjectField("아바타", Avatar, typeof(Avatar));
            }

            EditorGUILayout.Space();

            PrefabScript = EditorGUILayout.ObjectField("스크립트추가", PrefabScript, typeof(MonoScript), true);



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
