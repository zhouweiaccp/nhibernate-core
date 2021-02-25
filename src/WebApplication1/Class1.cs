using System;
using System.IO;
using ConfOrm;
using ConfOrm.NH;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Tool.hbm2ddl;

namespace YJingLee.TryConfOrm
{
	class Program
	{
		public static ObjectRelationalMapper GetMappedDomain()
		{
			var orm = new ObjectRelationalMapper();
			orm.TablePerClass<Domain>();
			return orm;
		}
		public static HbmMapping GetMapping()
		{
			var orm = new ObjectRelationalMapper();
			orm.TablePerClass<Domain>();
			var mapper = new Mapper(orm);
			return mapper.CompileMappingFor(new[] { typeof(Domain) });
		}

		public static void ShowXmlMapping()
		{
			//var document = CreateXmlMappings.Serialize(GetMapping());
			//File.WriteAllText("MyMapping.hbm.xml", document);
			//Console.Write(document);
		}

		public static void JustForConfOrm()
		{
			////配置NHibernate
			//var conf = NhConfig.ConfigureNHibernate();
			////在Configuration中添加HbmMapping
			//conf.AddDeserializedMapping(GetMapping(), "Domain");
			////配置数据库架构元数据
			//SchemaMetadataUpdater.QuoteTableAndColumns(conf);
			////创建数据库架构
			//new SchemaExport(conf).Create(false, true);
			////建立SessionFactory
			//var factory = conf.BuildSessionFactory();
			////打开Session做持久化数据
			//using (var s = factory.OpenSession())
			//{
			//	using (var tx = s.BeginTransaction())
			//	{
			//		var domain = new Domain { Name = "我的测试" };
			//		s.Save(domain);
			//		tx.Commit();
			//	}
			//}
			////打开Session做删除数据
			//using (var s = factory.OpenSession())
			//{
			//	using (var tx = s.BeginTransaction())
			//	{
			//		s.CreateQuery("delete from Domain").ExecuteUpdate();
			//		tx.Commit();
			//	}
			//}
			////删除数据库架构
			//new SchemaExport(conf).Drop(false, true);
		}

		static void Main1(string[] args)
		{
			ShowXmlMapping();
			JustForConfOrm();
			//运行后可以打开输出目录MyMapping.hbm.xml文件看看输出xml
			Console.ReadKey();
		}
	}

	internal class Domain
	{
	}
}
