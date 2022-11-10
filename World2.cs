using Godot;
using System;

public class World2 : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Vector2 PlayerPosition = new Vector2(); 
	Vector2 EnemyPosition = new Vector2(); 
	PackedScene _enemieScene = (PackedScene)GD.Load("res://Skeleton2.tscn");
	KinematicBody2D Player;
	int count = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<KinematicBody2D>("Player");		
		GD.Print(Player.Position);
		
	}
	
	private void _on_Timer_timeout()
	{
		count++;
		if(count < 10){
			KinematicBody2D enemy = (KinematicBody2D)_enemieScene.Instance();
			PlayerPosition = Player.Position;
			EnemyPosition = enemy.Position;
			EnemyPosition.y = PlayerPosition.y;
			EnemyPosition.x = PlayerPosition.x - 10;
			enemy.Position = EnemyPosition;
			AddChild(enemy);
			GD.Print(enemy.Position);
			GD.Print(Player.Position);
		}
		
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

