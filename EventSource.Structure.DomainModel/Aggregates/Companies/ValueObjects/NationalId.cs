using Framework.DomainModel.BuildingBlocks.ValueObjects;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects
{
    public class NationalId : ValueObject
    {
        /// <inheritdoc />
        public NationalId(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        /// <inheritdoc />
        public override void Validate() => throw new System.NotImplementedException();
    }
}