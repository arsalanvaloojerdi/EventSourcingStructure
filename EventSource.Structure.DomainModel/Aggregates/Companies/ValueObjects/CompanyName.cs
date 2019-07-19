using Framework.DomainModel.BuildingBlocks.ValueObjects;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects
{
    public class CompanyName : ValueObject
    {
        /// <inheritdoc />
        public CompanyName(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        /// <inheritdoc />
        public override void Validate() => throw new System.NotImplementedException();
    }
}