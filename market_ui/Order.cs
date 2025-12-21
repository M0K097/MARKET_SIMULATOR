public enum order_type
{
	ask,
	bid
}
public class Order
{
	public int id {get; private set; }
	public order_type type {get; private set;}
	public decimal price { get; private set;}
	public double amount {get;  set;}
	public DateTime time {get; private set;}

	public Order(int id, order_type type, decimal price, double amount, DateTime time)
	{
		this.id = id;
		this.type = type;
		this.price = price;
		this.amount = amount;
		this.time = time;
		
	}

}
