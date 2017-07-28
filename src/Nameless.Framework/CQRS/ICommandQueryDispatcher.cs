using Nameless.Framework.CQRS.Command;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.CQRS {

    public interface ICommandQueryDispatcher : ICommandDispatcher, IQueryDispatcher {
    }
}