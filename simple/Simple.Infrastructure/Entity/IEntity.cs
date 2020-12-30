using System;
using System.Collections.Generic;
using System.Text;

public interface IEntity : IEntity<int>
{
}

public interface IEntity<TPrimaryKey> : ITrack
{
    TPrimaryKey Id { get; set; }
}
