using System.Collections;

namespace WebApi.Test.InlineData;
public class CultureInlineDataTest : IEnumerable<Object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new Object[] { "fr" };
        yield return new Object[] { "pt-BR" };
        yield return new Object[] { "pt-PT" };
        yield return new Object[] { "en" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
