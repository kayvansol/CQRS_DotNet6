using Store.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Store.Domain.Log;

public partial class OperationLog: BaseEntity<int>
{
    public int Id { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? Parameters { get; set; }

    public string? Answer { get; set; }

    public long? ExecuteTime { get; set; }
}
