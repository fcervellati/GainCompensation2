using UnityEngine;
using System.ComponentModel;

public class StudySettings:INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
	float m_startingGain;
	float m_increaseFactor;
	float m_decreaseFactor;
	float m_upperBound;
	int m_fixedNumberOfReversalPoints;
	int m_usedNumberOfReversalPoints;

	protected void OnPropertyChanged(string propertyName)
	{
		var handler = PropertyChanged;
		if (handler != null) {
			handler (this, new PropertyChangedEventArgs(propertyName));
			}
	}
	public StudySettings()
	{}
	public override string ToString ()
	{
		return string.Format ("[StudySettings: StartingGain={0}, IncreaseFactor={1}, DecreaseFactor={2}, FixedNumberOfReversalPoint={3}, UsedNumberOfReversalPoint={4}]", StartingGain, IncreaseFactor, DecreaseFactor, FixedNumberOfReversalPoints, UsedNumberOfReversalPoints);
	}
	public StudySettings (float startingGain, float increaseFactor, float decreaseFactor, int fixedNumber, int usedNumber)
	{
		m_startingGain = startingGain;
		m_increaseFactor = increaseFactor;
		m_decreaseFactor = decreaseFactor;
		m_fixedNumberOfReversalPoints = fixedNumber;
		m_usedNumberOfReversalPoints = usedNumber;	
	}
	public override bool Equals(object obj)
	{
		if (obj == null || GetType () != obj.GetType ())
			return false;
		StudySettings s = (StudySettings)obj;
		m_startingGain = s.m_startingGain;
		m_increaseFactor = s.m_increaseFactor;
		m_decreaseFactor = s.m_decreaseFactor;
		m_fixedNumberOfReversalPoints = s.m_fixedNumberOfReversalPoints;
		m_usedNumberOfReversalPoints = s.m_usedNumberOfReversalPoints;
		return (m_startingGain == s.m_startingGain) && (m_increaseFactor == s.m_increaseFactor) && (m_decreaseFactor == s.m_decreaseFactor)
		&& (m_fixedNumberOfReversalPoints == s.m_fixedNumberOfReversalPoints) && (m_usedNumberOfReversalPoints == s.m_usedNumberOfReversalPoints);
	}
	public float StartingGain
	{
		get{ return m_startingGain;}
		set{ 
			if (value != m_startingGain) {
				m_startingGain = value;
				OnPropertyChanged ("StartingGain");
			}	
			}
	}
	public float IncreaseFactor
	{
		get{ return m_increaseFactor;}
		set{ 
			if (value != m_increaseFactor) {
				m_increaseFactor = value;
				OnPropertyChanged ("IncreaseFactor");
			}	
		}
	}
	public float DecreaseFactor
	{
		get{ return m_decreaseFactor;}
		set{ 
			if (value != m_decreaseFactor) {
				m_decreaseFactor = value;
				OnPropertyChanged ("DecreaseFactor");
			}	
		}
	}
	public int FixedNumberOfReversalPoints
	{
		get{ return m_fixedNumberOfReversalPoints;}
		set{ 
			if (value != m_fixedNumberOfReversalPoints) {
				m_fixedNumberOfReversalPoints = value;
				OnPropertyChanged ("FixedNumberOfReversalPoint");
			}	
		}
	}
	public int UsedNumberOfReversalPoints
	{
		get{ return m_usedNumberOfReversalPoints;}
		set{ 
			if (value != m_usedNumberOfReversalPoints) {
				m_usedNumberOfReversalPoints = value;
				OnPropertyChanged ("UsedNumberOfReversalPoint");
			}	
		}
	}

}

