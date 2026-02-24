using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    #region Properties
    public event Action OnKeyDamage;
    public event Action OnKeyHeal;
    public event Action OnKeyPoints;
    public event Action OnKeyAddLevel;

    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
            OnKeyDamage?.Invoke();
        if (Input.GetKeyUp(KeyCode.H))
            OnKeyHeal?.Invoke();
        if (Input.GetKeyUp(KeyCode.P))
            OnKeyPoints?.Invoke();
        if (Input.GetKeyUp(KeyCode.L))
            OnKeyAddLevel?.Invoke();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion

    #region Gizmos
    #endregion
}
