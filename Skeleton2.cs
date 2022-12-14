using Godot;
using System;

public class Skeleton2 : KinematicBody2D
{
	Vector2 MyScale = new Vector2(1, 1);
	int direction = 1;
	bool direction_flip = false;
	
	private enum state{
		IDLE,
		WALK,
		ATTACK,
		HURT,
		DEATH
	};
	
	private int life = 1;
	int collision = 0;
	private state currentState = state.WALK;
	
	bool facing_right = true; 
   	const int GRAVITY = 20;
	const int MAXFALLSPEED = 200;
	const int MAXSPEED = 40;
	public Vector2 Velocity;
	
	bool is_player_nearby = false;
	KinematicBody2D player;
	
	KinematicBody2D skeleton;
	Sprite sprite;
	AnimationPlayer animationPlayer;
	AnimationTree animationTree;
	AnimationNodeStateMachinePlayback animationState;
	RayCast2D wallDetector;
	RayCast2D wallDetector2;
	CollisionShape2D _SwordRight;
	CollisionShape2D _SwordLeft;
	CollisionShape2D _Hurt;
	
	public override void _Ready()
	{
		Velocity = Vector2.Zero;
		sprite = GetNode<Sprite>("Sprite");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		wallDetector = GetNode<RayCast2D>("Wall");
		wallDetector2 = GetNode<RayCast2D>("Wall2");
		skeleton = GetNode<KinematicBody2D>("Skeleton");
		_SwordRight = GetNode<CollisionShape2D>("AttackRight/AttackRightBox");
		_SwordLeft = GetNode<CollisionShape2D>("AttackLeft/AttackLeftBox");
		_Hurt = GetNode<CollisionShape2D>("HurtBox/CollisionShape2D");
		
		_SwordRight.Disabled = true;
		_SwordLeft.Disabled = true;
		Walk_enter();
	}
	
	
	
	public void _on_PlayerDetector_body_entered(object body)
	{
		if(body.GetType().Name.ToString() == "Player"){
			is_player_nearby = true;
			player = (KinematicBody2D)body;
			Attack_enter();
		}
	}
	
	private void _on_HurtBox_area_entered(Area2D area)
	{
		GD.Print(area.GetType().Name.ToString());
		if(area.IsInGroup("Spell")){
			Hurt();
		}
	}


	public void _on_PlayerDetector_body_exited(object body)
	{
		is_player_nearby = false;
		player = null;
		Walk_enter();
	}
	
	private void Hurt(){
		currentState = state.HURT;
		animationState.Start("Hit");
		life--;
		if(life == 0){
			Death();
		}
	
	}
	
	private void Hurt_End(){
		currentState = state.WALK;
		animationState.Start("Walk");
	}
	
	private void Death(){
		currentState = state.DEATH;
		animationState.Start("Death");
		GlobalVariable.score += 1;
		GD.Print(GlobalVariable.score);
		_Hurt.Disabled = true;
		
	}
	
	private void Death_End(){
		QueueFree();
	}

	public override void _PhysicsProcess(float delta)
	{
		if (facing_right) {
			sprite.FlipH = false;
		} else {
			sprite.FlipH = true;
		}
		
		_SwordRight.Disabled = true;
		_SwordLeft.Disabled = true;
		
		switch (currentState){
			case state.WALK:
				Detect_direction_change();
				Walk(delta);
				break;
			case state.ATTACK:
				Attack(delta);
				break;	
		};
		
	}
	
	public void Walk(float delta){
		if(direction_flip){
			direction_flip = false;
		}
		
		Velocity.y += GRAVITY;

		if(Velocity.y > MAXFALLSPEED) {
			Velocity.y = MAXFALLSPEED;
		}
		Velocity.x = MAXSPEED * direction;
		
		Velocity = MoveAndSlide(Velocity, Vector2.Up);
	
	}
	
	public void Attack(float delta){
		if(is_player_nearby){
			if(facing_right){
			_SwordRight.Disabled = false;
		}else{
			_SwordLeft.Disabled = false;
		}
			animationState.Travel("Attack");
		}
	
	}
	
	public void Detect_direction_change(){
		if(IsOnFloor()){
			if((wallDetector.IsColliding())  || (wallDetector2.IsColliding())){
				flip_direction();
				collision++;
				if(collision%2 == 0){
					facing_right = true;
				}else{
					facing_right = false;
				}
			}
		}
	}
	
	public void flip_direction(){
		direction *= -1;
		direction_flip = true;
		
	}
	
	public void Walk_enter(){
		animationState.Travel("Walk");
		currentState = state.WALK;
	}
	
	public void Attack_enter(){
		if(facing_right){
			_SwordRight.Disabled = false;
		}else{
			_SwordLeft.Disabled = false;
		}
		animationState.Travel("Attack");
		currentState = state.ATTACK;
	}

}

