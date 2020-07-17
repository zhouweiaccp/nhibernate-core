

## INNER JOIN
	SELECT * FROM Ref INNER JOIN Product Prod 
	on Ref.id = Prod.id_referencia AND Prod.ProdDtAlteracao =
	(SELECT MAX(Prod2.ProdDtAlteracao) FROM Product Prod2 WHERE Prod.ProdCod = Prod2.ProdCod)

	session.CreateCriteria<Ref>("r")
    .CreateAlias("products", "p", InnerJoin)
    .Add(Subqueries.PropertyEq("p.ProdDtAlteracao", DetachedCriteria.For<Product>("p2")
        .SetProjection(Projections.Max("p2.ProdDtAlteracao"))
        .Add(Restrictions.EqProperty("p.ProdCod", "p2.ProdCod"))))
    .List<Ref>();