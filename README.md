## Simple, flexible, self-contained mediator implementation with no external dependencies

**GOALS:**

1. Commands should be just POCO objects with no dependency on mediator framework or domain code. In that way, the can be shared with other teams as Nuget package and be transported via web API, ServiceBus, cloud services and be used in various scenarios including sync/async, request/response, pub/sub.

2. For request/response scenario Commands can define response type by implementing ICommandResponse<TResponse>. ICommandResponse interface is defined in the Commands project so that Commands project has no dependency on the framework. 

In the MediatR (https://github.com/jbogard/MediatR) library IRequest<> is defined in the framework. Because of that Commands project have a dependency on MediatR framework. That can make it harder to  with other teams.

4. Commands are unaware of is their outcome. They're not implementing any interfaces like ICreate, IUpdate etc. All of that is implemented in a different type of handlers (point 4). That makes it easy to change the outcome of the command without having to share a new version of Commands library with other teams.

5. On the top of default generic handler, it's possible to pre-define different types of custom handlers (like CrateAggregateHandler, UpdateAggregateHandler) to remove boilerplate code duplication from handlers and to be able to implement application-specific middleware for cross-cutting concerns (like logging, security, transaction management). 

You can do it by implementing IHanderType interface and by adding it to the list handler types inside CommandMediator

**EXAMPLES:**

Project: SampleDomain.Tests

**TODO:**

1. Convert all to async
2. Convert from Command Mediator into more generic Message Mediator, because the same solution can be used for handling Queries and Events.
