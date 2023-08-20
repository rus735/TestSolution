namespace DataManager.Models.FormData
{
    #region snippet
    public class TodoItemFormData
    {
        public required string Name { get; set; }
    }
    public class TodoItemIdFormData
    {
        public long Id { get; set; }
        public required string Name { get; set; }
    }    
    public class TodoItemUpdateFormData
    {
        public required List<long> Id { get; set; }
    }
    #endregion
}