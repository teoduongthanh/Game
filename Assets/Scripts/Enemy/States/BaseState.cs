public abstract class BaseState
{
    //trạng thái của kẻ thù của Enemy class
    public Enemy enemy;
    //trạng thái của Máy(Boss) StateMachine
    public StateMachine stateMachine;

    // 3 phương thức sử dung chính và sẽ được overiding 
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}