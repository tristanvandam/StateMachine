namespace StateMachine.Core
{
    public abstract class Actions
    {
        public int Id { get; }
        public string Name { get; }
        protected Actions(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}