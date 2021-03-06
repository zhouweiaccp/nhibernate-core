﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Exceptions;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;

namespace NHibernate.Test.Component.Basic
{
	using System.Threading.Tasks;
	[TestFixture]
	public class ComponentWithUniqueConstraintTestsAsync : TestCaseMappingByCode
	{
		protected override HbmMapping GetMappings()
		{
			var mapper = new ModelMapper();

			mapper.Component<Person>(comp =>
			{
				comp.Property(p => p.Name, m => m.NotNullable(true));
				comp.Property(p => p.Dob, m => m.NotNullable(true));
				comp.Unique(true); // hbm2ddl: Generate a unique constraint in the database
			});

			mapper.Class<Employee>(cm =>
			{
				cm.Id(employee => employee.Id, map => map.Generator(Generators.HighLow));
				cm.Property(employee => employee.HireDate);
				cm.Component(person => person.Person);
			});

			return mapper.CompileMappingForAllExplicitlyAddedEntities();
		}

		protected override void OnTearDown()
		{
			using (var session = Sfi.OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				session.Delete("from Employee");
				transaction.Commit();
			}
		}

		[Test]
		public async Task CanBePersistedWithUniqueValuesAsync()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var e1 = new Employee { HireDate = DateTime.Today, Person = new Person { Name = "Bill", Dob = new DateTime(2000, 1, 1) } };
				var e2 = new Employee { HireDate = DateTime.Today, Person = new Person { Name = "Hillary", Dob = new DateTime(2000, 1, 1) } };
				await (session.SaveAsync(e1));
				await (session.SaveAsync(e2));
				await (transaction.CommitAsync());
			}

			using (var session = OpenSession())
			using (session.BeginTransaction())
			{
				var employees = await (session.Query<Employee>().ToListAsync());
				Assert.That(employees.Count, Is.EqualTo(2));
				Assert.That(employees.Select(employee => employee.Person.Name).ToArray(), Is.EquivalentTo(new[] { "Hillary", "Bill" }));
			}
		}

		[Test]
		public void CannotBePersistedWithNonUniqueValuesAsync()
		{
			using (var session = OpenSession())
			using (session.BeginTransaction())
			{
				var e1 = new Employee { HireDate = DateTime.Today, Person = new Person { Name = "Bill", Dob = new DateTime(2000, 1, 1) } };
				var e2 = new Employee { HireDate = DateTime.Today, Person = new Person { Name = "Bill", Dob = new DateTime(2000, 1, 1) } };

				var exception = Assert.ThrowsAsync<GenericADOException>(async () =>
					{
						await (session.SaveAsync(e1));
						await (session.SaveAsync(e2));
						await (session.FlushAsync());
					});
				Assert.That(exception.InnerException, Is.InstanceOf<DbException>());
				Assert.That(exception.InnerException.Message,
					Does.Contain("unique").IgnoreCase
					.Or.Contains("duplicate entry").IgnoreCase);
			}
		}
	}
}
