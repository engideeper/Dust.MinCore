namespace Dust.MinCore.Common.Attribute
{
    public class Singleton : System.Attribute
    {
        public string IName { get; }

        public Singleton(string iName)
        {
            IName = iName;
        }
    }
}
