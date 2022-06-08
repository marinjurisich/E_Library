namespace E_Library.Models {
    public record UpdateBookInput (int Id, string Price,
        string Edition_statement, string Item_call_number, string Publisher_code, string Number_of_copies);
}
