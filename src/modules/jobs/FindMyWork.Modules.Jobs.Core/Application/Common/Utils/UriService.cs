using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts;
using Microsoft.AspNetCore.WebUtilities;

namespace FindMyWork.Modules.Jobs.Core.Application.Common.Utils;

public class UriService : IUriService
{
    private readonly string _baseUri;
    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }
    
    public Uri GetPageUri(int page, int take, string route)
    {
        var endpointUri = new Uri(string.Concat(_baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(
            endpointUri.ToString(), 
            "page", 
            page.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "page", page.ToString());
        return new Uri(modifiedUri);    
    }
}