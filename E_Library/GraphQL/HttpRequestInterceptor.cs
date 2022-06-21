using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace E_Library.GraphQL {

    public class HttpRequestInterceptor : DefaultHttpRequestInterceptor {

        public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken) {

            
            return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                cancellationToken);

        }

    }
}
