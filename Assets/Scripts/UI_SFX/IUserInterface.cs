using UnityEngine;

/// <summary>
/// An interface for UI components, such as ScoreUI and HealthUI.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Initialize the UI component to the initial value of the variable being displayed
    /// </summary>
    void InitializeUI();

    /// <summary>
    /// Set UI component to current value of the variable it is displaying
    /// </summary>
    void UpdateUI();
}
