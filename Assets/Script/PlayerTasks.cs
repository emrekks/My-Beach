using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Task
{
    Cleaning,
    Reception,
    TakingItem,
    LevelUp
}
public class PlayerTasks : MonoBehaviour
{
    //Task
    public Task task;
    public float taskDoingTime;
    public float taskResetTime;
    public bool taskFinished;
    
    //Cleaning
    [HideInInspector]public MeshRenderer objectMeshRenderer;
    [HideInInspector]public Material dirtyObject;
    [HideInInspector]public Material cleanObject;
    
    //Reception
    [HideInInspector]public bool givingTicket;
    public CustomerQueue CustomerQueue;
    public Transform[] moneySlots;
    private int selectMoneySlot = 0;
    public GameObject money;
    
    //TakingItem
    [HideInInspector]public GameObject takingItem;

    //LevelUp
    [HideInInspector]public GameObject levelupTargetObject;
    

    public void CleaningSunbed()
    {
        objectMeshRenderer.material = cleanObject;
    }

    public void GivingTicketAndTakingMoney()
    {
        if (CustomerQueue.customer.reachedDestination)
        {
            Instantiate(money, moneySlots[selectMoneySlot]);
            selectMoneySlot++;
           //CustomerQueue.customer.SelectTask();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerTasks))]
public class TasksEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerTasks script = (PlayerTasks)target;

        if (script.task == Task.Cleaning)
        {
            script.objectMeshRenderer = EditorGUILayout.ObjectField("Mesh Renderer", script.objectMeshRenderer, typeof(MeshRenderer), true) as MeshRenderer;
            script.dirtyObject = EditorGUILayout.ObjectField("Dirty Material", script.dirtyObject, typeof(Material), true) as Material;
            script.cleanObject = EditorGUILayout.ObjectField("Clean Material", script.cleanObject, typeof(Material), true) as Material;
        }
        else if (script.task == Task.TakingItem)
        {
            script.takingItem = EditorGUILayout.ObjectField("Taking Item", script.takingItem, typeof(GameObject), true) as GameObject;
        }
        else if (script.task == Task.LevelUp)
        {
            script.levelupTargetObject = EditorGUILayout.ObjectField("Level Up Target Object", script.levelupTargetObject, typeof(GameObject), true) as GameObject;
        }
    }
}
#endif 
