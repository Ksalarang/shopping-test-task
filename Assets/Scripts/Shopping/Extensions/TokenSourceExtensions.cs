using System.Threading;

namespace Shopping.Extensions
{
    public static class TokenSourceExtensions
    {
        public static void CancelAndDispose(this CancellationTokenSource tokenSource)
        {
            if (tokenSource.IsCancellationRequested == false)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
        }
    }
}