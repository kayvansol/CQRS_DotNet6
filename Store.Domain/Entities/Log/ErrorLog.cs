
namespace Store.Domain.Log;

public partial class ErrorLog: BaseEntity<int>
{
    public int Id { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? Parameters { get; set; }

    public string? Exception { get; set; }
}
