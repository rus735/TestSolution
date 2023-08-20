using DataManager.Models.Base;

namespace DataManager.Models.EF
{
    #region snippet
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsComplete { get; set; }
    }
    #endregion
}