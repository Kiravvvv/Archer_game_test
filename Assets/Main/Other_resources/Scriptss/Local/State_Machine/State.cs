
    public abstract class State
    {
        protected Game_character_abstract Character_script;
        protected StateMachine State_Machine_script;

        protected State(Game_character_abstract character, StateMachine stateMachine)
        {
            Character_script = character;
            State_Machine_script = stateMachine;
        }

    /// <summary>
    /// Вход состояния (начало реализации)
    /// </summary>
        public virtual void Enter_state()
        {

        }

    /// <summary>
    /// Блок относящийся к задаванию управления (в основном от игрока)
    /// </summary>
        public virtual void Handle_Input()
        {

        }

    /// <summary>
    /// Логика поведения этого состояния
    /// </summary>
        public virtual void Logic_Update()
        {

        }

    /// <summary>
    /// Физика и методы которые к ней относятся (например передвижение)
    /// </summary>
        public virtual void Physics_Update()
        {

        }


    /// <summary>
    /// Выход из состояния
    /// </summary>
        public virtual void Exit_state()
        {

        }
    }
