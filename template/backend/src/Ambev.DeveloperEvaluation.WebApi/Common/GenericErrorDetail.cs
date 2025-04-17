using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class GenericErrorDetail
{
    private Func<Type> getType;
    private string message;
    private Func<string> toString;

    public string type { get; set; }
    public string error { get; set; }
    public string detail { get; set; }
    public GenericErrorDetail(string type, string error, string detail) { 
        this.type = type;
        this.error = error;
        this.detail = detail;
    }
}
