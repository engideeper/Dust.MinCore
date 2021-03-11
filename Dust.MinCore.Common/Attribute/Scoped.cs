namespace Dust.MinCore.Common.Attribute
{
    public class Scoped : System.Attribute
    {
        public string IName { get; }

        public Scoped(string iName)
        {
            IName = iName;
        }
    }
}
