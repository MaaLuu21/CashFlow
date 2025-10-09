using System.Globalization;

namespace CashFlow.Api.Middleware;

public class CultureMiddleware
{
    //requestdelagate é como uma permissão se pode ou não continuar o fluxo
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    //precisa ser o nome Invoke
    public async Task Invoke(HttpContext context)
    {
        //lista de idiomas suportados pelo .net
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        //recebe do header qual a cultura deseja
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        //instancia uma cultura padrao
        var cultureInfo = new CultureInfo("en");

        //se ela veio preenchida e não for vazio ou um monte de espaços em branco
        //sobrescreve com a cultura que foi recebida no header
        if (string.IsNullOrEmpty(requestedCulture) == false 
            && supportedLanguages.Exists(l => l.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
