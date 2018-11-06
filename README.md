# UnityReorderableListExamples
Two very simple barebones examples of how to use Reorderable Lists in Editor scripts. Both examples show how to setup and user ReorderableList with custom GUI in Editor scripts using the various callbacks provided.
* [ReorderableListExample01.cs](https://github.com/Demkeys/UnityReorderableListExamples/blob/master/ReorderableListExample01.cs) allows you to add items at the end of the list.
* [ReorderableListExample02.cs](https://github.com/Demkeys/UnityReorderableListExamples/blob/master/ReorderableListExample02.cs) allows you to add items after the currently selected item, and also add items at the end of the list.

In both examples you can reorder and edit the items. There's also a button that prints out the names from the reordered list, as an example to show that the reodered list can be used after reordering.

If you need more info, you can take a look at the [ReorderableList](https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/GUI/ReorderableList.cs) class code from the Unity Cs Reference repo.
