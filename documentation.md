# Documentation

This document explains how to implement Souvenir support for a module. We will start simple but we will eventually cover all of the detail.

## Create a new source file for the module

Inside the `Handlers` folder, you will find a source file for each supported module. Feel free to create a new one, or — since many existing Souvenir implementations are very similar — take a copy of one and rename it.

## The enum type

Every Souvenir-supported module gets an enum type whose name is `S` followed by the name of the module (for example: `S3DMaze`). In it, you can declare *questions* that Souvenir can ask about (as well as *discriminators*, which are optional and will be covered later).

To create a new Souvenir question, declare an enum value and give it a `[SouvenirQuestion(...)]` attribute. We will use the two questions for 3D Maze as an example:

```cs
public enum S3DMaze
{
    [SouvenirQuestion("What were the markings in {0}?", ThreeColumns6Answers, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
    Markings,

    [SouvenirQuestion("What was the cardinal direction in {0}?", TwoColumns4Answers, "North", "South", "West", "East", TranslateAnswers = true)]
    Bearing
}
```

In order, the parameters to the `[SouvenirQuestion(...)]` attributes are:

* The question text. Use `{0}` instead of the module name, so that Souvenir can use the same question to ask either `What were the markings in 3D Maze?` as well as `What were the markings in the 3D Maze you solved first/second/etc.?`.
* An `AnswerLayout` value that determines how many answers are shown and how they are laid out. Take a look at `AnswerLayout.cs` to find out what values are available. Use `OneColumn4Answers` if the answers are going to be rather long (e.g. station names in *London Underground*).
* A list of possible answers. In our example, we can see that the first question lists all the combinations of markings possible in 3D Maze, while the second includes all of the cardinal directions that could be answers.
	* Please include this whenever the full set of possible answers is available, even if there are hundreds.
	* If your handler provides the full list of answers at runtime, please include `ExampleAnswers = [...]` instead so that the question can still be rendered meaningfully in TestHarness. *1000 Words* shows an example of this.
	* If the answers are numbers or strings of random characters that can be automatically generated, use an `AnswerGenerator`, which is a separate attribute to be added below the `[SouvenirQuestion(...)]` attribute. For example, `[AnswerGenerator.Integers(0, 10)]` will generate numerical answers in the range 0 through 10; `[AnswerGenerator.Strings('A', 'Z')]` will generate answers consisting of a single letter. You can search the code for `AnswerGenerator` to see some examples.
* Specify `TranslateAnswers = true` if the answers need translating into other languages.

After the list of answers, several optional parameters can be added. The most important ones are `Arguments`, `ArgumentGroupSize`, and `TranslateArguments`. To explain these, let’s use this example from *Bitmaps*:

```cs
    [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", ThreeColumns6Answers, TranslateArguments = [true, true], Arguments = ["white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right"], ArgumentGroupSize = 2)]
```

We want the question to vary from time to time: sometimes ask about the top-left quadrant, sometimes the other quadrants; and sometimes the black pixels, sometimes the white. To accommodate this, we note the following:

* Use `{1}`, `{2}` etc. as placeholders within the question string (remember to keep `{0}` reserved for the module name).
* Use `Arguments = [...]` to specify possible arguments (text fragments) that could be inserted for `{1}` etc. In this example, note that the strings alternate between color and quadrant; in other words, it’s `{1}` then `{2}` then `{1}` again then `{2}` etc.
	* As a special case, if you’re inserting ordinal numbers (first, second, third, etc.), please use the special constant `QandA.Ordinal` instead. *Fizz Buzz* provides a good example for this.
* Use `ArgumentGroupSize = n` to specify the number of inserted strings (not counting the `{0}`). In this case, this is 2 (the color and the quadrant).
* Use `TranslateArguments = [...]` to specify which of the arguments need translating into other languages. This should be `true` for anything that is used to construct a sentence; it should only be false for things like names displayed on the module or numerical codes. The number of booleans in this array must be equal to `ArgumentGroupSize`. In this example, both of them need translating, but check out *Red Cipher* for an example where one argument is a translatable word but the other is just a number.
* If *none* of the inserted strings need translating into other languages, `TranslateArguments` can be omitted.
* You may also want to specify `Type = AnswerType.Something` for the following special cases:
	* `Type = AnswerType.Sprites` or `Type = AnswerType.Audio` if the answers are sprites (images) or audio clips
	* `Type = AnswerType.DynamicFont` if the answers are to be rendered in a font provided by the module
	* Another `AnswerType` value if the answers are to be rendered in a font provided by Souvenir

A full list of the options is available below in the section titled “Full list of options”.

## The handler

The actual handler for each module is a method with the following properties:

* The name should be `Process` plus the name of the module without “The”, for example: `ProcessBlueButton`.
* The return type must be `IEnumerator<SouvenirInstruction>`, the method should be `private`, and take one parameter of type `ModuleData`.
* The method must have a `[SouvenirHandler(...)]` attribute with at least 4 arguments, which are as follows:
	* **Module ID** (also known as `ModuleType` in the `KMBombModule` component) (string)
	* **Module name without ‘The’** (string)
	* **Enum type** that we declared in the previous section, for example: `typeof(S3DMaze)` (Type)
	* **Contributor** (this name will automatically be credited in `CONTRIBUTORS.md`) (string)
	* If the module name starts with “The”, **`AddThe = true`**
	* If the module is a boss module, **`IsBossModule = true`**

### Getting values from the module

The actual method body is a coroutine that will obtain values from the module in question, wait for the module to be solved, and then generate questions. The first few lines generally look like this:

```cs
var comp = GetComponent(module, "TheOneTwoThreeGame");
var name = GetField<int>(comp, "NameSelector").Get(v => v is < 0 or >= 13 ? "expected name index 0–12" : null);
var names = GetArrayField<string>(comp, "Names").Get(expectedLength: 13);
```

Let’s go through these line by line:

* `GetComponent`: obtains the script that is attached to the module. You need to specify the name of the module’s script class here.
* `GetField`: obtains a reference to a field in that class. In this example, we are:
	* obtaining the field `NameSelector` of type `int`;
	* getting its value;
	* verifying that the value is in the range 0–12. We want to be quite strict with this kind of validation of values; if the programming of the module later changes in a way that breaks your Souvenir handler, we want to have useful logging that tells us exactly what went wrong.
* `GetArrayField`: obtains a reference to a field whose type is an array. Technically, `GetArrayField<string>` is the same as `GetField<string[]>`, but using `GetArrayField` means that when we use `.Get()` to obtain the array, we can validate the length of the array (in this example: 13) as well as every element of the array (not shown in this example).
* If the field is public, you *must* specify `isPublic: true` (example: *Boggle*).

### Waiting

To wait for the module to be solved, simply write:

```cs
yield return WaitForSolve;
```

There is also `WaitForActivate` (waits until module activation, when the lights turn on in the game room); and `WaitForUnignoredModules` (useful only for boss modules).

Of course, you can get values from fields before or after waiting for it to solve, or even during the solve if there are multiple stages or something; you’ll need to figure this out depending on how the module itself uses its fields.

### Generating questions

When it finally comes time to generate questions, you can simply create them as follows:

```cs
yield return question(S123Game.Name).Answers(names[name]);
```

In this very basic example, we are asking the question declared in the enum type `S123Game` with the name `Name`, and we’re providing a single correct answer.

You should `yield return` every possible question that Souvenir should be able to ask. Souvenir will pick one at random. You can do this anywhere in the handler, not just after waiting for solve.

*Arithmelogic* provides a more advanced example:

```cs
var screens = new[] { "left", "middle", "right" };
for (var i = 0; i < 3; i++)
	yield return question(SArithmelogic.Numbers, args: [screens[i]])
		.Answers(Enumerable.Range(0, 4).Select(ix => selVal[i][ix].ToString()).ToArray());
```

Things of note here are:

* We are using `args: [...]` to pass the arguments that should be plugged in for `{1}`, `{2}` etc. in the question.
* We are passing an array of strings into `.Answers()` because this question can have multiple possible correct answers. Souvenir will automatically ensure that only one of them appears when the question is asked.
* If one of the arguments is an ordinal (e.g., “first” as in “the first stage”), please use the provided function `Ordinal(...)` to generate the correct string so that this will automatically be translated into other languages.

The `.Answers(...)` function can take the following parameters:

* The correct answer or answers is/are always the first parameter.
* Use `preferredWrong: [...]` to specify some wrong answers that Souvenir should include with higher likelihood. For example, in a module with multiple stages, include the answers from the other stages here. It’s OK for this to include correct answers; Souvenir will sift those out.
* Use `all: [...]` to specify all possible answers. You only need to do that if you haven’t already specified all possible answers in the `[SouvenirQuestion(...)]` attribute and you haven’t specified an `AnswerGenerator`.
* All of these can be either `string[]` (for text answers), `AudioClip[]`, `Sprite[]`, or `Coord[]` (auto-generated sprites for grid-like structures, e.g. mazes).
* For text answers, if you specified `Type = AnswerType.DynamicFont` in the question attribute, use `info: new(...)` to provide the font and font texture.

### What if there are no questions

If, for any reason, Souvenir cannot generate any questions for a given module, you *must* `yield return legitimatelyNoQuestion(module, "...")` and provide a reason. The following example is from *Langton's Anteater*:

```cs
yield return legitimatelyNoQuestion(module, "the module generated 25 cells of the same colour.");
```

### Using sprites or audio clips as answers

There are a few different ways to create a question that uses sprites or audio clips as answers:

* **Import the sprites/audio from the target module.**
	* The target module often has a field of type `Sprite[]`/`AudioClip[]` that you can just read out. As an example, *Module Maze* uses this to show module icons as answers.
	* For audio clips not available in this way, you may need to use `Sounds.GetForeignClip` (example: *Thirty Dollar Module*).
	* In your call to `.Answers(...)`, in addition to passing the correct answer sprite(s)/audio clip(s) as usual, make sure to also pass the full list of all possible sprites/audio clips using `all:`.
* **Use `Coord` objects** if the module is just an n×m rectilinear grid. This will automatically be converted to sprites that represent a grid with one cell highlighted. Look at *Gridlock* for an example.
* **Generate the sprites/audio clips on the fly at runtime.**
	* For grids, you can use `Sprites.GenerateGridSprite` (example: *cRule*).
	* For rectilinear arrangements of circles, you can use `Sprites.GenerateCirclesSprite` (example: *Color Braille*).
	* You could use these as inspiration to make your own sprite generator.
	* For audio, you can look at *Audio Morse* or *Dialtones* for inspiration.
* **Include the sprites/audio clips in the Souvenir mod itself.** If at all possible, this should only be used if the other options are not available, as it means the sprites/audio clips are shipped with Souvenir, increasing the size of the mod. If you do decide for this option, make sure to:
	* Declare a field of type `Sprite[]`/`AudioClip[]` and assign all the sprites/audio clips you need using the Unity editor. For example, `ArithmelogicSprites` contains the sprites for one of the Arithmelogic questions.
	* Please sort the field alphabetically among the others starting in `SouvenirModule.cs` line 30 (sprites)/line 88 (audio).
	* In the `[SouvenirQuestion(...)]` attribute, use `Type = AnswerType.Sprites`/`Type = AnswerType.Audio` and specify either `SpriteFieldName = "..."` or `AudioFieldName = "..."` (insert the name of the new field you just added).
	* Now you can simply use these sprites/audio clips in your call to `.Answers(...)`.

## Full list of options

### `[SouvenirQuestion(...)]`

* **`Arguments`** — Specifies a set of possible arguments to be inserted in place of `{1}`, `{2}`, etc. If an argument must be translated into other languages, this must list all possible values for that argument (e.g. don’t just include “top-left” and “top-right” if there is also a “bottom-left” and a “bottom-right”).
* **`ArgumentGroupSize`** — Number of arguments (not counting the `{0}` for the module name).
* **`TranslateArguments`** — Array indicating which arguments need translating into other languages.
* **`TranslateAnswers`** — Specifies whether the answers need translating into other languages.
* **`TranslatableStrings`** — Additional strings that can be translated into other languages.
* **`UsesQuestionSprite`** — Specifies whether the question uses a question sprite (a small image to the right of the question).
* **`ExampleAnswers`** — Specifies a set of example answers for the purpose of showing the question in TestHarness. Use this only if you didn’t already specify all possible answers and you didn’t use an `AnswerGenerator`. Include at least as many example answers as there are displayed answers as specified by the layout.
* **`Type`** — An `AnswerType` value specifying either `AudioClip`, `Sprites`, `DynamicFont`, or a Souvenir-provided font.
* **`FontSize`** — Font size to use when displaying text answers. Only to be used when using a non-default font.
* **`CharacterSize`** — Character size to use when displaying text answers. Only to be used when using a non-default font.
* **`ForeignAudioID`** — Specify the mod ID if using audio clips from another module (e.g. *Module Listening*). Note that this is *not* the module ID. This is the ID specified in `modInfo.json` and used as the name for a mod's `.dll` file. It is specified in `Assets/Editor/Resources/ModConfig.asset`. This only works if all of the `AudioClip`s can be obtained via reflection. If not, use an `AudioFieldName` instead. Use the special constant `Sounds.Generated` if the audio clips are generated at runtime (e.g. *Audio Morse*). Omit if audio clips are provided by Souvenir.
* **`AudioSizeMultiplier`** — Visually scales waveforms for audio clips. A value of `1f` would be appropriate for constantly peaking audio. Increase this to achieve better visuals. The default value is `2f`.
* **`IsEntireQuestionSprite`** — If `true`, specifies that the entire question is a sprite that fills the whole question area, replacing the question text entirely (example: *Fuse Box*). In this case, the question text will still appear in the logging.
* **`SpriteFieldName`**, **`AudioFieldName`** — If the answers are sprites or audio clips, it is preferable if you can obtain the sprites/audio from the module so that Souvenir doesn’t have to include a copy of them; however, in cases where this is unavoidable, declare a field of type `Sprite[]`/`AudioClip[]` (please sort it alphabetically among the others starting in `SouvenirModule.cs` line 30 (sprites)/line 88 (audio)), assign the sprites/audio clips in the Unity editor, and then specify the name of that field here.

### `[SouvenirDiscriminator(...)]`

This is used to create a *discriminator* that can be used to distinguish between multiple instances of the same module on the bomb. Ordinarily, if there are two *Connection Check* modules, Souvenir might ask about “the Connection Check you solved first/second/etc.”, but this can be used to phrase other ways of distinguishing them, such as “the Connection Check with no 2s”.

```cs
[SouvenirDiscriminator("the Connection Check with no {0}s", Arguments = ["1", "2", "3", "4", "5", "6", "7", "8"], ArgumentGroupSize = 1)]
NoNs
```

Since questions about boss modules must be asked before they can be solved, this is **required for boss modules.**

```cs
[SouvenirDiscriminator("the Forget Me Not which displayed a {0} in the {1} stage", Arguments = ["1", QandA.Ordinal, "2", QandA.Ordinal], ArgumentGroupSize = 2)]
Discriminator
```

* The first parameter is the discriminator phrasing. Note that this time, arguments start counting at `{0}`.
* **`Arguments`**: Specifies a set of possible arguments to be inserted in place of `{0}`, `{1}`, etc. If an argument must be translated into other languages, this must list all possible values for that argument (e.g. don’t just include “top-left” and “top-right” if there is also a “bottom-left” and a “bottom-right”).
* **`ArgumentGroupSize`**: Number of arguments (including `{0}`).
* **`TranslateArguments`**: Array indicating which arguments need translating into other languages.
* **`TranslatableStrings`**: Additional strings that can be translated into other languages.
* **`UsesQuestionSprite`**: Specifies whether the discriminator uses a question sprite (a small image to the right of the question). Souvenir will never pair a question that uses a question sprite with a discriminator that also uses a question sprite, so if all of your questions use a question sprite, you’ll want a discriminator without one (and vice versa).

### `question(...)` (to be used with `yield return`)

There are two overloads to this function. The first one is used when your question appears as text on the module (almost all questions):

* The first parameter is the enum value that defines the question (for example, `S3DMaze.Markings` in the example at the top).
* **`args: [...]`** — the set of arguments to be inserted in place of `{1}`, `{2}`, etc. in the question text. Please use `Ordinal(...)` for ordinal numbers (such as “first” as in “the first stage”).
* **`questionSprite: ...`** — a sprite to be displayed beside the question. These are often used to point at a specific element on the module (e.g. a button in *Ordered Buttons*) but they can also use graphics from the module (e.g. *Simon Signals*).
* **`questionSpriteRotation: ...`** — can be used to display the question sprite at an angle. Currently only used by *Simon Subdivides*.

The second one is used when the entire question is a sprite, which is done to adhere to the theming of “non-verbal” modules such as *❖* and *Fuse Box*:

* The first parameter is the enum value that defines the question (for example, `SFuseBox.Flashes`).
* **`entireQuestionSprite: ...`** — specifies the sprite to be used as the entire question.
* **`args: [...]`** — the set of arguments to be inserted in place of `{1}`, `{2}`, etc. in the question text. Although this doesn’t show up on the module during gameplay, please be mindful to include this to ensure useful logging.

### `.AvoidDiscriminators(...)` (optionally to be added after `question(...)`)

Use this to ensure that the question and the discriminator don’t refer to the same information (for example, “What was the digit in the first stage of the Forget Me Not which displayed a 0 in the first stage?”). You can specify **either** one or more enum values that define the discriminator (e.g. `SConnectionCheck.NoNs`), **or** one or more strings that correspond to the second parameter (`id`) of the `new Discriminator(...)` call (see section below).

### `.Answers(...)` (to be used after `question(...)`)

* The first parameter is either a single correct answer or an array of correct answers. These can be `string`, `AudioClip`, `Sprite` or `Coord`.
* **`preferredWrong: [...]`** — specifies some wrong answers that Souvenir should include with higher likelihood. For example, in a module with multiple stages, include the answers from the other stages here. It’s OK for this to include correct answers; Souvenir will sift those out.
* **`all: [...]`** — specifies all possible answers. You only need to include this if you haven’t already specified all possible answers in the `[SouvenirQuestion(...)]` attribute and you haven’t specified an `AnswerGenerator`.
* **`info: new(...)`** — specifies font information. Valid only for text answers, and only if the question has `Type = AnswerType.DynamicFont`.
	* **`font:`** — specifies the font (to be obtained from the module). Required for `Type = AnswerType.DynamicFont`.
	* **`fontTexture:`** — specifies the font texture (to be obtained from the module). Required for `Type = AnswerType.DynamicFont`.
	* **`raiseBy:`** — optional. Use this in cases where the dynamic font’s baseline does not align with the answer boxes on Souvenir. Currently only used by *Encrypted Maze*, which uses a font containing the symbols.

### `new Discriminator(...)` (to be used with `yield return`)

* The first parameter (`discriminator`) is the enum value that defines the discriminator (for example, `SConnectionCheck.NoNs`).
* The second parameter (`id`) is a string that internally identifies this discriminator. This must be unique for every discriminator generated. For example, if you have a discriminator such as “the module that had an X in the nth stage”, you can use `$"stage{n}"`.
* The third parameter (`value`) is the value that must be unique among the modules that the discriminator is here to distinguish. For example, if you have a discriminator such as “the module that had an X in the nth stage”, this would have to be `"X"`.
* **`args = [...]`** — specifies arguments to be inserted in place of `{0}`, `{1}`, etc. in the discriminator text. Please use `Ordinal(...)` for ordinal numbers (such as “first” as in “the first stage”).
* **`questionSprite: ...`** — a sprite to be displayed beside the question. Souvenir will not pair a question that uses a question sprite with a discriminator that uses a question sprite, so if all of your discriminators use a question sprite, make sure that you have a question without one (and vice-versa).
* **`questionSpriteRotation: ...`** — can be used to display the question sprite at an angle.
* **`avoidAnswers: [...]`** — can be used to ensure that the discriminator is not paired with a question that displays the specified answer(s), whether it is a wrong answer or the correct answer. For example, *Hinges* uses this to ensure that the discriminator doesn’t accidentally reveal the answer to the question. However, in most cases `.AvoidDiscriminators(...)` should be used on the question instead.

Every applicable discriminator *must* be yield-returned even if the current handler can't use it (e.g. because of a strike).

#### Discriminator priorities

It is possible to give some discriminators priority over others. This should generally be used sparingly, but it is currently used for great effect by the following modules:

* In *Concentation*, it will prefer to use the initial positions of numbers that have been moved, rather than ones that haven’t.
* In *Kugelblitz*, it will prefer to disambiguate just by color if it can, and only use the numbers of linked Kugelblitzes if it can’t.
* In *Variety*, it makes it so that if we’re asking about the colors of the LED on a Variety, it will prefer to say “the Variety that has one” if it can, and only use “the Variety that has a maze” if it can’t.

The priority is assigned as a property after the constructor call. Example from *Kugelblitz*:

```cs
yield return new Discriminator(SKugelblitz.Links, "colorlink", ...) { Priority = 1 };
```

When not specified, the priority defaults to `0`. Note that a **lower** value results in **greater** priority: discriminators with priority `0` are considered first, then `1`, then `2`, etc. You do not need to ensure the numbers are consecutive.

You can also have the arguments and priority depend on the question being asked by supplying a lambda. Example from *Variety*:

```cs
yield return new Discriminator(SVariety.Has, "led")
{
	ArgumentsFromQuestion = q => SVariety.LED.Equals(q) ? ["one"] : ["an LED"],
	PriorityFromQuestion = q => SVariety.LED.Equals(q) ? 0 : 1
};
```

In this example, if the question is about the LED, the discriminator will say “the Variety that has one” and the priority is 0. If any other question is asked, the text will be “the Variety that has an LED” and the priority is 1.

If a discriminator becomes invalid at runtime for a particular module (usually upon a strike), it should still be `yield return`ed but with `AvoidEntirely = true`:

```cs
yield return new Discriminator(...) { AvoidEntirely = true };
```

## Reflection helpers

Souvenir uses reflection to access information on other modules on the bomb. There are several types and methods in order to help with this.

### Getting members

These functions obtain a reference to a member (a field, property or method). Note that this is **not** the same as *retrieving the value* stored in a field or *invoking* a method (you do that separately). If you’re going to access a member multiple times, please only obtain the reference once.

#### Fields

To get an **instance field or property** (not static) called `name` from `target`:

```cs
GetField<T>(object target, string name, bool isPublic = false)

GetIntField(object target, string name, bool isPublic = false)

GetArrayField<T>(object target, string name, bool isPublic = false)

GetListField<T>(object target, string name, bool isPublic = false)

GetProperty<T>(object target, string name, bool isPublic = false)
```

To get a **static field or property** from `targetType`:

```cs
GetStaticField<T>(Type targetType, string name, bool isPublic = false)

GetStaticProperty<T>(Type targetType, string name, bool isPublic = false)
```

---

In `GetField` and `GetStaticField`, `T` is the type stored in the field.

In `GetArrayField` and `GetListField`, `T` is the type stored in the collection.

Be sure to use the most applicable method because they make validating data easier (see **Getting values** below).

#### Methods

To get an **instance method** (not static) called `name` with return type `T` from `target`:

```cs
GetMethod<T>(object target, string name, int numParameters, bool isPublic = false)
```

To get an **instance method** (not static) called `name` with return type `void` from `target`:

```cs
GetMethod(object target, string name, int numParameters, bool isPublic = false)
```

To get a static method from `targetType`:

```cs
GetStaticMethod<T>(Type targetType, string name, int numParameters, bool isPublic = false)

GetStaticMethod(Type targetType, string name, int numParameters, bool isPublic = false)
```

---

### Getting values

#### After `GetField`/`GetProperty`

All of the functions in the above section give you an object that has several `Get` methods for retrieving the data stored in a field or property, and validating that data. Each `Get` overload has a corresponding `GetFrom` overload in case you wish to specify a different target. This means you do not need to retrieve the same field reference from several instances of the same type.

```cs
Get(Func<T, string> validator = null, bool nullAllowed = false)

// Properties only
Get(object[] index, Func<T, string> validator = null, bool nullAllowed = false)

GetFrom(object obj, Func<T, string> validator = null, bool nullAllowed = false)
```

* `validator` — provide a function that returns `null` when the data is valid, otherwise a string explaining why it is invalid. Example:

	```cs
	var fldMyValue = GetField<string>(comp, "_myValue");
	var myValue = fldMyValue.Get(validator: x => x.length > 5 ? "expected length <= 5" : null);
	```

	You should *always* use validators when applicable, since they tend to provide much nicer error messages than some error later down the line would.

* `index` — the index to specify for indexed properties.

#### After `GetIntField`

```cs
Get(int? min = null, int? max = null)
```

`min` and `max` are both inclusive.

#### After `GetArrayField`/`GetListField`

```cs
// This returns either T[] or List<T> depending on the type
Get(int expectedLength, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<TElement, string> validator = null)

Get(int minLength, int? maxLength = null, bool nullArrayAllowed = false, bool nullContentAllowed = false, Func<TElement, string> validator = null)
```

Here, `validator` is used to check each individual element. You can still use the standard overload above to run a validator against the collection object itself, but this will always reject a collection with any `null` elements.

### Setting values

You can also set fields and properties to new values using `.Set()` or `.SetTo()`. This should be used very sparingly as it means you’re making a change to the target module instead of just reading out its info. Only do this if you know what you’re doing.

### Invoking methods

```cs
Invoke(params object[] arguments)
InvokeOn(object target, params object[] arguments)
```
