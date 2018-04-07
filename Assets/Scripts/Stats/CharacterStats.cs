using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour {

	public int diceSides = 6; // dicesides set to public instead of hardcoding

	public Stat armor;

	// Attributes
	[Header("Attributes")]
	public Stat strength;
	public int strengthDamageDice {get; private set;}
	public int strengthDamageModifier = 0;
	public Stat dexterity;
	public Stat intelligence;
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	[Space]

	//Characteristics
	[Header("Characteristics")]
	public Stat damage;
	public Stat lift;
	public Stat hitPoints;
	public Stat will;
	public Stat perception;
	public Stat fatigue;
	public Stat basicSpeed;
	public Stat basicMove;

	// Set current health to max health
	// when starting the game.
	void Awake () {
		currentHealth = maxHealth;
		strengthDamageDice = StrengthDamageDice(strength); // Set the number of damage dice based on strength stat
	}

	// Get strength stat and set number of 
	// damage dice if between a certain range
	public int StrengthDamageDice(Stat strength){
		int str = strength.GetValue();
		if (str >=1 && str <= 11){
			return strengthDamageDice = 1;
		} else if (str >= 12 && str <= 17){
			return strengthDamageDice = 2;
		} else if (str >= 18 && str <= 23){
			return strengthDamageDice = 3;
		} else if (str >= 24 && str <= 29){
			return strengthDamageDice = 4;
		} else if (str >= 30 && str <= 35){
			return strengthDamageDice = 5;
		} else if (str>= 36 && str <= 41){
			return strengthDamageDice = 6;
		} else if (str >= 42 && str <= 47){
			return strengthDamageDice = 7;
		} else if (str <= 0){
			Debug.LogWarning("StrengthDamageDice returned as 0");
			return strengthDamageDice = 0;
		} else {
			Debug.LogWarning("StrengthDamageDice returned above specified range");
			return strengthDamageDice = 0;
		}
	}

	// Rolls dice and applies modifiers
	public int DiceRoller(int strengthDamageDice, int strengthDamageModifier){
		int diceTally = 0; // New int to keep a running tally
		while (strengthDamageDice > 0){
			int diceResult = Random.Range(1, (diceSides + 1));
			diceTally += diceResult;
			strengthDamageDice--;
		}
		return diceTally += strengthDamageModifier; // Returns result of tally after applying modifiers
	}
	
	// Damage the character
	public void TakeDamage (int diceTally) {
		// Subtract the armor value
		diceTally -= armor.GetValue ();
		diceTally = Mathf.Clamp (diceTally, 0, int.MaxValue);

		// Damage the character
		currentHealth -= diceTally;
		Debug.Log (transform.name + " takes " + diceTally + " damage.");

		// If health reaches zero
		if (currentHealth <= 0) {
			Die ();
		}
	}

	public virtual void Die () {
		// Die in some way
		// This method is meant to be overwritten
		Debug.Log (transform.name + " died.");
	}

}