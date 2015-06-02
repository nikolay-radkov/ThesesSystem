namespace ThesesSystem.Data.TableGenerator
{
    using ThesesSystem.Common.DataGenerators;

    public abstract class TableGenerator : ITableGenerator
    {
        private IRandomGenerator generator;
        private int count;
        private IThesesSystemDbContext context;

        public TableGenerator(IRandomGenerator generator, IThesesSystemDbContext context, int count)
        {
            this.Generator = generator;
            this.Count = count;
            this.Context = context;
        }

        public IRandomGenerator Generator
        {
            get
            {
                return this.generator;
            }

            private set
            {
                this.generator = value;
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }

            private set
            {
                this.count = value;
            }
        }

        public IThesesSystemDbContext Context
        {
            get
            {
                return this.context;
            }

            set
            {
                this.context = value;
            }
        }

        public abstract void Generate();
    }
}
