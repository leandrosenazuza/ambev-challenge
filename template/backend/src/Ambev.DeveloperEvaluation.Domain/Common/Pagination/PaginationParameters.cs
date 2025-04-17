namespace Ambev.DeveloperEvaluation.WebApi.Common.Pagination;


public class PaginationParameters
{
    public string? CustomerName { get; set; }
    public decimal? MinTotal { get; set; }
    public decimal? MaxTotal { get; set; }
   
    private DateTime? _minDate;
    public DateTime? MinDate
    {
        get => _minDate;
        set => _minDate = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
    }

    private DateTime? _maxDate;
    public DateTime? MaxDate
    {
        get => _maxDate;
        set => _maxDate = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
    }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
}

