using UnityEngine;
using System;

public class Points : MonoBehaviour
{
    #region Properties
    public int CurrentPoints { get; set; }
    public int CurrentLevel { get; set; }
    public event Action OnGetPoints;
    public event Action OnAddLevel;

    #endregion



    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        CurrentPoints = 0;
        CurrentLevel = 1;
    }

    //private void Update()
    //{
    //	if (Input.GetKeyUp(KeyCode.Escape))
    //		AddPoints(200);
    //}
    #endregion

    #region Public Methods
    public void AddPoints(int pointsToAdd)
    {
        CurrentPoints += pointsToAdd;
        OnGetPoints?.Invoke();
    }

    public void AddLevel()
    {
        CurrentLevel++;
        OnAddLevel?.Invoke();
    }
    #endregion
}
