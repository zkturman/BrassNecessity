using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class PlayerControllerInputs : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool lightAttack;
	public bool strongAttack;
	public bool applyElement;
	public int switchWeapon;
	public bool pause;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnLightAttack(InputValue value)
	{
		LightAttackInput(value.isPressed);
	}

	public void OnStrongAttack(InputValue value)
    {
		StrongAttackInput(value.isPressed);
    }

	public void OnSwitchWeapon(InputValue value)
    {
		float input = value.Get<float>();
		SwitchWeaponInput((int)input);
    }

	public void OnApplyElement(InputValue value)
    {
		ApplyElementInput(value.isPressed);
    }

	public void OnPause(InputValue value)
    {
		PauseInput(value.isPressed);
    }


#endif


	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

	public void LightAttackInput(bool newAttackState)
    {
		lightAttack = newAttackState;
    }

	public void StrongAttackInput(bool newAttackState)
    {
		strongAttack = newAttackState;
    }

	public void SwitchWeaponInput(int newSwitchValue)
    {
		switchWeapon = newSwitchValue;
    }

	public void ApplyElementInput(bool newApplyState)
    {
		applyElement = newApplyState;
    }

	public void PauseInput(bool newPauseValue)
    {
		pause = newPauseValue;
    }

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
