using System;

namespace Tasky.WebApp.EntityFrameworkCore;

public class WebAppEntityFrameworkCoreFixture : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
