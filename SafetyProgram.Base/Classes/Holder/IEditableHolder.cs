namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an editable holder. An IHolder where the content may be changed.
    /// Subscribers to the holder will be notified when the content is changed.
    /// </summary>
    /// <typeparam name="TContent">The type of content held by the editable holder.</typeparam>
    public interface IEditableHolder<TContent> : IHolder<TContent>
    {
        /// <summary>
        /// Get or set the content of the holder.
        /// </summary>
        new TContent Content { get; set; }
    }
}
