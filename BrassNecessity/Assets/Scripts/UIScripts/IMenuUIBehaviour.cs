using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuUIBehaviour
{

    public void NavigateToNextElement(Vector2 direction);

    public void ExecuteCurrentButton();
}
