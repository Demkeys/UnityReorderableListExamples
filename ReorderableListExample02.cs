using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class ReorderableListExample02 : EditorWindow {

	ReorderableList rl;
	List<MyClass02> someList;
	Vector2 scrollPos = Vector2.zero;
	GenericMenu dropDownMenu;

	[MenuItem("MyTools/ReorderableListExample02")]
	static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<ReorderableListExample02>();
		window.Show();
	}

	void OnEnable()
	{
		someList = new List<MyClass02>();
		// Add some data to the list
		someList.Add(new MyClass02() { Name = "John", Role = "2D Artist" });
		someList.Add(new MyClass02() { Name = "Jane", Role = "Engine Developer" });
		someList.Add(new MyClass02() { Name = "Max", Role = "Music Composer" });

		rl = new ReorderableList(someList, typeof(MyClass02));
		rl.elementHeight += 5;

		// Add callback methods
		rl.onAddCallback += rlAddCallback;
		rl.drawElementCallback += rlDrawElementCallback;
		rl.onSelectCallback += rlSelectCallback;
		rl.drawHeaderCallback += rlDrawHeaderCallback;
		rl.onChangedCallback += rlOnChangedCallback;
		rl.onAddCallback += rlOnAddCallback;
		rl.onRemoveCallback += rlOnRemoveCallback;
		rl.onAddDropdownCallback += rlOnAddDropdownCallback;

		dropDownMenu = new GenericMenu();

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
		rl.onAddDropdownCallback -= rlOnAddDropdownCallback;
	}

	void OnGUI()
	{
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Reorderable List Example 2", 
		new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontSize = 15, fontStyle = FontStyle.Bold });
		EditorGUILayout.Space();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		rl.DoLayoutList();
		EditorGUILayout.EndScrollView();

		dropDownMenu = new GenericMenu();
		
		// If an item is selected in the list, make 'Add Next' and 'Add Last' options available
		if(rl.index >= 0)
		{
			dropDownMenu.AddItem(new GUIContent("Add Next"), false, AddNext, rl);
			dropDownMenu.AddItem(new GUIContent("Add Last"), false, AddLast, rl);
		}
		// If no item is selected in the list, disable 'Add Next' option
		else
		{
			dropDownMenu.AddDisabledItem(new GUIContent("Add Next"), false);
			dropDownMenu.AddItem(new GUIContent("Add Last"), false, AddLast, rl);
		}

		// If you want to print out the names from the reordered list.
		// This is to show that the list can be used after reodering.
		if(GUILayout.Button("Print list names"))
		{
			string s = "";
			for(int i = 0; i < rl.list.Count; i++)
			{
				MyClass02 mc = (MyClass02)rl.list[i];
				s += i < rl.list.Count-1 ? mc.Name + ", " : mc.Name + ".";
			}
			Debug.Log(s);
		}
	}

	void rlOnAddDropdownCallback(Rect myRect, ReorderableList _rl)
	{
		dropDownMenu.DropDown(myRect);
	}

	void AddNext(object userData)
	{
		ReorderableList _rl = (ReorderableList)userData;
		_rl.list.Insert(_rl.index+1, new MyClass02() { Name = "SomeName", Role = "SomeRole" });
		_rl.index = _rl.index+1;
	}

	void AddLast(object userData)
	{
		ReorderableList _rl = (ReorderableList)userData;
		_rl.list.Add(new MyClass02() { Name = "SomeName", Role = "SomeRole" });
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
		_rl.list.Add(new MyClass02() { IndexNo = _rl.count, Name = "SomeName", Role = "SomeRole" });
		_rl.index = _rl.list.Count-1;
	}

}

[System.Serializable]
public class MyClass02
{
	public int IndexNo = 1;
	public string Name = "";
	public string Role = "";
}
