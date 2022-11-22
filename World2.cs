using Godot;
using System;

public class World2 : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Vector2 MonsterPortalPosition = new Vector2(); 
	Vector2 EnemyPosition = new Vector2(); 
	PackedScene _enemieScene = (PackedScene)GD.Load("res://Skeleton2.tscn");
	KinematicBody2D Player;
	Sprite MonsterPortal;	
	int count = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<KinematicBody2D>("Player");		
		MonsterPortal = GetNode<Sprite>("MonsterPortal");		
		GD.Print(Player.Position);
		
	}
	
	private void _on_Timer_timeout()
	{
		count++;
		if(count < 15){
			KinematicBody2D enemy = (KinematicBody2D)_enemieScene.Instance();
			MonsterPortalPosition = MonsterPortal.Position;
			EnemyPosition = enemy.Position;
			EnemyPosition.y = MonsterPortalPosition.y;
			EnemyPosition.x = MonsterPortalPosition.x;
			enemy.Position = EnemyPosition;
			AddChild(enemy);
			GD.Print(enemy.Position);
			GD.Print(Player.Position);
		}
		
	}
	
	private void _on_Portail_body_entered(object body)
	{
		if(body.GetType().Name.ToString() == "Player" && GlobalVariable.score >= 5){
			GetTree().ChangeScene("res://Victory.tscn");
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	 	
	}
}




