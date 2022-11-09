using Godot;
using System;

public class Score : CanvasLayer
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Label score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		score = GetNode<Label>("Label");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	 	score.Text = "Score " + GlobalVariable.score;
	}
}
