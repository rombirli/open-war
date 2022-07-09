namespace Save
{
    public interface ISaver
    {
        public void Save(string path);
        public bool Load(string path);
    }
}