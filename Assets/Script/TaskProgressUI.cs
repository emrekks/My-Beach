using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TaskProgressUI : MonoBehaviour
{
    //Task
    public Image taskBar;
    
    public Image taskBarFill;
    
    private bool _isRefilling;
    
    private bool _cleaningTaskCompleted = false;
   
    private bool _receptionTaskCompleted = false;
    
    private PlayerTasks _playerTasks;

    private void Awake()
    {
        _playerTasks = GetComponentInParent<PlayerTasks>();
    }

    private void Update()
    {
        RefillTaskTime();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isRefilling = false;

            if (!_isRefilling && taskBarFill.fillAmount >= 0)
            {
                taskBarFill.fillAmount -= Time.deltaTime / _playerTasks.taskDoingTime;
            }
            
            if (!_isRefilling && taskBarFill.fillAmount <= 0)
            {
                if (_playerTasks.task == Task.Cleaning && !_playerTasks.taskFinished)
                {
                    _playerTasks.taskFinished = true;
                    _playerTasks.CleaningSunbed();
                }

                if (_playerTasks.task == Task.Reception && !_playerTasks.taskFinished)
                {
                    _playerTasks.taskFinished = true;
                    _playerTasks.GivingTicketAndTakingMoney();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isRefilling && _playerTasks.taskFinished == false && taskBarFill.fillAmount > 0)
        {
            _isRefilling = true;
        }
    }

    void RefillTaskTime()
    {
        if (_isRefilling &&  taskBarFill.fillAmount <= _playerTasks.taskDoingTime)
        {
            taskBarFill.fillAmount += Time.deltaTime / _playerTasks.taskDoingTime;
        }
        else
        {
            _isRefilling = false;
        }
    }
}
