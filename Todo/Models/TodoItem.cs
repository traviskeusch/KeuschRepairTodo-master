using SQLite;

namespace Todo
{
	public class TodoItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string CustomerName { get; set; }
		public string Notes { get; set; }
        public string ContactNumber { get; set; }
        public int PartsCost { get; set; }
        public int LaborCost { get; set; }
        public int TotalCost { get; set; }
        public bool Done { get; set; }
        public bool KeepDoneOrder { get; set; }
    }
}

