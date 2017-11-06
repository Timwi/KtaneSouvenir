using UnityEngine;

public class TestSelectable : MonoBehaviour
{
	public TestHighlightable Highlight;
	public TestSelectable[] Children;
	protected TestSelectableArea _selectableArea;
	public TestSelectableArea SelectableArea
	{
		get
		{
			if (_selectableArea == null)
			{
				if (GetComponent<KMSelectable>() != null && GetComponent<KMSelectable>().SelectableColliders.Length > 0)
				{
					_selectableArea = new GameObject("SelectableArea").AddComponent<TestSelectableArea>();
					_selectableArea.Selectable = this;
					_selectableArea.transform.parent = transform;

					foreach (Collider collider in GetComponent<KMSelectable>().SelectableColliders)
					{
						TestSelectableArea colSelectableArea = collider.gameObject.AddComponent<TestSelectableArea>();
						collider.isTrigger = false;
						collider.gameObject.layer = 11;
						colSelectableArea.Selectable = this;
						_selectableArea.Colliders.Add(collider);
					}

					_selectableArea.DeactivateSelectableArea();
				}

				else if (Highlight != null)
				{
					MeshRenderer meshRenderer = Highlight.gameObject.GetComponent<MeshRenderer>();
					if (meshRenderer == null)
					{
						//Adding a BoxCollider will take on an appropriate size/position based on
						//a MeshRenderer (rather than just a MeshFilter, as it appeared to work in 4.6)
						//Thus, we add a MeshRenderer if needed but immediately disable it.
						meshRenderer = Highlight.gameObject.AddComponent<MeshRenderer>();
						meshRenderer.enabled = false;
					}

					BoxCollider collider = Highlight.gameObject.AddComponent<BoxCollider>();
					collider.isTrigger = true;
					_selectableArea = Highlight.gameObject.AddComponent<TestSelectableArea>();
					_selectableArea.Selectable = this;
					_selectableArea.gameObject.layer = 11;
					_selectableArea.DeactivateSelectableArea();
				}
			}

			return _selectableArea;

		}
	}
	public TestSelectable Parent;

	// Interaction Punch Stuff
	bool animating = false;
	float animationTime = 0;
	float animationLength = 0.75f;
	Vector3 punchAxis;
	float punchAmplitude;

	AnimationCurve punchCurve = new AnimationCurve(new Keyframe[9]
	{
		new Keyframe(0.0f, 0.0f),
		new Keyframe(0.112586f, 0.9976035f),
		new Keyframe(0.3120486f, -0.1720615f),
		new Keyframe(0.4316337f, 0.07030682f),
		new Keyframe(0.5524869f, -0.03141804f),
		new Keyframe(0.6549395f, 0.003909959f),
		new Keyframe(0.770987f, -0.009817753f),
		new Keyframe(0.8838775f, 0.001939224f),
		new Keyframe(1f, 0.0f)
	});

	void Start()
	{
		if (!GetComponent<KMSelectable>()) return;

		GetComponent<KMSelectable>().OnInteractionPunch += delegate (float intensityModifier)
		{
			DoInteractionPunch(intensityModifier, gameObject.transform.position);
		};
	}

	void Update()
	{
		if (animating)
		{
			animationTime += Time.deltaTime;

			if (animationTime <= animationLength)
			{
				Quaternion quaternion = Quaternion.AngleAxis(punchCurve.Evaluate(animationTime / animationLength) * punchAmplitude, punchAxis);// * KTInputManager.Instance.GetControlRotation();
				transform.rotation = quaternion;
			}
			else
			{
				animationTime = 0;
				animating = false;
			}
		}
	}

	public void DoInteractionPunch(float intensityModifier, Vector3 pointOfContact)
	{
		if (Parent && !Parent.GetComponent<TestHarness>()) // Find the top TestSelectable that isn't the TestHarness.
		{
			Parent.DoInteractionPunch(intensityModifier, pointOfContact);
		}
		else
		{
			if (animating) animationTime = 0;

			animating = true;

			punchAmplitude = intensityModifier * 5;

			Vector3 position = transform.position; // This should be the module
			Vector3 lhs = pointOfContact - position;
			lhs.Normalize();
			punchAxis = -Vector3.Cross(lhs, transform.up);
			//punchAxis = -punchAxis;
			
			//this.punchTween = LeanTween.value(this.gameObject, new Action<float>(this.OnPunchTweenUpdate), 0.0f, punchAmplitude, punchDuration).setEase(LeanTweenType.punch).setOnComplete((Action) (() => this.punchTween = (LTDescr) null));

			
		}
	}

	public bool Interact()
	{
		bool shouldDrill = Children.Length > 0;

		if (GetComponent<KMSelectable>().OnInteract != null)
		{
			shouldDrill = GetComponent<KMSelectable>().OnInteract();
		}

		return shouldDrill;
	}

	public void InteractEnded()
	{
		if (GetComponent<KMSelectable>().OnInteractEnded != null)
		{
			GetComponent<KMSelectable>().OnInteractEnded();
		}
	}

	public void Select()
	{
		Highlight.On();
		if (GetComponent<KMSelectable>().OnSelect != null)
		{
			GetComponent<KMSelectable>().OnSelect();
		}
		if (GetComponent<KMSelectable>().OnHighlight != null)
		{
			GetComponent<KMSelectable>().OnHighlight();
		}
	}

	public bool Cancel()
	{
		if (GetComponent<KMSelectable>().OnCancel != null)
		{
			return GetComponent<KMSelectable>().OnCancel();
		}

		return true;
	}

	public void Deselect()
	{
		Highlight.Off();
		if (GetComponent<KMSelectable>().OnDeselect != null)
		{
			GetComponent<KMSelectable>().OnDeselect();
		}
	}

	public void OnDrillAway(TestSelectable newParent)
	{
		DeactivateChildSelectableAreas(newParent);
	}

	public void OnDrillTo()
	{
		ActivateChildSelectableAreas();
	}

	public void ActivateChildSelectableAreas()
	{
		if (this.SelectableArea != null)
		{
			this.SelectableArea.DeactivateSelectableArea();
		}
		for (int i = 0; i < Children.Length; i++)
		{
			if (Children[i] != null)
			{
				if (Children[i].SelectableArea != null)
				{
					Children[i].SelectableArea.ActivateSelectableArea();
				}
			}
		}
	}

	public void DeactivateImmediateChildSelectableAreas()
	{
		for (int i = 0; i < Children.Length; i++)
		{
			if (Children[i] != null)
			{
				if (Children[i].SelectableArea != null)
				{
					Children[i].SelectableArea.DeactivateSelectableArea();
				}
			}
		}
	}

	public void DeactivateChildSelectableAreas(TestSelectable newParent)
	{
		TestSelectable parent = newParent;
		while (parent != null)
		{
			if (parent == this)
				return;
			parent = parent.Parent;
		}

		parent = this;

		while (parent != newParent && parent != null)
		{
			for (int i = 0; i < parent.Children.Length; i++)
			{
				if (parent.Children[i] != null)
				{
					if (parent.Children[i].SelectableArea != null)
					{
						parent.Children[i].SelectableArea.DeactivateSelectableArea();
					}
				}
			}

			parent = parent.Parent;

			if (parent != null && parent == newParent && parent.SelectableArea != null)
			{
				parent.SelectableArea.ActivateSelectableArea();
			}
		}
	}
}
