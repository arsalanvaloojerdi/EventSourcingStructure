using Framework.DomainModel.Core;
using Framework.DomainModel.Equality;

namespace Framework.DomainModel.BuildingBlocks.ValueObjects
{
    public abstract class ValueObject : IValueObject
    {
        public override bool Equals(object obj)
        {
            return EqualsBuilder.ReflectionEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return HashCodeBuilder.ReflectionHashCode(this);
        }

        public abstract void Validate();
    }
}
