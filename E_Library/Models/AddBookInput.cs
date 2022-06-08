namespace E_Library.Models {
    public record AddBookInput (string Title, string Year, int AuthorId, string Isbn, string Price,
        string Edition_statement, string Item_call_number, string Publisher_code, string Number_of_copies);
}
