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

    [MenuItem("Tools/�亸�� ���� ����� ����ȭ ������Ʈ")]
    public static void ShowWindow()
    {

        //PrefabMaker window = (PrefabMaker)GetWindow(typeof(PrefabMaker),true, "�亸�� ���� ������ �����ո���� ��");
        FractureObjMaker window = (FractureObjMaker)GetWindowWithRect(typeof(FractureObjMaker), new Rect(0, 0, 500, 450), true, "�亸�� ���� ����� ����ȭ ������Ʈ");
        window.Show();
        //GetWindow(typeof(PrefabMaker));

        
    }

    private void OnGUI()
    {
        GUILayout.Label("����ȭ ������Ʈ ����", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        prefabName = EditorGUILayout.TextField("������Ʈ�̸�", prefabName);
        PrefabObj = EditorGUILayout.ObjectField("�����տ�����Ʈ",PrefabObj,typeof(GameObject),true);
        FractureObj = EditorGUILayout.ObjectField("�ڽĿ�����Ʈ", FractureObj, typeof(GameObject), true);
        folderRoute = EditorGUILayout.TextField("�����հ��", folderRoute);
        CreatePrefab = EditorGUILayout.Toggle("������ȭ", CreatePrefab);
        ObjScale = EditorGUILayout.Slider("������ũ��", ObjScale, 0, 10);
        PrefabScript = EditorGUILayout.ObjectField("��ũ��Ʈ�߰�", PrefabScript, typeof(MonoScript), true);
        if (GUILayout.Button("����"/*,EditorStyles.boldLabel*/))
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
