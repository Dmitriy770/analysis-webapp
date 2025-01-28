using System.Diagnostics;
using System.Reflection;
using System.Text;
using MediatR;

namespace Common.OpenTelemetry.MediatR;

public sealed class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private Activity? StartActiveSpan()
    {
        Consumer
        var activity = Tracing.WebActivitySource.StartActivity(ProducingActivity, ActivityKind.Producer);
        if (activity is not null)
        {
            var propagationContext = new PropagationContext(activity.Context, Baggage.Current);
            Propagators.DefaultTextMapPropagator.Inject(propagationContext, message.Headers ??= new Headers(),
                (headers, key, value) => headers.Add(key, Encoding.UTF8.GetBytes(value)));
        }
        return activity;
    }
    
    private static readonly AssemblyName CurrentAssembly = typeof(TracingBehavior<,>).Assembly.GetName();
    private static string Version => CurrentAssembly.Version!.ToString();
    private static string AssemblyName => CurrentAssembly.Name!;
    public static readonly ActivitySource ConsumerActivitySource = new(AssemblyName, Version);
}