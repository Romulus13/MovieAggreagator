namespace BusinessLayer.Model
{
    public class ImdbSearchResult
    {
        public string? SearchType
        {
            get;
            set;
        }

        public string? Expression
        {
            get;
            set;
        }

        public List<ImdbSearchResultItem>? Results
        {
            get;
            set;
        }

        public string? ErrorMessage
        {
            get;
            set;
        }
    }
}
