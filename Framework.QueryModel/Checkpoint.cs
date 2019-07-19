using System;

namespace Framework.QueryModel
{
	public class Checkpoint
	{
		/// <inheritdoc />
		public Checkpoint(string projectionName, string position)
		{
			Id = Guid.NewGuid();
			ProjectionName = projectionName;
			Position = position;
		}

		public Guid Id { get; set; }

		public string ProjectionName { get; set; }

		public string Position { get; set; }

		private Checkpoint() { }
	}
}