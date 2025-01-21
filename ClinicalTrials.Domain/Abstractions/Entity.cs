namespace ClinicalTrials.Domain.Abstractions
{
    /// <summary>
    /// Represents the base class from which all entities derive.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public Guid? Id { get; protected set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity(Guid? id)
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}
