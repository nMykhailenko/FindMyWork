namespace FindMyWork.Modules.Jobs.Core.Application.Common.Contracts;

public interface IUriService
{
    Uri GetPageUri(int page, int take, string route);
}