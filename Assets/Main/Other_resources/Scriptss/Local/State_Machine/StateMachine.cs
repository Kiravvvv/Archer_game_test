
    public class StateMachine
    {
        public State Current_State { get; private set; }

    /// <summary>
    /// Стартовое состояние
    /// </summary>
    /// <param name="starting_State">Состояние</param>
        public void Initialize(State starting_State)
        {
        Current_State = starting_State;
        starting_State.Enter_state();
        }



    /// <summary>
    /// Сменить состояние на другоие
    /// </summary>
    /// <param name="new_State">новое состояние</param>
        public void Change_State(State new_State)
        {
        Current_State.Exit_state();

        Current_State = new_State;
        new_State.Enter_state();
        }

    }
