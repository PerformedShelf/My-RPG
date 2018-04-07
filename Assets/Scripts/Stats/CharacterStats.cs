using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour {

	public int diceSides = 6; // dicesides set to public instead of hardcoding

	public Stat armor;

	// Attributes
	[Header("Attributes")]
	public Stat strength;
	public int strengthDamageDice {get; private set;}
	public int strengthDamageModifier {get; private set;}
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
		strengthDamageModifier = StrengthDamageModifier(strength);
	}

	// Get strength stat and set number of 
	// damage dice if between a certain range
	public int StrengthDamageDice(Stat strength){
		int i = strength.GetValue();
		if (i >=1 && i <= 11){
			return strengthDamageDice = 1;
		} else if (i >= 12 && i <= 17){
			return strengthDamageDice = 2;
		} else if (i >= 18 && i <= 23){
			return strengthDamageDice = 3;
		} else if (i >= 24 && i <= 29){
			return strengthDamageDice = 4;
		} else if (i >= 30 && i <= 35){
			return strengthDamageDice = 5;
		} else if (i>= 36 && i <= 41){
			return strengthDamageDice = 6;
		} else if (i >= 42 && i <= 47){
			return strengthDamageDice = 7;
		} else if (i <= 0){
			Debug.LogWarning("StrengthDamageDice returned as 0");
			return strengthDamageDice = 0;
		} else {
			Debug.LogWarning("StrengthDamageDice returned above specified range");
			return strengthDamageDice = 0;
		}
	}

		public int StrengthDamageModifier(Stat strength){
			int i = strength.GetValue();
			if (i >= 1 && i <= 3){
				return strengthDamageModifier = -5;
			}	else if ( i == 4){
				return strengthDamageModifier = -4;
			}	else if ( i == 5 || i == 12 || i == 18 || i == 24 || i == 30 || i == 36 || (i >= 50 && i < 55) || (i >= 80 && i < 85 )){
				return strengthDamageModifier = -3;
			}	else if ( i == 6 || i == 13 || i == 19 || i == 25 || i == 31 || i == 37 || (i >= 55 && i < 60) || (i >= 85 && i < 90 )){
				return strengthDamageModifier = -2;
			}	else if ( i == 7 || i == 14 || i == 20 || i == 26 || i == 32 || i == 38 || (i >= 60 && i < 65) || (i >= 90 && i < 95 )){
				return strengthDamageModifier = -1;
			}	else if ( i == 8 || i == 15 || i == 21 || i == 27 || i == 33 || i == 39 || (i >= 65 && i < 70) || (i >= 95 && i < 100 )){
				return strengthDamageModifier = 0;
			}	else if ( i == 9 || i == 16 || i == 22 || i == 28 || i == 34 || (i >= 40 && i < 45) || (i >= 70 && i < 75) || i == 100 ){
				return strengthDamageModifier = 1;
			}	else if ( i == 10 || i == 17 || i == 23 || i == 29 || i == 35 || (i >= 45 && i < 50) || ( i >= 75 && i < 80 )){
				return strengthDamageModifier = 2;
			}	else if (i == 11){
				return strengthDamageModifier = 3;
			}	else if ( i <= 0 ){
				return strengthDamageModifier -999;
			}	else {
				Debug.LogWarning("strengthDamageModifier returned  above specified range");
				return strengthDamageModifier -999;
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