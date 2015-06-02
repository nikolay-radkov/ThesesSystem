namespace ThesesSystem.Data.TableGenerator
{
    using ThesesSystem.Common.DataGenerators;

    public interface ITableGenerator
    {
        int Count { get; }

        IRandomGenerator Generator { get; }

        IThesesSystemDbContext Context { get; set; }

        void Generate();
    }
}
