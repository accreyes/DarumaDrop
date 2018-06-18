using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxObjectCreator : MonoBehaviour {

    public float timeLimit = 50;
    public Text timeRemaining;
    public Text score;
    public Text console;
    public int points = 0;
    private bool isStart=false;
    private float timeTemp;
    //private int rectCount = 3;

    [SerializeField] private GameObject redBox;
    [SerializeField] private GameObject blueBox;
    [SerializeField] private GameObject greenBox;
    [SerializeField] private Transform spawnParent;
    private List<GameObject> spawnedBoxes = new List<GameObject>(); //is the list element a pointer to the object? because the object is destroyed rather than the list?
    private int deleteCount = 0;
    

    // Use this for initialization
    void Start () {

        //Debug.Log(i);
        this.redBox.SetActive(false);
        this.greenBox.SetActive(false);
        this.blueBox.SetActive(false);
        this.spawnedBoxes.Clear();
        EventBroadcaster.Instance.AddObserver("ON_START_EVENT", this.OnStartEvent);
        EventBroadcaster.Instance.AddObserver("ON_CLICK_RED_EVENT", this.OnClickRedEvent);
        EventBroadcaster.Instance.AddObserver("ON_CLICK_GREEN_EVENT", this.OnClickGreenEvent);
        EventBroadcaster.Instance.AddObserver("ON_CLICK_BLUE_EVENT", this.OnClickBlueEvent);
        score.text = "Score : " + points.ToString();
        console.text = "";
        timeTemp = timeLimit;
        //OnStartEvent();
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            if (timeLimit > 0)
            {
                timeLimit -= Time.deltaTime;
                timeRemaining.text = "Remaining Time : " + ((int)timeLimit).ToString();
            }
            else
            {
                timeRemaining.text = "Time Is Up";
                console.text = "GAME OVER!";
                isStart = false;
            }
        }
        score.text = "Score : " + points.ToString();
    }

    private void OnStart(int numBox)
    {
        int temp = 0;
        timeLimit = timeTemp;
        Destroy();
        this.spawnedBoxes.Clear();
        console.text = "START!";
        points = 0;

        Vector3 position = Vector3.zero;
        position.x = 53.1f; position.y =-28.5f;
        while (temp < numBox)
        {
             int i = Random.Range(1, 10);
            //int i = 5;
        if (i<=3)
            {
                GameObject newRect = GameObject.Instantiate<GameObject>(this.redBox, this.spawnParent);
                newRect.transform.localPosition = position;
                newRect.gameObject.SetActive(true);
                position.y += 5;
                spawnedBoxes.Add(newRect);
                string s = newRect.name;
                Debug.Log(s);
            }
        else if(i>3 && i<=6)
            {
                GameObject newRect = GameObject.Instantiate<GameObject>(this.greenBox, this.spawnParent);
                newRect.transform.localPosition = position;
                newRect.gameObject.SetActive(true);
                position.y += 5;
                spawnedBoxes.Add(newRect);
                string s = newRect.name;
                Debug.Log(s);
            }
            else
            {
                GameObject newRect = GameObject.Instantiate<GameObject>(this.blueBox, this.spawnParent);
                newRect.transform.localPosition = position;
                newRect.gameObject.SetActive(true);
                position.y += 5;
                spawnedBoxes.Add(newRect);
            }
        temp++;
        }
        isStart = true;
    }

    private void OnStartEvent(Parameters parameter)
    {
        int numBox = parameter.GetIntExtra("NUM_BOX_KEY", 3);
        this.OnStart(numBox);
    }

    public void OnClickRedEvent()
    {
        if (timeLimit <= 0)
        {
            console.text = "GAME OVER!";
            
        }
        else if (deleteCount < this.spawnedBoxes.Count && this.spawnedBoxes[deleteCount].name.Equals("RedBox(Clone)"))
            {
            GameObject.Destroy(this.spawnedBoxes[deleteCount]); 
            deleteCount++;
            Reorganize();
            console.text = "CORRECT!";
            if (deleteCount >= this.spawnedBoxes.Count)
            {
                console.text = "CORRECT! YOU WIN!";
                isStart = false;
            }
        }
        else if (deleteCount < this.spawnedBoxes.Count)
        {
            console.text = "WRONG TRY AGAIN!";
        }

    }
    public void OnClickGreenEvent()
    {
        if (timeLimit <= 0)
        {
            console.text = "GAME OVER!";
            
        }
        else if (deleteCount < this.spawnedBoxes.Count && this.spawnedBoxes[deleteCount].name.Equals("GreenBox(Clone)"))
        {
            
            GameObject.Destroy(this.spawnedBoxes[deleteCount]);
            deleteCount++;
            Reorganize();
            console.text = "CORRECT!";
            if (deleteCount >= this.spawnedBoxes.Count)
            {
                console.text = "CORRECT! YOU WIN!";
                isStart = false;
            }
        }
        else if (deleteCount < this.spawnedBoxes.Count)
        {
            console.text = "WRONG TRY AGAIN!";
        }

    }
    public void OnClickBlueEvent()
    {
        if(timeLimit<=0)
        {
            console.text = "GAME OVER!";
        }
        else if (deleteCount < this.spawnedBoxes.Count && this.spawnedBoxes[deleteCount].name.Equals("BlueBox(Clone)"))
        {
            GameObject.Destroy(this.spawnedBoxes[deleteCount]);
            deleteCount++;
            Reorganize();
            console.text = "CORRECT!";
            if (deleteCount >= this.spawnedBoxes.Count)
            {
                console.text = "CORRECT! YOU WIN!";
                isStart = false;
            }
        }
        else if (deleteCount < this.spawnedBoxes.Count)
        {
            console.text = "WRONG TRY AGAIN!";
        }
        
        
    }

    private void Reorganize()
    {
        for(int i = deleteCount;i<this.spawnedBoxes.Count;i++)
        {
            this.spawnedBoxes[i].transform.Translate(0, -5.0f, 0);
        }
        points++;
    }
    private void Destroy()
    {
        for (int i = 0; i < this.spawnedBoxes.Count; i++)
        {
            GameObject.Destroy(this.spawnedBoxes[i]); 
        }
        
    }

    private void Win()
    {

    }
}
