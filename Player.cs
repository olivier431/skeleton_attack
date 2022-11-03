using Godot;
using System;

public class Player : KinematicBody2D
{
	enum State{
		IDLE,
		RUN,
		JUMP,
		FALL,
		ATTACK,
		DEATH,
		HURT
	}
	
	Vector2 UP = new Vector2(0, -1);
	const int GRAVITY = 20;
	const int MAXFALLSPEED = 200;
	const int MAXSPEED = 100;
	const int JUMPFORCE = 370;
	
	const int ACCEL = 10;
	
	public int life = 100;
	Vector2 dir = new Vector2();
	State currentState = State.IDLE;
	bool facing_right = true;
	Vector2 motion = new Vector2();
	Camera2D _cam;
	Area2D _Hurtbox;
	Sprite sprite;
	AnimationPlayer _animationplayer;
	AnimationTree _animationtree;
	AnimationNodeStateMachinePlayback _statemachine;
	ProgressBar  _ProgressBar;
	CollisionShape2D _SpellRight;
	CollisionShape2D _SpellLeft;
	
	
	public override void _Ready()
	{
		 sprite = GetNode<Sprite>("Sprite");
		_cam = GetNode<Camera2D>("Camera2D");
		_animationtree = GetNode<AnimationTree>("AnimationTree");
		_statemachine = (AnimationNodeStateMachinePlayback)_animationtree.Get("parameters/playback");
		_ProgressBar = (ProgressBar)GetNode("HealthBar/ProgressBar");
		_SpellRight = GetNode<CollisionShape2D>("SpellRight/AttackRightBox");
		_SpellLeft = GetNode<CollisionShape2D>("SpellLeft/AttackLeftBox");
		_cam.Zoom = new Vector2(0.15f, 0.15f);
		
		_SpellRight.Disabled = true;
		_SpellLeft.Disabled = true;
		
	
	}
	
	private void _on_HurtBox_area_entered(Area2D area)
	{
		GD.Print(area.GetType().Name.ToString());
		if(area.IsInGroup("Attack")){
			GD.Print("OK!");
			Hurt();
		}
	}
	
	public Vector2 DIR(){
		var Dir = new Vector2();
		Dir.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		return Dir;
	}
	
	private void idle(){
		if(dir.x != 0){
			currentState = State.RUN;
			_statemachine.Travel("Run");
		}
		else{
			motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
		}
		if (IsOnFloor())
		{
			// On ne regarde qu'un seul fois et non le maintient de la touche
			if (Input.IsActionJustPressed("ui_jump"))
			{
				currentState = State.JUMP;
				_statemachine.Travel("Jump");
			}
		}
		
		if (Input.IsActionJustPressed("Attack1"))
		{
			currentState = State.ATTACK;
			_statemachine.Start("Attack");
		}
	}
	private void Run(){
		if(dir.x > 0){
			sprite.FlipH = false;
			facing_right = true;
		}else if(dir.x < 0){
			sprite.FlipH = true;
			facing_right = false;
		}
		motion.x += ACCEL * dir.x;
		motion.x = motion.Clamped(MAXSPEED).x;
			// On ne regarde qu'un seul fois et non le maintient de la touche
		if (Input.IsActionJustPressed("ui_jump"))
		{
			currentState = State.JUMP;
			_statemachine.Travel("Jump");
		}
	
		if(dir.x == 0){
			currentState = State.IDLE;
			_statemachine.Travel("Idle");
		}
		
		if(motion.y > 0){
			currentState = State.FALL;
			_statemachine.Travel("Fall");
		}
		
		if (Input.IsActionJustPressed("Attack1"))
		{
			currentState = State.ATTACK;
			_statemachine.Start("Attack");
		}
	}
	
	private void Jump(){
		if(IsOnFloor()){
			motion.x = Mathf.Lerp(motion.x, MAXSPEED * motion.x > 0 ? 1 : -1, (ACCEL * 1f) / MAXSPEED);
			motion.y = -JUMPFORCE;
		}
		if(motion.y > 0){
			currentState = State.FALL;
			_statemachine.Start("Fall");
		}
	}
	
	private void Fall(){
		if(dir.x > 0){
			sprite.FlipH = false;
			facing_right = true;
		}else if(dir.x < 0){
			sprite.FlipH = true;
			facing_right = false;
		}
		motion.x += ACCEL * dir.x;
		motion.x = motion.Clamped(MAXSPEED).x;
		
		if(motion.y == 0){
			currentState = State.IDLE;
			_statemachine.Travel("Idle");
		}
		
		if (Input.IsActionJustPressed("Attack1"))
		{
			currentState = State.ATTACK;
			_statemachine.Start("Attack");
		}
	}	
	
	private void Attack(){
		motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
		if(facing_right){
			_SpellRight.Disabled = false;
		}else{
			_SpellLeft.Disabled = false;
		}
	}
	private void End_Attack(){
		currentState = State.IDLE;
		_statemachine.Start("Idle");
		_SpellRight.Disabled = true;
		_SpellLeft.Disabled = true;
	}
	
	private void Hurt(){
		currentState = State.HURT;
		_statemachine.Start("Hurt");
		life = life - 25;
		if(life == 0){
			Death();
		}
		Life_change(life);
	}
	
	private void Hurt_End(){
		currentState = State.IDLE;
		_statemachine.Start("Idle");
	}
	
	private void Death(){
		currentState = State.DEATH;
		_statemachine.Start("Death");
	}
	
	private void End_Death(){
		GD.Print("Death");
		QueueFree();
		GetTree().ChangeScene("res://DeathScene.tscn");
	}
	
	private void Life_change(int life){
		_ProgressBar.Value = life;
	}
 	public override void _PhysicsProcess(float delta)
	{
		dir = DIR();
		switch (currentState)
		{
			case State.IDLE:
				idle();
				break;
			case State.RUN:
				Run();
				break;
			case State.JUMP:
				Jump();
				break;
			case State.FALL:
				Fall();
				break;
			case State.ATTACK:
				Attack();
				break;	
		}
		
		motion.y += GRAVITY;
		if (motion.y > MAXFALLSPEED)
		{
			motion.y = MAXFALLSPEED;
		}
		
		motion = MoveAndSlide(motion, UP);

	}
	
}

