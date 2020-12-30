using System;
using System.Collections.Generic;
using System.Text;

public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
{
}
