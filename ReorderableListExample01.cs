using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class ReorderableListExample01 : EditorWindow {

	ReorderableList rl;
	List<MyClass01> someList;
	Vector2 scrollPos = Vector2.zero;

	[MenuItem("MyTools/ReorderableListExample01")]
	static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<ReorderableListExample01>();
		window.Show();
	}

	void OnEnable()
	{
		someList = new List<MyClass01>();
		// Add some data to the list
		someList.Add(new MyClass01() { Name = "John", Role = "2D Artist" });
		someList.Add(new MyClass01() { Name = "Jane", Role = "Engine Developer" });
		someList.Add(new MyClass01() { Name = "Max", Role = "Music Composer" });

		rl = new ReorderableList(someList, typeof(MyClass01));
		rl.elementHeight += 5;

		// Add callback methods
		rl.onAddCallback += rlAddCallback;
		rl.drawElementCallback += rlDrawElementCallback;
		rl.onSelectCallback += rlSelectCallback;
		rl.drawHeaderCallback += rlDrawHeaderCallback;
		rl.onChangedCallback += rlOnChangedCallback;
		rl.onAddCallback += rlOnAddCallback;
		rl.onRemoveCallback += rlOnRemoveCallback;
	}

	void OnDisable()
	{
		// Remove callback methods
		rl.onAddCallback -= rlAddCallback;
		rl.drawElementCallback -= rlDrawElementCallback;
		rl.onSelectCallback -= rlSelectCallback;
		rl.drawHeaderCallback -= rlDrawHeaderCallback;
		rl.onChangedCallback -= rlOnChangedCallback;
		rl.onAddCallback -= rlOnAddCallback;
		rl.onRemoveCallback -= rlOnRemoveCallback;
	}

	void OnGUI()
	{
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Reorderable List Example 1", 
		new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontSize = 15, fontStyle = FontStyle.Bold });
		EditorGUILayout.Space();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		rl.DoLayoutList();
		EditorGUILayout.EndScrollView();

		// If you want to print out the names from the reordered list.
		// This is to show that the list can be used after reodering.
		if(GUILayout.Button("Print list names"))
		{
			string s = "";
			for(int i = 0; i < rl.list.Count; i++)
			{
				MyClass01 mc = (MyClass01)rl.list[i];
				s += i < rl.list.Count-1 ? mc.Name + ", " : mc.Name + ".";
			}
			Debug.Log(s);
		}

	}

	void rlOnAddCallback(ReorderableList _rl)
	{
		// Uncomment the below line to display message when Item is added.
		// Debug.Log("New item added");
	}

	void rlOnChangedCallback(ReorderableList _rl)
	{
		// Uncomment the below line to display message when list has changed.
		// Debug.Log("List changed");
	}

	void rlOnRemoveCallback(ReorderableList _rl)
	{
		// Uncomment the below line to display message when Item is removed.
		// Debug.Log("Item #" + _rl.index.ToString() + " removed.");
		
		// This line is required because when you add a method to the OnRemoveCallback, clicking the remove
		// button doesn't remove items anymore. So we have to add the functionality manually.
		_rl.list.RemoveAt(_rl.index); 
	}

	void rlDrawHeaderCallback(Rect myRect)
	{
		EditorGUI.LabelField(myRect, "Team Members");
	}

	void rlDrawElementCallback(Rect myRect, int index, bool isActive, bool isFocused)
	{
		EditorGUI.LabelField(new Rect(myRect.x, myRect.y + 5, 40, 40), index.ToString());
		EditorGUI.LabelField(new Rect(myRect.x + 30, myRect.y + 5, 100, 40), someList[index].Name);

		if(isActive)
		{
			someList[index].Name = EditorGUI.TextArea(new Rect(myRect.x + 30, myRect.y + 5, 100, 15), 
				someList[index].Name);
			someList[index].Role = GUI.TextArea(new Rect(myRect.x + 150, myRect.y + 5, 400, 15),
				someList[index].Role);
		}
		else
		{
			EditorGUI.LabelField(new Rect(myRect.x + 30, myRect.y + 5, 100, 15), 
				someList[index].Name);
			EditorGUI.LabelField(new Rect(myRect.x + 150, myRect.y + 5, 400, 15),
				someList[index].Role);
		}
	}

	void rlSelectCallback(ReorderableList _rl)
	{
		// Uncomment the below line to display message when Item is selected.
		// Debug.Log("Item #" + _rl.index.ToString() + " selected.");
	}

	void rlAddCallback(ReorderableList _rl)
	{
		_rl.list.Add(new MyClass01() { IndexNo = _rl.count, Name = "SomeName", Role = "SomeRole" });
		_rl.index = _rl.list.Count-1;
	}

}

[System.Serializable]
public class MyClass01
{
	public int IndexNo = 1;
	public string Name = "";
	public string Role = "";
}
