
namespace Store.Domain.Log;

public partial class HandleLog: BaseEntity<int>
{
    public int Id { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? Parameters { get; set; }

    public int? ErrorCode { get; set; }

    public string? Exception { get; set; }

    public long? ExecuteTime { get; set; }
}
