namespace Framework.DomainModel.BuildingBlocks.Entities
{
    public abstract class EntityBase<TKey>
    {
        /// <inheritdoc />
        protected EntityBase(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;

            var entity = obj as EntityBase<TKey>;

            // ReSharper disable once PossibleNullReferenceException
            return this.Id.Equals(entity.Id);
        }
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return this.Id.GetHashCode();
        }
    }
}
