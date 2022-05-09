using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class FractureObjMaker : EditorWindow
{
    private string prefabName = "ObjPrefab";
    private Object PrefabObj = null;
    private Object FractureObj = null;
    private bool CreatePrefab = true;
    const string PrefabRoute = "Assets/";
    private string folderRoute = "Prefab";
    private float ObjScale = 1;

    private Object PrefabScript = null;

    [MenuItem("Tools/밥보다 쉽게 만드는 파편화 오브젝트")]
    public static void ShowWindow()
    {

        //PrefabMaker window = (PrefabMaker)GetWindow(typeof(PrefabMaker),true, "밥보다 쉬운 누워서 프리팹만들기 툴");
        FractureObjMaker window = (FractureObjMaker)GetWindowWithRect(typeof(FractureObjMaker), new Rect(0, 0, 500, 450), true, "밥보다 쉽게 만드는 파편화 오브젝트");
        window.Show();
        //GetWindow(typeof(PrefabMaker));

        
    }

    private void OnGUI()
    {
        GUILayout.Label("파편화 오브젝트 제작", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        prefabName = EditorGUILayout.TextField("오브젝트이름", prefabName);
        PrefabObj = EditorGUILayout.ObjectField("프리팹오브젝트",PrefabObj,typeof(GameObject),true);
        FractureObj = EditorGUILayout.ObjectField("자식오브젝트", FractureObj, typeof(GameObject), true);
        folderRoute = EditorGUILayout.TextField("프리팹경로", folderRoute);
        CreatePrefab = EditorGUILayout.Toggle("프리팹화", CreatePrefab);
        ObjScale = EditorGUILayout.Slider("프리팹크기", ObjScale, 0, 10);
        PrefabScript = EditorGUILayout.ObjectField("스크립트추가", PrefabScript, typeof(MonoScript), true);
        if (GUILayout.Button("제작"/*,EditorStyles.boldLabel*/))
        {
            SpawnObject();
        }
    }


    private void SpawnObject()
    {
        GameObject EmptyObj = PrefabObj as GameObject;
        EmptyObj.transform.localScale = new Vector3(ObjScale, ObjScale, ObjScale);
        
        if(FractureObj != null)
        {
            //Instantiate(FractureObj,EmptyObj.transform.position,Quaternion.identity,EmptyObj.transform);
            Instantiate(FractureObj, EmptyObj.transform);
        }

        if(PrefabScript != null)
        {
            MonoScript script = PrefabScript as MonoScript;
            EmptyObj.AddComponent(script.GetClass());
        }

        if (CreatePrefab != false)
        {


        string path = PrefabRoute + folderRoute + "/" + EmptyObj.name + ".prefab";
                //AssetDatabase.CreateAsset(EmptyObj, path);
        PrefabUtility.SaveAsPrefabAssetAndConnect(EmptyObj, path, InteractionMode.UserAction);
        }

    }

}
