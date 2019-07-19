using Framework.DomainModel.BuildingBlocks.ValueObjects;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects
{
    public class FullName : ValueObject
    {
        /// <inheritdoc />
        public FullName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        /// <inheritdoc />
        public override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}