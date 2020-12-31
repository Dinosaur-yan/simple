using System;
using System.Collections.Generic;
using System.Text;

public interface ITrack
{
    bool IsDelete { get; set; }

    int CreateUser { get; set; }

    DateTime CreateTime { get; set; }

    DateTime? UpdateTime { get; set; }

    DateTime? DeleteTime { get; set; }
}
