// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GreetDeadlines.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GreetDeadlines {
  public static partial class GreetDeadlinesService
  {
    static readonly string __ServiceName = "greetDeadlines.GreetDeadlinesService";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::GreetDeadlines.GreetDeadlinesRequest> __Marshaller_greetDeadlines_GreetDeadlinesRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GreetDeadlines.GreetDeadlinesRequest.Parser));
    static readonly grpc::Marshaller<global::GreetDeadlines.GreetDeadlinesResponse> __Marshaller_greetDeadlines_GreetDeadlinesResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GreetDeadlines.GreetDeadlinesResponse.Parser));

    static readonly grpc::Method<global::GreetDeadlines.GreetDeadlinesRequest, global::GreetDeadlines.GreetDeadlinesResponse> __Method_greet_with_deadline = new grpc::Method<global::GreetDeadlines.GreetDeadlinesRequest, global::GreetDeadlines.GreetDeadlinesResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "greet_with_deadline",
        __Marshaller_greetDeadlines_GreetDeadlinesRequest,
        __Marshaller_greetDeadlines_GreetDeadlinesResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GreetDeadlines.GreetDeadlinesReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GreetDeadlinesService</summary>
    [grpc::BindServiceMethod(typeof(GreetDeadlinesService), "BindService")]
    public abstract partial class GreetDeadlinesServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::GreetDeadlines.GreetDeadlinesResponse> greet_with_deadline(global::GreetDeadlines.GreetDeadlinesRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GreetDeadlinesService</summary>
    public partial class GreetDeadlinesServiceClient : grpc::ClientBase<GreetDeadlinesServiceClient>
    {
      /// <summary>Creates a new client for GreetDeadlinesService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public GreetDeadlinesServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GreetDeadlinesService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public GreetDeadlinesServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected GreetDeadlinesServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected GreetDeadlinesServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::GreetDeadlines.GreetDeadlinesResponse greet_with_deadline(global::GreetDeadlines.GreetDeadlinesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return greet_with_deadline(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::GreetDeadlines.GreetDeadlinesResponse greet_with_deadline(global::GreetDeadlines.GreetDeadlinesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_greet_with_deadline, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::GreetDeadlines.GreetDeadlinesResponse> greet_with_deadlineAsync(global::GreetDeadlines.GreetDeadlinesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return greet_with_deadlineAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::GreetDeadlines.GreetDeadlinesResponse> greet_with_deadlineAsync(global::GreetDeadlines.GreetDeadlinesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_greet_with_deadline, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override GreetDeadlinesServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GreetDeadlinesServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GreetDeadlinesServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_greet_with_deadline, serviceImpl.greet_with_deadline).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GreetDeadlinesServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_greet_with_deadline, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GreetDeadlines.GreetDeadlinesRequest, global::GreetDeadlines.GreetDeadlinesResponse>(serviceImpl.greet_with_deadline));
    }

  }
}
#endregion
