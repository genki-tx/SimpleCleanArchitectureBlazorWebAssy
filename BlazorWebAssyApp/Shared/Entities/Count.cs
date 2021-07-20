namespace SCA
{
    public enum CountType : int
    {
        A, B
    }

    // Entity
    // Entity can't depend on View, Presenter, Usecase, Gateway
    public class Count
    {
        public CountType Type { get; set; } // type of this count
        public int Num { get; set; } // numlber of count
    }
}