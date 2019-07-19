using Framework.DomainModel.BuildingBlocks.ValueObjects;

namespace EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects
{
    public class NationalCode : ValueObject
    {
        /// <inheritdoc />
        public NationalCode(string code)
        {
            Code = code;
        }

        public string Code { get; private set; }

        /// <inheritdoc />
        public override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}