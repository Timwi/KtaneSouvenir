using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SHeptabutton
{
	[Question("What was the label of the button in {0}?", ThreeColumns6Answers, "PUSH", "HOLD", "BOOM", "WAIT", "LOOK", "FAIL", "DONE", "PRESS", "ABORT", "TOUCH", "CLICK", "AVOID", "SOLVE", "PUNCH", "SELECT", "BUTTON", "IGNITE", "LAUNCH", "DEFUSE", "DISARM", "IGNORE", "RELEASE", "EXPLODE", "DEPRESS", "CONTROL", "NEGLECT", "ACHIEVE", "POLYGON")]
	QLabel,
	
	[Question("What color was the button in the {1} stage of {0}?", ThreeColumns6Answers, "Aqua", "Azure", "Black", "Blue", "Cream", "Cyan", "Forest", "Gray", "Green", "Indigo", "Jade", "Lime", "Magenta", "Maroon", "Maya", "Mint", "Olive", "Orange", "Pink", "Plum", "Red", "Rose", "Salmon", "Teal", "Violet", "White", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
	QColor,

	[Discriminator("the Heptabutton that was labeled {0}", Arguments = ["PUSH", "HOLD", "BOOM", "WAIT", "LOOK", "FAIL", "DONE", "PRESS", "ABORT", "TOUCH", "CLICK", "AVOID", "SOLVE", "PUNCH", "SELECT", "BUTTON", "IGNITE", "LAUNCH", "DEFUSE", "DISARM", "IGNORE", "RELEASE", "EXPLODE", "DEPRESS", "CONTROL", "NEGLECT", "ACHIEVE", "POLYGON"], ArgumentGroupSize = 1)]
	DLabel,

	[Discriminator("the Heptabutton that was colored {0} in the {1} stage", TranslateArguments = [true, false], Arguments = ["aqua", QandA.Ordinal, "azure", QandA.Ordinal, "black", QandA.Ordinal, "blue", QandA.Ordinal, "cream", QandA.Ordinal, "cyan", QandA.Ordinal, "forest", QandA.Ordinal, "gray", QandA.Ordinal, "green", QandA.Ordinal, "indigo", QandA.Ordinal, "jade", QandA.Ordinal, "lime", QandA.Ordinal, "magenta", QandA.Ordinal, "maroon", QandA.Ordinal, "maya", QandA.Ordinal, "mint", QandA.Ordinal, "olive", QandA.Ordinal, "orange", QandA.Ordinal, "pink", QandA.Ordinal, "plum", QandA.Ordinal, "red", QandA.Ordinal, "rose", QandA.Ordinal, "salmon", QandA.Ordinal, "teal", QandA.Ordinal, "violet", QandA.Ordinal, "white", QandA.Ordinal, "yellow", QandA.Ordinal], ArgumentGroupSize = 2)]
	DColor
}

public partial class SouvenirModule
{
	[Handler("heptabutton", "Heptabutton", typeof(SHeptabutton), "Infusion", AddThe = true)]
	[ManualQuestion("What was the label of the button?")]
	[ManualQuestion("What was the color of the button in each stage?")]
	private IEnumerator<SouvenirInstruction> ProcessHeptabutton(ModuleData module)
	{
		var comp = GetComponent(module, "TheHeptabutton");
		var fldStage = GetField<int>(comp, "stage");
		var fldLabel = GetField<TextMesh>(comp, "ButtonText", isPublic: true);
		var fldColor = GetField<int>(comp, "colorIndex");
		var fldHeld = GetField<bool>(comp, "buttonHeld");
		
		var labels = new string[28] { "PUSH", "HOLD", "BOOM", "WAIT", "LOOK", "FAIL", "DONE", "PRESS", "ABORT", "TOUCH", "CLICK", "AVOID", "SOLVE", "PUNCH", "SELECT", "BUTTON", "IGNITE", "LAUNCH", "DEFUSE", "DISARM", "IGNORE", "RELEASE", "EXPLODE", "DEPRESS", "CONTROL", "NEGLECT", "ACHIEVE", "POLYGON" };
		var colors = new string[27] { "Aqua", "Azure", "Black", "Blue", "Cream", "Cyan", "Forest", "Gray", "Green", "Indigo", "Jade", "Lime", "Magenta", "Maroon", "Maya", "Mint", "Olive", "Orange", "Pink", "Plum", "Red", "Rose", "Salmon", "Teal", "Violet", "White", "Yellow" };
		
		var label = fldLabel.Get().text;
		var allColors = new int[7];
		
		while (module.Unsolved)
		{
			var stage = fldStage.Get();
			if (stage < 7 && !fldHeld.Get())
				allColors[stage] = fldColor.Get();
			yield return null;
		}
		
		yield return new Discriminator(SHeptabutton.DLabel, $"label", label, args: [label]);

		yield return question(SHeptabutton.QLabel).AvoidDiscriminators(SHeptabutton.DLabel).Answers(label);
		
		for (var i = 0; i < 7; i++)
		{
			yield return new Discriminator(SHeptabutton.DColor, $"color{i}", allColors[i], args: [colors[allColors[i]].ToLowerInvariant(), Ordinal(i + 1)]);
			yield return question(SHeptabutton.QColor, args: [Ordinal(i + 1)]).AvoidDiscriminators(SHeptabutton.DColor).Answers(colors[allColors[i]].ToString());
		}
	}
}