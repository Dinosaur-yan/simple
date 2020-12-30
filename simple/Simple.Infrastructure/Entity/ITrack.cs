using System;
using System.Collections.Generic;
using System.Text;

public interface ITrack
{
    IsDelete IsDelete { get; set; }

    int CreateUser { get; set; }

    DateTime CreateTime { get; set; }

    DateTime? DeleteTime { get; set; }
}
