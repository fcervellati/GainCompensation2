
/// <summary>
/// This class store parameters of the staircase that does not change during the experiment
/// </summary>
[System.Serializable]
public class StaircaseSetting{
	public string Name = "Default Staircase";
	public int Direction = 1; // 1 for left, -1 for right
	public int FixedNumberOfReversalPoints = 20; // number of reversal points required to stop the study
	public int UsedNumberOfReversalPoints = 12; // number of reversal points used for calculating threshold
	public int Up = 3;
	public int Down = 1;

	public float WalkingVelocity;

	public float UpperBound = 100f;
	public float IncreaseFactor = 2.7646f; 
	public float DecreaseFactor = 0.4715f;
}