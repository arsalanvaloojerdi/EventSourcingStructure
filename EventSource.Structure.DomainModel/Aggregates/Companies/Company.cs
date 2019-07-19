using EventSource.Structure.DomainModel.Aggregates.Companies.Entities;
using EventSource.Structure.DomainModel.Aggregates.Companies.Snapshots;
using EventSource.Structure.DomainModel.Aggregates.Companies.ValueObjects;
using EventSource.Structure.DomainModel.Events.Company;
using Framework.DomainModel.BuildingBlocks.Aggregates;
using Framework.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EventSource.Structure.DomainModel.Aggregates.Companies
{
	public class Company : AggregateRoot<Guid>
	{
		private List<Employee> _employees;

		/// <inheritdoc />
		public Company(CompanyName name, NationalId nationalId) : base(Guid.NewGuid())
		{
			ApplyChange(new CompanyRegistered(this.Id, name.Value, nationalId.Id));
		}

		public CompanyName Name { get; private set; }

		public NationalId NationalId { get; private set; }

		public IReadOnlyCollection<Employee> Employees => new ReadOnlyCollection<Employee>(_employees);

		public void HireEmployee(Employee employee)
		{
			ApplyChange(new EmployeeHired(this.Id, employee.Id, employee.FullName.FirstName, employee.FullName.LastName,
				employee.NationalCode.Code));
		}

		/// <inheritdoc />
		public override void RestoreSnapshot(Snapshot snapshot)
		{
			var snapshotToRestore = (CompanySnapshot)snapshot;

			this.Id = snapshotToRestore.AggregateId;
			this.Name = new CompanyName(snapshotToRestore.Name);
			this.NationalId = new NationalId(snapshotToRestore.NationalId);
			this._employees = snapshotToRestore.Employees.Select(q =>
				new Employee(q.Id, new FullName(q.FirstName, q.LastName), new NationalCode(q.NationalCode))).ToList();
			this.CurrentVersion = snapshotToRestore.Version;
		}

		/// <inheritdoc />
		public override Snapshot TakeSnapshot() => new CompanySnapshot(this);

		// For Persistence
		private Company() : base(Guid.NewGuid()) { }

		#region Appliers

		private void Apply(CompanyRegistered @event)
		{
			this.Id = @event.AggregateId;
			this.Name = new CompanyName(@event.Name);
			this.NationalId = new NationalId(@event.NationalId);
			this._employees = new List<Employee>();
		}

		private void Apply(EmployeeHired @event)
		{
			var employee = new Employee(@event.EmployeeId, new FullName(@event.FirstName, @event.LastName),
				new NationalCode(@event.NationalCode));

			_employees.Add(employee);
		}

		#endregion
	}
}