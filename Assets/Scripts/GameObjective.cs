using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjective : MonoBehaviour
{
    public int objectiveID;
    public string objectiveName;
    public bool objectiveActive;
    public bool objectiveCompleted;
    public int objectivePartsTotal;
    public int objectiveStep;

    private Text objMessageField;
    private Text objDoneMessageField;



    [Header("Events")]
    public GameEvent onObjectiveActivated;
    public GameEvent onObjectiveCompleted;

    private GameObject objManager;

    private void Awake()
    {
        objManager = GameObject.Find("ObjectiveManager");
        //objMessageField = objManager.GetComponent<ObjectiveTracker>().objMessageField;
        //objDoneMessageField = objManager.GetComponent<ObjectiveTracker>().objDoneMessageField;


        if (objectiveActive)
        {
            StartQuest();
        }


    }

    // THIS FUNCTION STARTS AN OBJECTIVE, ADDS IT TO THE OBJECTIVE UI, CALLS UI/SOUND FEEDBACK, MARKS IT AS ACTIVE
    public void StartQuest()
    {
        Debug.Log("Objective " + objectiveName + " started.");
        objectiveActive = true;
        //StartCoroutine(ShowStartMessage());
        objManager.GetComponent<ObjectiveTracker>().AddObjective(this);
        if (onObjectiveActivated != null)
        {
            onObjectiveActivated.Raise(this, null);
        }



    }

    // THIS FUNCTION MOVES AN ACTIVE OBJECTIVE ONE 'STEP' AHEAD - THIS FINISHES ANY QUEST NOT ABOUT DOING EXACTLY THE SAME THING X TIMES.
    public void AddQuestStep()
    {
        if (objectiveActive)
        {
            objectiveStep++;
            if (objectiveStep == objectivePartsTotal)
            {
                objectiveCompleted = true;
                //StartCoroutine(ShowEndMessage());

                if (onObjectiveCompleted != null)
                {
                    onObjectiveCompleted.Raise(this, null);
                }
                objManager.GetComponent<ObjectiveTracker>().RemoveObj(objectiveID);
                Debug.Log("Objective " + objectiveName + " complete.");
            }
        }

    }

    IEnumerator ShowStartMessage()
    {
        if (objMessageField.text != null)
        {
            objMessageField.text = "New objective: " + objectiveName;
            yield return new WaitForSeconds(5); // ADJUST THIS NUMBER TO MAKE OBJECTIVE STARTING TEXT STAY LONGER/SHORTER
            objMessageField.text = null;
        }
        else
        {
            objMessageField.text = "Multiple new objectives!";
            yield return new WaitForSeconds(5);
            objMessageField.text = null;
        }
    }

    IEnumerator ShowEndMessage()
    {
        if (objDoneMessageField.text != null)
        {
            objDoneMessageField.text = "Objective complete: " + objectiveName;
            yield return new WaitForSeconds(5); // ADJUST THIS NUMBER TO MAKE OBJECTIVE ENDING TEXT STAY LONGER/SHORTER
            objDoneMessageField.text = null;
        }
        else
        {
            objDoneMessageField.text = "Multiple objectives completed!";
            yield return new WaitForSeconds(5);
            objDoneMessageField.text = null;
        }

    }

}