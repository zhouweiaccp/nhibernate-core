using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH734
{
	[TestFixture]
	public class Fixture : BugTestCase
	{
		[TestAttribute]
		public void LimitProblem()
		{
			using (ISession session = Sfi.OpenSession())
			{
				ICriteria criteria = session.CreateCriteria(typeof(MyClass));
				criteria.SetMaxResults(100);
				criteria.SetFirstResult(0);
				try
				{
					session.BeginTransaction();
					criteria.List<MyClass>();
					session.Transaction.Commit();
				}
				catch
				{
					if (session.Transaction != null)
					{
						session.Transaction.Rollback();
					}
					throw;
				}
			}
		}
	}
}
