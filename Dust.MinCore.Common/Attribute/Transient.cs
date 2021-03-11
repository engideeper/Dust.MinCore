namespace Dust.MinCore.Common.Attribute
{
    public class Transient : System.Attribute
    {
        public string IName { get; }

        public Transient(string iName)
        {
            IName = iName;
        }
    }
}
