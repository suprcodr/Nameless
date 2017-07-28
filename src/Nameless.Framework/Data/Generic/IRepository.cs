namespace Nameless.Framework.Data.Generic {

    /// <summary>
    /// Repository interface.
    /// </summary>
    public interface IRepository : IPersister, IQuerier, IDirectiveExecutor {
    }
}