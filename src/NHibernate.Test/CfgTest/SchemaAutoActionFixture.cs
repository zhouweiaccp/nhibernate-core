using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernate.Test.CfgTest
{
	[TestFixture]
	public class SchemaAutoActionFixture
	{
		[Test]
		public void Equality()
		{
			Assert.That(SchemaAutoAction.Recreate.Equals("create-drop"));
			Assert.That(SchemaAutoAction.Recreate == "create-drop");
			Assert.That(SchemaAutoAction.Create.Equals("create"));
			Assert.That(SchemaAutoAction.Create == "create");
			Assert.That(SchemaAutoAction.Update.Equals("update"));
			Assert.That(SchemaAutoAction.Update == "update");
			Assert.That(SchemaAutoAction.Validate.Equals("validate"));
			Assert.That(SchemaAutoAction.Validate == "validate");
		}

		[Test]
		public void TestSqlite() {

			// ��ȡ����
			var cfg = new Configuration().Configure("sqlite.xml");
			cfg.Mappings(m => {
				m.DefaultSchema = "tb";//https://www.cnblogs.com/lyj/archive/2010/01/20/inside-nh3-lambda-configuration.html
			});


			cfg.Cache(c =>
			{
				c.UseMinimalPuts = true;
				c.RegionsPrefix = "xyz";
				c.DefaultExpiration = 15;
				c.Provider<HashtableCacheProvider>();
				//c.QueryCache<StandardQueryCache>();
			});
			var dialect = Dialect.Dialect.GetDialect(cfg.GetDerivedProperties());

			//cfg.AddDeserializedMapping()
			dialect.Keywords.Add("Abracadabra");
			// ������ṹ
			SchemaMetadataUpdater.QuoteTableAndColumns(cfg, dialect);
			new SchemaExport(cfg).Create(false, true);

			// ��Session
			var sessionFactory = cfg.BuildSessionFactory();
			//using (var session = sessionFactory.OpenSession())
			//{
			//	// ����
			//	var user = new User();
			//	user.Name = "�����ںη�";
			//	user.Password = "********";
			//	user.Email = "realh3@gmail.com";

			//	session.Save(user);
			//	session.Flush(); // ��ʹ�����������±���Flush

			//	// ��ѯ
			//	var userNow = session.Query<User>().FirstOrDefault();

			//	// �޸�
			//	userNow.Name = "����";
			//	session.Flush();

			//	// ɾ����ʡ�˰�
			//}
		}
			
	}
}
