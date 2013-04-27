namespace SafetyProgram.Base
{
    public abstract class ExtendedICommand<T> : BaseICommand
    {
        protected readonly T data;

        public ExtendedICommand(T data)
        {
            this.data = data;
        }
    }
}
