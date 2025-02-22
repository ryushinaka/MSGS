namespace MiniScript.MSGS.Data
{
    public abstract class BaseStringIndex : IStringIndex
    {
        public abstract void OnCreate(ref ValString vs);

        public abstract void OnDelete(ref ValString vs);
        
        public abstract void OnUpdate(ref ValString vs);

        public virtual void SetKey(char c)
        {
            throw new System.NotImplementedException();
        }

        public virtual void IndexLength(uint length)
        {

        }
    }
}


