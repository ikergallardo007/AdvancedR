using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Points _points;
	[SerializeField] private Health _payerHealth;
	[SerializeField] private UIController _ui;
	[SerializeField] private SoundController _sound;
	[SerializeField] private InputSystem _inputs;
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
		//Event Listener
		_payerHealth.OnGetDamage += OnGetDamage;
		_payerHealth.OnGetHeal += OnGetHeal;
		_payerHealth.OnDie += OnDie;
		_points.OnGetPoints += OnAddPoints;
		_points.OnAddLevel += OnAddLevel;
        _inputs.OnKeyDamage += GetKeyDamage;
		_inputs.OnKeyHeal += GetKeyHeal;
		_inputs.OnKeyPoints += GetKeyPoints;
		_inputs.OnKeyAddLevel += GetKeyAddLevel;
    }

 


    #endregion

    #region Private Methods
    private void OnGetDamage()
	{
        _sound.PlayDamageSound();
		_ui.UpdateSliderLife(_payerHealth.CurrentHealth);
	}
	private void OnGetHeal()
	{
		_ui.UpdateSliderLife(_payerHealth.CurrentHealth);
	}
	private void OnDie()
	{
		_sound.PlayDieSound();
		Destroy(_payerHealth.gameObject);
	}
	private void OnAddPoints()
	{
		_ui.UpdatePoints(_points.CurrentPoints);
	}
	private void OnAddLevel()
    {
        _ui.UpdateLevel(_points.CurrentLevel);
    }
    private void GetKeyDamage()
    {
		_payerHealth.GetDamage(20);
    }
    private void GetKeyHeal()
    {
        _payerHealth.GetHeal(20);
    }
    private void GetKeyPoints()
    {
        _points.AddPoints(200);
    }
	private void GetKeyAddLevel()
    {
		_points.AddLevel();
    }

    #endregion
}
