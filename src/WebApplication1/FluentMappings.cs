using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;


namespace WebApplication1
{
	/// <summary>
	/// https://github.com/zkweb-framework/ZKWeb/blob/3522c1298ad8185f41a84454d9ad0113d83e01cf/ZKWeb/ZKWeb.ORM.NHibernate/NHibernateDatabaseContextFactory.cs#L0-L1
	/// </summary>
	public class FluentMappings_Auto
	{

		//private static NHibernate.ISessionFactory CreateSessionFactory()
		//{
		//	return Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("ConnectionString")));
	 // }
		/// <summary>
		///https://www.cnblogs.com/doublog/archive/2013/03/06/2946555.html
		/// FluentMappings和AutoMappings混合使用
		///Fluent NHibernate的AutoMapping功能还是极大的提高开发效率的，只要满足基本的默认规则就行，在建立新项目时尤其好用。但在需要使用已存在的表或者是不满足默认规则的时候就可能需要自动以Map来实现了。配置代码如下：
		/// </summary>
		/// <returns></returns>
		public static FluentConfiguration GetConfiguration()
		{
			var model = FluentNHibernate.Automapping.AutoMap.Assembly(System.Reflection.Assembly.Load("NHibernate.DomainModel")).
			Where(t => t.Namespace == "NHibernate.DomainModel.NHSpecific")
			.IgnoreBase<NHibernate.DomainModel.NHSpecific.BasicClass>()
			;

			return Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("ConnectionString")))
				.Mappings(m =>
				{
					m.AutoMappings.Add(model);
					m.FluentMappings.AddFromAssemblyOf<NHibernate.DomainModel.NHSpecific.Child>();
				});
          }


		//public static FluentConfiguration GetConfiguration()
		//{
		//	var model = AutoMap.Assembly(Assembly.Load("EntApp.ShineFlow.Data")).
		//	Where(t => t.Namespace == "EntApp.ShineFlow.Data.Model")
		//	.IgnoreBase<BaseEntity>()
		//	.Conventions.AddFromAssemblyOf<EnumConvention>()
		//	.Conventions.Add<AssignedIdConvention>()
		//	.Conventions.Add<BinaryColumnLengthConvention>()
		//	;

		//	return Fluently.Configure()
		//		.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("ConnectionString")))
		//		.Mappings(m => {
		//			m.AutoMappings.Add(model);
		//			m.FluentMappings.AddFromAssemblyOf<EntApp.ShineFlow.Data.CustomModel.User>();
		//		})

		//		.ExposeConfiguration(f => f.SetInterceptor(new SqlStatementInterceptor()));
		//}

	}
}
